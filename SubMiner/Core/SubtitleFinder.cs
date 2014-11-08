using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace SubMiner.Core
{
    public class SubtitleFinder
    {
        public List<Subtitle> FindForFile(string filename, string lang)
        {
            var hash = MovieHasher.ComputeMovieHash(filename);
            var matches = FindMatches(hash, lang);
            return CreateSubtitlesFromMatches(matches);
        }

        private MatchCollection FindMatches(string hash, string lang)
        {
            var url = string.Format("http://www.opensubtitles.org/en/search/sublanguageid-{0}/moviehash-{1}/xml", lang, hash);
            var response = this.GetUrlContents(url);

            var pattern = @"<subtitle>.+?'(http:\/\/dl\..+?)'.+?<MovieName><!\[CDATA\[(.+?)]]><\/MovieName>.+?<\/subtitle>";
            var regex = new Regex(pattern, RegexOptions.Singleline);
            return regex.Matches(response);
        }

        private string GetUrlContents(string url)
        {
            using (var response = WebRequest.Create(url).GetResponse())
            using (var stream = response.GetResponseStream())
            using (var buffer = new BufferedStream(stream))
            using (var reader = new StreamReader(buffer))
                return reader.ReadToEnd();
        }

        private List<Subtitle> CreateSubtitlesFromMatches(MatchCollection matches)
        {
            var subtitles = new List<Subtitle>();
            foreach (Match match in matches)
            {
                var name = match.Groups[2].ToString();
                var url = match.Groups[1].ToString();
                var subtitle = new Subtitle(name, url);
                subtitles.Add(subtitle);
            }
            return subtitles;
        }
    }
}
