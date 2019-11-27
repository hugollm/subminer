using Microsoft.Win32;

namespace SubMiner.Core
{
    public class RegistryStore
    {
        private string RootKey;

        public RegistryStore(string rootKey)
        {
            RootKey = rootKey;
        }

        public void Set(string key, string value)
        {
            var rootKey = Registry.CurrentUser.CreateSubKey(RootKey);
            rootKey.SetValue(key, value);
        }

        public string Get(string key)
        {
            var rootKey = Registry.CurrentUser.CreateSubKey(RootKey);
            var value = rootKey.GetValue(key);
            return value != null ? value.ToString() : null;
        }
    }
}
