﻿using System;
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
using System.Net;

namespace SubMiner
{
    public partial class MainForm : Form
    {
        public SubtitleFinder SubtitleFinder = new SubtitleFinder();
        public SubtitleDownloader SubtitleDownloader = new SubtitleDownloader();
        public RegistryStore RegistryStore = new RegistryStore("SubMiner");

        string Version = "1.1.0";
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
            if (fileField.Text.Trim() != "")
                SearchSubtitles();
        }

        private void InitializeFields(string filePath)
        {
            InitializeLanguage();
            InitializeFilePath(filePath);
        }

        private void InitializeLanguage()
        {
            var lang = RegistryStore.Get("lang");
            int intLang = 0;
            Int32.TryParse(lang, out intLang);
            languageField.SelectedIndex = intLang;
        }

        private void InitializeFilePath(string filePath)
        {
            if (filePath != null && File.Exists(filePath))
            {
                fileField.Text = filePath;
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
            catch (WebException)
            {
                MessageBox.Show("Connection failure.");
            }
            fillSubtitleList(subtitles);
            subtitleList.Enabled = true;
            endLongProcessing();
            DownloadFirstAndCloseIfRequested();
        }

        private void DownloadFirstAndCloseIfRequested()
        {
            if (!DownloadFirstAndClose || subtitleList.Items.Count == 0)
                return;
            var name = subtitleList.Items[0].SubItems[0].Text;
            var url = subtitleList.Items[0].SubItems[1].Text;
            if (!OverwriteConfirmation())
                return;
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
            if (!OverwriteConfirmation())
                return;
            DownloadSelectedSubtitle();
        }

        private bool OverwriteConfirmation()
        {
            var subtitlePath = SubtitleDownloader.SubtitlePathForFile(fileField.Text);
            if (File.Exists(subtitlePath))
            {
                var result = MessageBox.Show("A subtitle was already found. Ovewrite it?", "Subtitle found", MessageBoxButtons.OKCancel);
                return result != DialogResult.Cancel;
            }
            return true;
        }

        private void DownloadSelectedSubtitle()
        {
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
            }
            catch (WebException)
            {
                MessageBox.Show("Connection failure.");
            }
            endLongProcessing();
        }

        private void languageField_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegistryStore.Set("lang", languageField.SelectedIndex.ToString());
        }
    }
}
