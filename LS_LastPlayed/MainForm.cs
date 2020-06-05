using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LS_LastPlayed
{
    public partial class MainForm : Form
    {
        private const char UP_ARROW = '\u2191';
        private const char DOWN_ARROW = '\u2193';

        private LastPlayedList lpList, filteredLPList;
        private int lastSortedCol;
        private static object syncLock = new object();
        private static readonly string[] tagSeparator = { " - " };

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            lpList = new LastPlayedList();
            filteredLPList = new LastPlayedList();

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.LastPath))
            {
                folderTxtBox.Text = Properties.Settings.Default.LastPath;
                await LoadLastPlayed();
            }
        }

        private async void browseBtn_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (!string.IsNullOrWhiteSpace(folderTxtBox.Text))
                    fbd.SelectedPath = folderTxtBox.Text;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    folderTxtBox.Text = fbd.SelectedPath;
                    Properties.Settings.Default.LastPath = fbd.SelectedPath;
                    Properties.Settings.Default.Save();
                    await LoadLastPlayed();
                }
            }
        }

        private void lpDataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var lastCol = lpDataGrid.Columns[lastSortedCol];
            lastCol.HeaderText = lastCol.DataPropertyName;

            var col = lpDataGrid.Columns[e.ColumnIndex];
            lock (syncLock)
            {
                filteredLPList.Sort(col.DataPropertyName);
                lpDataGrid.DataSource = filteredLPList.List;

                if (filteredLPList.SortDirection == ListSortDirection.Ascending)
                    lpDataGrid.Columns[e.ColumnIndex].HeaderText = col.DataPropertyName + " " + UP_ARROW;
                else
                    lpDataGrid.Columns[e.ColumnIndex].HeaderText = col.DataPropertyName + " " + DOWN_ARROW;
            }

            lastSortedCol = e.ColumnIndex;
            lpDataGrid.Refresh();
        }

        private async Task LoadLastPlayed()
        {
            loadingLbl.Visible = true;
            lpDataGrid.DataSource = null;
            lpDataGrid.Refresh();

            await Task.Run(() => ParseFiles());
            loadingLbl.Visible = false;

            SearchLastPlayed();
        }

        private void ParseFiles()
        {
            var folder = folderTxtBox.Text;
            if (!Directory.Exists(folder)) return;

            var files = Directory.GetFiles(folder, "Loopstream*.txt");
            if (files.Length < 1) return;

            lock (syncLock)
            {
                lpList.Clear();
            }

            foreach (var lsFile in files)
            {
                // File names are saved as Loopstream-<Year>-<Month>-<Day>_<Hour>.<Minute>.<Second>.<extension>.txt
                string streamDate = lsFile.Remove(lsFile.LastIndexOf('.', lsFile.Length - 5)) // .txt and recording extension
                    .Remove(0, lsFile.IndexOf("m-") + 2) // "Loopstream-"
                    .Replace('_', ' ').Replace('.', ':');
                var lpDate = DateTime.Parse(streamDate);

                foreach (var song in File.ReadAllLines(lsFile))
                {
                    // Edge case where the song title is on a new line
                    // Probably because I did something dumb since it only appeared once, 3+ years after I initially made this
                    if (!(song[0] >= '0' && song[0] <= '9'))
                    {
                        lock (syncLock)
                        {
                            var lastLP = lpList.LastOrDefault();
                            if (lastLP != null)
                            {
                                lastLP.Artist = lastLP.Title;
                                lastLP.Title = song;
                            }
                        }

                        continue;
                    }

                    var timeText = song.Remove(song.IndexOf(' '));
                    var timePieces = timeText.Split(':');
                    var songTime = new TimeSpan(int.Parse(timePieces[0]), int.Parse(timePieces[1]), int.Parse(timePieces[2]));
                    var songLP = lpDate.Add(songTime);
                    var relativeLP = DateTime.Now.Subtract(songLP).Days;
                    var songParts = song.Replace(timeText + " ", "").Split(tagSeparator, StringSplitOptions.RemoveEmptyEntries);
                    string artist = "", title = "";

                    if (songParts.Length > 1)
                    {
                        artist = songParts[0];
                        var sb = new StringBuilder();

                        for (int i = 1; i < songParts.Length; i++)
                            sb.Append(songParts[i] + " - ");

                        title = sb.ToString().Trim(' ', '-');
                    }
                    else
                    {
                        title = songParts[0];
                    }

                    // "@" is mice usage, "-" I don't remember
                    if (!string.IsNullOrEmpty(artist) || (title != "@" && title != "-"))
                    {
                        var lp = new LastPlayed(artist, title, relativeLP, songLP);
                        lock (syncLock)
                        {
                            lpList.Add(lp);
                        }
                    }
                }
            }

            lock (syncLock)
            {
                filteredLPList = lpList;
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            SearchLastPlayed();
        }

        private void searchTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchLastPlayed();
        }

        private void regexChkBox_CheckedChanged(object sender, EventArgs e)
        {
            casingChkBox.Enabled = regexChkBox.Checked;
        }

        private void SearchLastPlayed()
        {
            lock (syncLock)
            {
                if ((string.IsNullOrWhiteSpace(artistSrchBox.Text) && string.IsNullOrWhiteSpace(titleSrchBox.Text)) || lpList.Count < 1)
                {
                    if (lpList.Count > 0)
                    {
                        filteredLPList = lpList;
                        lpDataGrid.DataSource = filteredLPList.List;
                        lpDataGrid.Refresh();
                    }
                }
                else
                {
                    IEnumerable<LastPlayed> lpSearch = lpList.List;

                    if (regexChkBox.Checked)
                    {
                        RegexOptions reo = RegexOptions.None;
                        if (casingChkBox.Checked)
                            reo = RegexOptions.IgnoreCase;

                        var reArtist = new Regex(artistSrchBox.Text, reo);
                        var reTitle = new Regex(titleSrchBox.Text, reo);

                        lpSearch = lpSearch.Where(lp => reArtist.IsMatch(lp.Artist) && reTitle.IsMatch(lp.Title));
                    }
                    else
                    {
                        var queries = artistSrchBox.Text.Split(' ');
                        foreach (var q in queries)
                        {
                            lpSearch = lpSearch.Where(lp => lp.Artist.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0);
                        }

                        queries = titleSrchBox.Text.Split(' ');

                        foreach (var q in queries)
                        {
                            lpSearch = lpSearch.Where(lp => lp.Title.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0);
                        }
                    }

                    filteredLPList = new LastPlayedList(lpSearch.ToList());
                    lpDataGrid.DataSource = filteredLPList.List;
                    lpDataGrid.Refresh();
                }
            }
        }
    }

    public class LastPlayed
    {
        public LastPlayed() { }

        public LastPlayed(string artist, string title, int days, DateTime date)
        {
            Artist = artist;
            Title = title;
            Days = days;
            Date = date;
            Plays = 1;
        }

        public LastPlayed(string artist, string title, int days, DateTime date, int playcount)
        {
            Artist = artist;
            Title = title;
            Days = days;
            Date = date;
            Plays = playcount;
        }

        public string Artist { get; set; }
        public string Title { get; set; }
        public Playcount Plays { get; set; }
        public CustDays Days { get; set; } // Days Ago
        public DateTime Date { get; set; }
    }

    public class CustDays : IComparable
    {
        public CustDays() { Days = 0; }

        public CustDays(int i) { Days = i; }

        public int Days { get; set; }

        public static implicit operator CustDays(int i)
        {
            return new CustDays(i);
        }

        public static implicit operator int(CustDays cd)
        {
            return cd.Days;
        }

        public override string ToString()
        {
            return $"{Days} days ago";
        }

        public int CompareTo(object other)
        {
            if (other == null)
                return 1;

            var otherDays = other as CustDays;

            if (otherDays != null)
                return Days.CompareTo(otherDays);
            else
                throw new Exception("Other object is not a CustDays object");
        }
    }

    public class Playcount : IComparable
    {
        public Playcount() { Plays = 0; }

        public Playcount(int i) { Plays = i; }

        public int Plays { get; set; }

        public static implicit operator Playcount(int i)
        {
            return new Playcount(i);
        }

        public static implicit operator int(Playcount cd)
        {
            return cd.Plays;
        }

        public override string ToString()
        {
            return $"{Plays} plays";
        }

        public int CompareTo(object other)
        {
            if (other == null)
                return 1;

            var otherPlaycount = other as Playcount;

            if (otherPlaycount != null)
                return Plays.CompareTo(otherPlaycount);
            else
                throw new Exception("Other object is not a Playcount object");
        }
    }

    public class LastPlayedList : IList<LastPlayed>
    {
        private string lastSorted;

        public LastPlayedList()
        {
            List = new List<LastPlayed>();
            lastSorted = "";
        }

        public LastPlayedList(List<LastPlayed> lastPlayed)
        {
            List = lastPlayed;
            lastSorted = "";
        }

        public void Sort(string property)
        {
            var propInfo = typeof(LastPlayed).GetProperty(property);

            if (lastSorted == propInfo.Name)
            {
                List.Reverse();
                if (SortDirection == ListSortDirection.Ascending)
                    SortDirection = ListSortDirection.Descending;
                else
                    SortDirection = ListSortDirection.Ascending;
            }
            else
            {
                List = List.OrderBy(lp => propInfo.GetValue(lp)).ToList();
                lastSorted = propInfo.Name;
                SortDirection = ListSortDirection.Ascending;
            }
        }

        public List<LastPlayed> List { get; private set; }
        public ListSortDirection SortDirection { get; private set; }

        public LastPlayed this[int index]
        {
            get
            {
                return ((IList<LastPlayed>)List)[index];
            }

            set
            {
                ((IList<LastPlayed>)List)[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return ((IList<LastPlayed>)List).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IList<LastPlayed>)List).IsReadOnly;
            }
        }

        public void Add(LastPlayed item)
        {
            var lpCheck = List.FirstOrDefault(lp => lp.Artist.ToUpper() == item.Artist.ToUpper() && lp.Title.ToUpper() == item.Title.ToUpper());

            if (lpCheck != null)
            {
                lpCheck.Plays++;

                if (lpCheck.Days > item.Days)
                {
                    lpCheck.Date = item.Date;
                    lpCheck.Days = item.Days;
                }
            }
            else
            {
                ((IList<LastPlayed>)List).Add(item);
            }
        }

        public void Clear()
        {
            ((IList<LastPlayed>)List).Clear();
        }

        public bool Contains(LastPlayed item)
        {
            return ((IList<LastPlayed>)List).Contains(item);
        }

        public void CopyTo(LastPlayed[] array, int arrayIndex)
        {
            ((IList<LastPlayed>)List).CopyTo(array, arrayIndex);
        }

        public IEnumerator<LastPlayed> GetEnumerator()
        {
            return ((IList<LastPlayed>)List).GetEnumerator();
        }

        public int IndexOf(LastPlayed item)
        {
            return ((IList<LastPlayed>)List).IndexOf(item);
        }

        public void Insert(int index, LastPlayed item)
        {
            ((IList<LastPlayed>)List).Insert(index, item);
        }

        public bool Remove(LastPlayed item)
        {
            return ((IList<LastPlayed>)List).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<LastPlayed>)List).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<LastPlayed>)List).GetEnumerator();
        }
    }
}
