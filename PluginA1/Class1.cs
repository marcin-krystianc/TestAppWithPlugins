
using System.Runtime.InteropServices;
using MyPlugins;

namespace PluginA1
{
    public class PluginA1 : IMyPlugin
    {
        [DllImport("NativeLibraryA")]
        private static extern int GetIntegerA();
        
        public string GetResult(string path)
        {
            var aResult = GetIntegerA();
            return $"Hello from PluginA1 a={aResult:x8}";
        }
    }
}