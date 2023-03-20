using System.Runtime.InteropServices;
using MyPlugins;

namespace PluginB1
{
    public class PluginB1 : IMyPlugin
    {
        [DllImport("NativeLibraryB")]
        private static extern int GetIntegerB();
        
        public string GetResult(string path)
        {
            var bResult = GetIntegerB();
            return $"Hello from PluginB1 b={bResult:x8}";
        }
    }
}