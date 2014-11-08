using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace SubMiner.Core
{
    public class SubtitleDownloader
    {
        public void DownloadForFile(Subtitle subtitle, string moviePath)
        {
            var zipPath = Path.GetTempFileName();
            var extractDirectory = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(zipPath));

            DownloadZip(subtitle.Url, zipPath);
            ExtractZip(zipPath, extractDirectory);
            MoveSubtitle(extractDirectory, moviePath);
            Cleanup(zipPath, extractDirectory);
        }

        private void DownloadZip(string url, string zipPath)
        {
            using (WebClient Client = new WebClient())
            {
                Client.DownloadFile(url, zipPath);
            }
        }

        private void ExtractZip(string zipPath, string extractDirectory)
        {
            ZipFile.ExtractToDirectory(zipPath, extractDirectory);
        }

        private void MoveSubtitle(string extractDirectory, string moviePath)
        {
            var movieDirectory = Path.GetDirectoryName(moviePath);
            var movieBaseName = Path.GetFileNameWithoutExtension(moviePath);
            var subtitlePath = Path.Combine(movieDirectory, movieBaseName + ".srt");

            var subfiles = Directory.GetFiles(extractDirectory, "*.srt");
            var subfile = "";
            if (subfiles.Length > 0)
                subfile = subfiles[0];
            if (subfile != "")
                File.Move(subfile, subtitlePath);
        }

        private void Cleanup(string zipPath, string extractDirectory)
        {
            File.Delete(zipPath);
            Directory.Delete(extractDirectory, true);
        }
    }
}
