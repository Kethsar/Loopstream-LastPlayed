namespace LS_LastPlayed
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.folderTxtBox = new System.Windows.Forms.TextBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.lpDataGrid = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.artistSrchBox = new System.Windows.Forms.TextBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.titleSrchBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.loadingLbl = new System.Windows.Forms.Label();
            this.regexChkBox = new System.Windows.Forms.CheckBox();
            this.casingChkBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.lpDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // folderTxtBox
            // 
            this.folderTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.folderTxtBox.Location = new System.Drawing.Point(50, 7);
            this.folderTxtBox.Name = "folderTxtBox";
            this.folderTxtBox.ReadOnly = true;
            this.folderTxtBox.Size = new System.Drawing.Size(654, 20);
            this.folderTxtBox.TabIndex = 0;
            // 
            // browseBtn
            // 
            this.browseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseBtn.Location = new System.Drawing.Point(710, 5);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(73, 23);
            this.browseBtn.TabIndex = 1;
            this.browseBtn.Text = "Browse...";
            this.toolTip1.SetToolTip(this.browseBtn, "Browse to the folder that has your Loopstream song logs");
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // lpDataGrid
            // 
            this.lpDataGrid.AllowUserToAddRows = false;
            this.lpDataGrid.AllowUserToDeleteRows = false;
            this.lpDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lpDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.lpDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lpDataGrid.Location = new System.Drawing.Point(1, 98);
            this.lpDataGrid.Name = "lpDataGrid";
            this.lpDataGrid.ReadOnly = true;
            this.lpDataGrid.RowHeadersVisible = false;
            this.lpDataGrid.Size = new System.Drawing.Size(782, 487);
            this.lpDataGrid.TabIndex = 10;
            this.lpDataGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.lpDataGrid_ColumnHeaderMouseClick);
            // 
            // artistSrchBox
            // 
            this.artistSrchBox.Location = new System.Drawing.Point(50, 37);
            this.artistSrchBox.Name = "artistSrchBox";
            this.artistSrchBox.Size = new System.Drawing.Size(310, 20);
            this.artistSrchBox.TabIndex = 2;
            this.artistSrchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTxtBox_KeyDown);
            // 
            // searchBtn
            // 
            this.searchBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBtn.Location = new System.Drawing.Point(710, 35);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(73, 23);
            this.searchBtn.TabIndex = 4;
            this.searchBtn.Text = "Search";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // titleSrchBox
            // 
            this.titleSrchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleSrchBox.Location = new System.Drawing.Point(404, 37);
            this.titleSrchBox.Name = "titleSrchBox";
            this.titleSrchBox.Size = new System.Drawing.Size(300, 20);
            this.titleSrchBox.TabIndex = 3;
            this.titleSrchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTxtBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Artist:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(368, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Title:";
            // 
            // loadingLbl
            // 
            this.loadingLbl.AutoSize = true;
            this.loadingLbl.BackColor = System.Drawing.Color.DarkGray;
            this.loadingLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadingLbl.Location = new System.Drawing.Point(307, 108);
            this.loadingLbl.Name = "loadingLbl";
            this.loadingLbl.Size = new System.Drawing.Size(159, 37);
            this.loadingLbl.TabIndex = 11;
            this.loadingLbl.Text = "Loading...";
            this.loadingLbl.Visible = false;
            // 
            // regexChkBox
            // 
            this.regexChkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.regexChkBox.AutoSize = true;
            this.regexChkBox.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.regexChkBox.Location = new System.Drawing.Point(543, 61);
            this.regexChkBox.Name = "regexChkBox";
            this.regexChkBox.Size = new System.Drawing.Size(107, 31);
            this.regexChkBox.TabIndex = 12;
            this.regexChkBox.Text = "Regular Expressions";
            this.regexChkBox.UseVisualStyleBackColor = true;
            this.regexChkBox.CheckedChanged += new System.EventHandler(this.regexChkBox_CheckedChanged);
            // 
            // casingChkBox
            // 
            this.casingChkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.casingChkBox.AutoSize = true;
            this.casingChkBox.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.casingChkBox.Enabled = false;
            this.casingChkBox.Location = new System.Drawing.Point(656, 61);
            this.casingChkBox.Name = "casingChkBox";
            this.casingChkBox.Size = new System.Drawing.Size(88, 31);
            this.casingChkBox.TabIndex = 13;
            this.casingChkBox.Text = "Case Insensitive";
            this.casingChkBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 587);
            this.Controls.Add(this.casingChkBox);
            this.Controls.Add(this.regexChkBox);
            this.Controls.Add(this.loadingLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.titleSrchBox);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.artistSrchBox);
            this.Controls.Add(this.lpDataGrid);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.folderTxtBox);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "Loopstream Last Played";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lpDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox folderTxtBox;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.DataGridView lpDataGrid;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox artistSrchBox;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.TextBox titleSrchBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label loadingLbl;
        private System.Windows.Forms.CheckBox regexChkBox;
        private System.Windows.Forms.CheckBox casingChkBox;
    }
}

