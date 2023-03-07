// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
using MyPlugins;

class TestPluginsAlc
{
    static void Main(string[] args)
    {
        var paths = new[]
        {
            @"D:\workspace\TestParquetSharpAlcPlugins\Plugin601\bin\Debug\net6.0\publish\Plugin601.dll",
            @"D:\workspace\TestParquetSharpAlcPlugins\Plugin1001\bin\Debug\net6.0\publish\Plugin1001.dll",
            @"D:\workspace\TestParquetSharpAlcPlugins\MathNet3x\bin\Debug\net6.0\publish\MathNet3x.dll",
            @"D:\workspace\TestParquetSharpAlcPlugins\MathNet50\bin\Debug\net6.0\publish\MathNet50.dll",
        };
        
        var plugins = LoadPlugins(paths);
        
        for (int i = 0; i < 2; i++)
        {
            foreach (var plugin in plugins)
            { 
                var result = plugin.GetResult("MyPath.parquet");
                Console.WriteLine($"{result}");
            }
        }
    }

    static IReadOnlyList<IMyPlugin> LoadPlugins(IEnumerable<string> paths)
    {
        var result = new List<IMyPlugin>();
        
        foreach (var path in paths)
        {  
            var alc = new PluginAlc(path);
            var pluginAssembly = alc.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(path)));
            //var dependencyContext = DependencyContext.Load(pluginAssembly);
            var plugin = CreatePlugin(pluginAssembly);
            result.Add(plugin);
        }

        return result;
    }

    static IMyPlugin CreatePlugin(Assembly assembly)
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