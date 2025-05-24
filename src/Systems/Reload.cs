using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using Mono.Cecil;
using Utils.Logger;

namespace PluginReloader.Systems;

public static class Reload {
    private static string _reloadPluginsFolder = Path.Combine(Paths.BepInExRootPath, MyPluginInfo.PLUGIN_NAME);
    /// <summary>
    /// Contains the list of all plugins that are loaded and support reloading
    /// They exist outside of <see cref="IL2CPPChainloader"/>"/>
    /// </summary>
    public static List<BasePlugin> LoadedPlugins { get; } = new();

    internal static void UnloadPlugins() {
        for (int i = LoadedPlugins.Count - 1; i >= 0; i--) {
            var plugin = LoadedPlugins[i];

            if (!plugin.Unload()) {
                Log.Warning($"Plugin {plugin.GetType().FullName} does not support unloading, skipping...");
            } else {
                LoadedPlugins.RemoveAt(i);
            }
        }
    }

    internal static void ReloadPlugins() {
        UnloadPlugins();
        LoadPlugins();
    }

    internal static void ReloadIfReloadFileExists() {
        if (!Directory.Exists(_reloadPluginsFolder)) return;

        var reload = Path.Combine(_reloadPluginsFolder, "reload");
        if (!File.Exists(reload)) return;

        File.Move(reload, Path.Combine(_reloadPluginsFolder, "reloaded"));
        ReloadPlugins();
    }

    internal static List<string> LoadPlugins() {
        Log.Info($"Loading plugins from {_reloadPluginsFolder}");
        if (!Directory.Exists(_reloadPluginsFolder)) return new();

        return Directory.GetFiles(_reloadPluginsFolder, "*.dll").SelectMany(LoadPlugin).ToList();
    }

    private static List<string> LoadPlugin(string path) {
        var defaultResolver = new DefaultAssemblyResolver();
        defaultResolver.AddSearchDirectory(_reloadPluginsFolder);
        defaultResolver.AddSearchDirectory(Paths.ManagedPath);
        defaultResolver.AddSearchDirectory(Paths.BepInExAssemblyDirectory);
        defaultResolver.AddSearchDirectory(Path.Combine(Paths.BepInExRootPath, "interop"));

        using var dll = AssemblyDefinition.ReadAssembly(path, new() { AssemblyResolver = defaultResolver });
        dll.Name.Name = $"{dll.Name.Name}-{DateTime.Now.Ticks}";

        using var ms = new MemoryStream();
        dll.Write(ms);

        var loaded = new List<string>();

        var assembly = Assembly.Load(ms.ToArray());
        foreach (var pluginType in assembly.GetTypes().Where(x => typeof(BasePlugin).IsAssignableFrom(x))) {
            // skip plugins already loaded
            if (LoadedPlugins.Any(x => x.GetType() == pluginType)) continue;

            try {
                // we skip chainloader here and don't check dependencies. Fast n dirty.
                var plugin = (BasePlugin)Activator.CreateInstance(pluginType);
                var metadata = MetadataHelper.GetMetadata(plugin);
                LoadedPlugins.Add(plugin);
                plugin.Load();
                loaded.Add(metadata.Name);

                Log.Info($"Loaded plugin {pluginType.FullName}");
            } catch (Exception ex) {
                Log.Error($"Plugin {pluginType.FullName} threw an exception during initialization:");
                Log.Error(ex);
            }
        }

        return loaded;
    }
}
