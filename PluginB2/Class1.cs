using System.Runtime.InteropServices;
using MyPlugins;

namespace PluginB2
{
    public class PluginB2 : IMyPlugin
    {
        [DllImport("NativeLibraryB")]
        private static extern int GetIntegerB();
        
        public string GetResult(string path)
        {
            var bResult = GetIntegerB();
            return $"Hello from PluginB2 b={bResult:x8}";
        }
    }
}