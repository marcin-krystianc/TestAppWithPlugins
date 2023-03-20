using System.Runtime.InteropServices;
using MyPlugins;

namespace PluginC1
{
    public class PluginC1 : IMyPlugin
    {
        [DllImport("NativeLibraryC")]
        private static extern int GetIntegerC();
        
        public string GetResult(string path)
        {
            var cResult = GetIntegerC();
            return $"Hello from PluginC1 c={cResult:x8}";
        }
    }
}