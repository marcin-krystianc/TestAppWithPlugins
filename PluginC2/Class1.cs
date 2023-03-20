using System.Reflection;
using System.Runtime.InteropServices;
using MyPlugins;

namespace PluginC2
{
    public class PluginC2 : IMyPlugin
    {
        [DllImport("NativeLibraryA")]
        private static extern int GetIntegerA();
        
        [DllImport("NativeLibraryC")]
        private static extern int GetIntegerC();
        
        public string GetResult(string path)
        {
            DllImportResolver resolver = (name, assembly, path) =>
            {
                return IntPtr.Zero;
            };
            //NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), resolver);

            var cResult = GetIntegerA() << 16;
            cResult += +GetIntegerC();
            return $"Hello from PluginC2 c={cResult:x8}";
        }
    }
}