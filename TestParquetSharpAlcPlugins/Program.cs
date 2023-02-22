// See https://aka.ms/new-console-template for more information

using System;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
using MyPlugins;

class TestPluginsAlc
{
    static void Main(string[] args)
    {
        var alc601 = new PluginAlc(@"D:\workspace\TestParquetSharpAlcPlugins\Plugin601\bin\Debug\net7.0\win-x64\publish\Plugin601.dll");
        //var pluginAssembly = alc.LoadFromAssemblyName(assemblyName);
        
        var plugin601Assembly = alc601.LoadFromAssemblyName(new AssemblyName("Plugin601"));
        var dependency601Context = DependencyContext.Load(plugin601Assembly);
        var plugin601Command = CreateCommand(plugin601Assembly);
        var plugin601Result = plugin601Command.GetResult("MyPath.parquet");
        Console.WriteLine($"{plugin601Result}");
        
        var alc1001 = new PluginAlc(@"D:\workspace\TestParquetSharpAlcPlugins\Plugin1001\bin\Debug\net7.0\win-x64\publish\Plugin1001.dll");
        var plugin1001Assembly = alc1001.LoadFromAssemblyName(new AssemblyName("Plugin1001"));
        var dependency1001Context = DependencyContext.Load(plugin1001Assembly);
        var plugin1001Command = CreateCommand(plugin1001Assembly);
        var plugin1001Result = plugin1001Command.GetResult("MyPath.parquet");
        Console.WriteLine($"{plugin1001Result}");
    }

    static IMyPlugin CreateCommand(Assembly assembly)
    {
        int count = 0;

        Console.WriteLine(typeof(IMyPlugin).Module.FullyQualifiedName);
        foreach (Type type in assembly.GetTypes())
        {
            foreach (var ii in type.GetInterfaces ())
            {
                Console.WriteLine(ii.Module.FullyQualifiedName);
            }    
            
            if (typeof(IMyPlugin).IsAssignableFrom(type))
            {
                IMyPlugin result = Activator.CreateInstance(type) as IMyPlugin;
                if (result != null)
                {
                    return result;
                }
            }
        }

        return null;
    }
}

class PluginAlc : AssemblyLoadContext
{
    private readonly AssemblyDependencyResolver _resolver;
    public PluginAlc(string path)
    {
        _resolver = new AssemblyDependencyResolver(path);
    }
    protected override Assembly Load(AssemblyName assemblyName)
    {
        var assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
        return assemblyPath != null ? LoadFromAssemblyPath(assemblyPath) : null;
    }
    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        var assemblyPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
        
        Console.WriteLine($"{assemblyPath}");
        return assemblyPath != null ? LoadUnmanagedDllFromPath(assemblyPath) : IntPtr.Zero;
    }
}