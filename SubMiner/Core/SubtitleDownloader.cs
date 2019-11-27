using System.IO;
using System.IO.Compression;
using System.Net;

namespace SubMiner.Core
{
    public class SubtitleDownloader
    {
        private const string Extension = ".srt";

        public void DownloadForFile(Subtitle subtitle, string moviePath)
        {
            var zipPath = Path.GetTempFileName();
            var extractDirectory = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(zipPath));

            DownloadZip(subtitle.Url, zipPath);
            ExtractZip(zipPath, extractDirectory);
            MoveSubtitle(extractDirectory, moviePath);
            Cleanup(zipPath, extractDirectory);
        }

        public string SubtitlePathForFile(string moviePath)
        {
            var movieDirectory = Path.GetDirectoryName(moviePath);
            var movieBaseName = Path.GetFileNameWithoutExtension(moviePath);
            return Path.Combine(movieDirectory, movieBaseName + Extension);
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
            var subtitlePath = SubtitlePathForFile(moviePath);
            if (File.Exists(subtitlePath))
            {
                File.Delete(subtitlePath);
            }
            File.Move(GetSubtitleFromExtractDirectory(extractDirectory), subtitlePath);
        }

        private string GetSubtitleFromExtractDirectory(string extractDirectory)
        {
            var subfiles = Directory.GetFiles(extractDirectory, "*" + Extension);
            return subfiles[0];
        }

        private void Cleanup(string zipPath, string extractDirectory)
        {
            File.Delete(zipPath);
            Directory.Delete(extractDirectory, true);
        }
    }
}
