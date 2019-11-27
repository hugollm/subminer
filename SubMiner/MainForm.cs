using SubMiner.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace SubMiner
{
    public partial class MainForm : Form
    {
        private const string LanguageRegistryName = "lang";

        public SubtitleFinder SubtitleFinder = new SubtitleFinder();
        public SubtitleDownloader SubtitleDownloader = new SubtitleDownloader();
        public RegistryStore RegistryStore = new RegistryStore("SubMiner");

        string Version = "1.1.1";
        bool DownloadFirstAndClose = false;

        public Dictionary<string, string> Languages = new Dictionary<string, string>
        {
            {"English", "eng"},
            {"Portuguese (BR)", "pob"}
        };

        public MainForm(string filePath, bool downloadFirstAndClose)
        {
            InitializeComponent();

            Text += " " + Version;
            MaximizeBox = false;
            MinimizeBox = false;

            DownloadFirstAndClose = downloadFirstAndClose;
            InitializeFields(filePath);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(fileField.Text))
            {
                SearchSubtitles();
            }
        }

        private void InitializeFields(string filePath)
        {
            InitializeLanguage();
            InitializeFilePath(filePath);
        }

        private void InitializeLanguage()
        {
            var lang = RegistryStore.Get(LanguageRegistryName);
            Int32.TryParse(lang, out int intLang);
            languageField.SelectedIndex = intLang;
        }

        private void InitializeFilePath(string filePath)
        {
            if (File.Exists(filePath))
            {
                fileField.Text = filePath;
                searchButton.Enabled = true;
            }
        }

        private void selectFileField_Click(object sender, EventArgs e)
        {
            fileDialog.ShowDialog();
            fileField.Text = fileDialog.FileName;
            if (!string.IsNullOrWhiteSpace(fileField.Text))
            {
                statusLabel.Text = "";
                downloadButton.Enabled = false;
                searchButton.Enabled = true;
                clearSubtitleList();
                SearchSubtitles();
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchSubtitles();
        }

        private void SearchSubtitles()
        {
            startLongProcessing("Searching subtitles...");
            var subtitles = new List<Subtitle>();
            try
            {
                subtitles = SubtitleFinder.FindForFile(fileField.Text, Languages[languageField.Text]);
            }
            catch (WebException wex)
            {
                MessageBox.Show("Connection failure." + Environment.NewLine + wex.Message);
            }
            fillSubtitleList(subtitles);
            subtitleList.Enabled = true;
            endLongProcessing();
            DownloadFirstAndCloseIfRequested();
        }

        private void DownloadFirstAndCloseIfRequested()
        {
            if (!DownloadFirstAndClose || subtitleList.Items.Count == 0)
            {
                return;
            }
            var name = subtitleList.Items[0].SubItems[0].Text;
            var url = subtitleList.Items[0].SubItems[1].Text;
            if (!OverwriteConfirmation())
            {
                return;
            }
            DownloadSubtitle(name, url);
            Application.Exit();
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
            statusLabel.Text = "# of subtitle files found: " + subtitles.Count;
        }

        private void clearSubtitleList()
        {
            for (int i = subtitleList.Items.Count - 1; i >= 0; i--)
            {
                if (subtitleList.Items[i].Selected)
                {
                    subtitleList.Items[i].Remove();
                }
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
            this.Enabled = true;
            Refresh();
        }

        private void subtitleList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            downloadButton.Enabled = (subtitleList.SelectedItems.Count > 0);
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            DownloadSelectedSubtitle();
        }

        private bool OverwriteConfirmation()
        {
            var subtitlePath = SubtitleDownloader.SubtitlePathForFile(fileField.Text);
            if (File.Exists(subtitlePath))
            {
                var result = MessageBox.Show("A subtitle file already exists. Overwrite it?",
                    "Subtitle File Found", MessageBoxButtons.OKCancel);
                return result != DialogResult.Cancel;
            }
            return true;
        }

        private void DownloadSelectedSubtitle()
        {
            if (!OverwriteConfirmation())
            {
                return;
            }
            var name = subtitleList.SelectedItems[0].SubItems[0].Text;
            var url = subtitleList.SelectedItems[0].SubItems[1].Text;
            DownloadSubtitle(name, url);
        }

        private void DownloadSubtitle(string name, string url)
        {
            startLongProcessing("Downloading subtitle...");
            var subtitle = new Subtitle(name, url);
            try
            {
                SubtitleDownloader.DownloadForFile(subtitle, fileField.Text);
                statusLabel.Text = "Download complete.";
            }
            catch (WebException wex)
            {
                MessageBox.Show("Connection failure." + Environment.NewLine + wex.Message);
            }
            endLongProcessing();
        }

        private void languageField_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegistryStore.Set(LanguageRegistryName, languageField.SelectedIndex.ToString());
        }

        private void subtitleList_DoubleClick(object sender, EventArgs e)
        {
            DownloadSelectedSubtitle();
        }
    }
}
