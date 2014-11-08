using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SubMiner.Core;

namespace SubMiner
{
    public partial class MainForm : Form
    {
        public SubtitleFinder SubtitleFinder = new SubtitleFinder();
        public SubtitleDownloader SubtitleDownloader = new SubtitleDownloader();

        public MainForm(string[] args)
        {
            InitializeComponent();
            InitializeFields(args);
        }

        private void InitializeFields(string[] args)
        {
            languageField.SelectedIndex = 0;
            
            if (args.Length > 0 && File.Exists(args[0]))
            {
                fileField.Text = args[0];
                searchButton.Enabled = true;
            }
        }

        private void selectFileField_Click(object sender, EventArgs e)
        {
            fileDialog.ShowDialog();
            fileField.Text = fileDialog.FileName;
            if (fileField.Text != "")
                searchButton.Enabled = true;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            startLongProcessing("Searching subtitles...");

            var subtitles = SubtitleFinder.FindForFile(fileField.Text, languageField.Text);
            fillSubtitleList(subtitles);
            subtitleList.Enabled = true;

            endLongProcessing();
        }

        private void fillSubtitleList(List<Subtitle> subtitles)
        {
            subtitleList.Items.Clear();
            foreach (var sub in subtitles)
            {
                string[] row = { sub.Name, sub.Url };
                var item = new ListViewItem(row);
                subtitleList.Items.Add(item);
            }
        }

        private void startLongProcessing(string status)
        {
            statusLabel.Text = status;
            statusLabel.Visible = true;
            this.Enabled = false;
            Refresh();
        }

        private void endLongProcessing()
        {
            statusLabel.Text = "...";
            statusLabel.Visible = false;
            this.Enabled = true;
            Refresh();
        }

        private void subtitleList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (subtitleList.SelectedItems.Count > 0)
                downloadButton.Enabled = true;
            else
                downloadButton.Enabled = false;
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            startLongProcessing("Downloading subtitle...");
            
            var name = subtitleList.SelectedItems[0].SubItems[0].Text;
            var url = subtitleList.SelectedItems[0].SubItems[1].Text;
            var subtitle = new Subtitle(name, url);
            SubtitleDownloader.DownloadForFile(subtitle, fileField.Text);

            endLongProcessing();
        }
    }
}
