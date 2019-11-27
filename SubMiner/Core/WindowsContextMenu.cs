using Microsoft.Win32;

namespace SubMiner.Core
{
    class WindowsContextMenu
    {
        public void AddEntry(string fileType, string key, string title, string command, string[] extraArguments = null)
        {
            var regEntry = Registry.ClassesRoot.CreateSubKey(fileType + "\\shell\\" + key);
            var regCommand = Registry.ClassesRoot.CreateSubKey(fileType + "\\shell\\" + key + "\\command");

            regEntry.SetValue(null, title);
            regCommand.SetValue(null, MakeCommand(command, extraArguments));
        }

        public void RemoveEntry(string fileType, string key)
        {
            Registry.ClassesRoot.DeleteSubKeyTree(fileType + "\\shell\\" + key, false);
        }

        private string MakeCommand(string command, string[] extraArguments)
        {
            command += " \"%L\"";
            if (extraArguments != null)
            {
                foreach (var arg in extraArguments)
                {
                    command += " \"" + arg + "\"";
                }
            }
            return command;
        }
    }
}
