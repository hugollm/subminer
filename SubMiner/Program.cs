using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using SubMiner.Core;

namespace SubMiner
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var arg1 = args.Length > 0 ? args[0] : null;
            var arg2 = args.Length > 1 ? args[1] : null;

            if (arg1 == "--install-context")
                InstallContext();
            else if (arg1 == "--uninstall-context")
                UninstallContext();
            else
                RunGui(arg1, arg2 != null);
        }

        private static void InstallContext()
        {
            var menu = new WindowsContextMenu();
            var exePath = Assembly.GetEntryAssembly().Location;

            menu.AddEntry("*", "SubMiner.1", "SubMiner: Search Subtitles", exePath);
            menu.AddEntry("*", "SubMiner.2", "SubMiner: Download First", exePath, new string[] {"true"});
        }

        private static void UninstallContext()
        {
            var menu = new WindowsContextMenu();
            menu.RemoveEntry("*", "SubMiner.1");
            menu.RemoveEntry("*", "SubMiner.2");
        }

        private static void RunGui(string filePath, bool downloadFirst)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(filePath, downloadFirst));
        }
    }
}
