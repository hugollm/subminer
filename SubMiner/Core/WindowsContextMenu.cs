using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace SubMiner.Core
{
    class WindowsContextMenu
    {
        public void AddEntry(string fileType, string key, string title, string command)
        {
            var regEntry = Registry.ClassesRoot.CreateSubKey(fileType + "\\shell\\" + key);
            var regCommand = Registry.ClassesRoot.CreateSubKey(fileType + "\\shell\\" + key + "\\command");

            regEntry.SetValue(null, title);
            regCommand.SetValue(null, command + " %L");
        }

        public void RemoveEntry(string fileType, string key)
        {
            Registry.ClassesRoot.DeleteSubKeyTree(fileType + "\\shell\\" + key);
        }
    }
}
