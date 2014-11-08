using Microsoft.Win32;
using System.Diagnostics;

static class FileShellExtension
{
    public static void Register(string fileType, string shellKeyName, string menuText, string menuCommand)
    {
        string regPath = string.Format(@"{0}\shell\{1}", fileType, shellKeyName);
        using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(regPath))
        {
            key.SetValue(null, menuText);
        }
        using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(string.Format(@"{0}\command", regPath)))
        {
            key.SetValue(null, menuCommand);
        }
    }

    public static void Unregister(string fileType, string shellKeyName)
    {
        Debug.Assert(!string.IsNullOrEmpty(fileType) && !string.IsNullOrEmpty(shellKeyName));
        string regPath = string.Format(@"{0}\shell\{1}", fileType, shellKeyName);
        Registry.ClassesRoot.DeleteSubKeyTree(regPath);
    }
}
