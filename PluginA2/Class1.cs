using System.Runtime.InteropServices;
using MyPlugins;

namespace PluginA2
{
    public class PluginA2 : IMyPlugin
    {
        [DllImport("NativeLibraryA")]
        private static extern int GetIntegerA();
        
        public string GetResult(string path)
        {
            var aResult = GetIntegerA();
            return $"Hello from PluginA2 a={aResult:x8}";
        }
    }
}