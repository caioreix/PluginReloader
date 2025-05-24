using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace PluginReloader;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]

public class Plugin : BasePlugin {
    public readonly static Harmony Harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
    public override void Load() {
        Settings.Config.Load(Config, Log, "Client");

        Utils.Logger.Log.Trace("Patching harmony");
        Harmony.PatchAll();

        Utils.Logger.Log.Info($"Plugin {MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} loaded!");
    }

    public override bool Unload() {
        Utils.Logger.Log.Trace("Unpatching harmony");
        Harmony.UnpatchSelf();
        Utils.Logger.Log.Info($"Plugin {MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} unloaded!");
        return true;
    }
}
