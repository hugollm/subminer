using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubMiner.Core
{
    public class Subtitle
    {
        public string Name;
        public string Url;

        public Subtitle(string name, string url)
        {
            this.Name = name;
            this.Url = url;
        }
    }
}
