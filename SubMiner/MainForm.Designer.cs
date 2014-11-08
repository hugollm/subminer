namespace SubMiner
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
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.selectFileField = new System.Windows.Forms.Button();
            this.languageField = new System.Windows.Forms.ComboBox();
            this.subtitleList = new System.Windows.Forms.ListView();
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.urlHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.downloadButton = new System.Windows.Forms.Button();
            this.fileField = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selectFileField
            // 
            this.selectFileField.Location = new System.Drawing.Point(12, 11);
            this.selectFileField.Name = "selectFileField";
            this.selectFileField.Size = new System.Drawing.Size(102, 23);
            this.selectFileField.TabIndex = 1;
            this.selectFileField.Text = "Select File...";
            this.selectFileField.UseVisualStyleBackColor = true;
            this.selectFileField.Click += new System.EventHandler(this.selectFileField_Click);
            // 
            // languageField
            // 
            this.languageField.DisplayMember = "0";
            this.languageField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageField.FormattingEnabled = true;
            this.languageField.Items.AddRange(new object[] {
            "eng",
            "por"});
            this.languageField.Location = new System.Drawing.Point(12, 41);
            this.languageField.Name = "languageField";
            this.languageField.Size = new System.Drawing.Size(102, 21);
            this.languageField.TabIndex = 2;
            // 
            // subtitleList
            // 
            this.subtitleList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.urlHeader});
            this.subtitleList.Enabled = false;
            this.subtitleList.FullRowSelect = true;
            this.subtitleList.Location = new System.Drawing.Point(12, 70);
            this.subtitleList.MultiSelect = false;
            this.subtitleList.Name = "subtitleList";
            this.subtitleList.Size = new System.Drawing.Size(612, 318);
            this.subtitleList.TabIndex = 3;
            this.subtitleList.UseCompatibleStateImageBehavior = false;
            this.subtitleList.View = System.Windows.Forms.View.Details;
            this.subtitleList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.subtitleList_ItemSelectionChanged);
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "Subtitle";
            this.nameHeader.Width = 300;
            // 
            // urlHeader
            // 
            this.urlHeader.Text = "Link";
            this.urlHeader.Width = 300;
            // 
            // downloadButton
            // 
            this.downloadButton.Enabled = false;
            this.downloadButton.Location = new System.Drawing.Point(201, 40);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(75, 23);
            this.downloadButton.TabIndex = 4;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // fileField
            // 
            this.fileField.Location = new System.Drawing.Point(120, 14);
            this.fileField.Name = "fileField";
            this.fileField.ReadOnly = true;
            this.fileField.Size = new System.Drawing.Size(504, 20);
            this.fileField.TabIndex = 5;
            // 
            // searchButton
            // 
            this.searchButton.Enabled = false;
            this.searchButton.Location = new System.Drawing.Point(120, 40);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 6;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(285, 45);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(16, 13);
            this.statusLabel.TabIndex = 7;
            this.statusLabel.Text = "...";
            this.statusLabel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 403);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.fileField);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.subtitleList);
            this.Controls.Add(this.languageField);
            this.Controls.Add(this.selectFileField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SubMiner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.Button selectFileField;
        private System.Windows.Forms.ComboBox languageField;
        private System.Windows.Forms.ListView subtitleList;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.TextBox fileField;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.ColumnHeader urlHeader;
        private System.Windows.Forms.Label statusLabel;
    }
}

