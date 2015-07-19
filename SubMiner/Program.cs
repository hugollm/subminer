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
            var arg = args.Length > 0 ? args[0] : null;
            if (arg == "--install-context")
                InstallContext();
            else if (arg == "--uninstall-context")
                UninstallContext();
            else
                RunGui(arg);
        }

        private static void InstallContext()
        {
            var menu = new WindowsContextMenu();
            var exePath = Assembly.GetEntryAssembly().Location;

            menu.AddEntry("*", "SubMiner", "SubMine!", exePath);
        }

        private static void UninstallContext()
        {
            var menu = new WindowsContextMenu();
            menu.RemoveEntry("*", "SubMiner");
        }

        private static void RunGui(string filePath)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(filePath));
        }
    }
}
