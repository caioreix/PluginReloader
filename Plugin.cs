using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using UnityEngine;

namespace PluginReloader;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]

public class Plugin : BasePlugin {
    public readonly static Harmony Harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
    private static MonoBehaviour _clientBehavior;

    public override void Load() {
        Settings.Config.Load(Config, Log, "Client");

        _clientBehavior = AddComponent<Behaviors.Reload>();

        Systems.Reload.LoadPlugins();

        Utils.Logger.Log.Info($"Plugin {MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} loaded!");
    }

    public override bool Unload() {
        if (_clientBehavior != null) {
            Object.Destroy(_clientBehavior);
            _clientBehavior = null;
        }

        Utils.Logger.Log.Info($"Plugin {MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} unloaded!");
        return true;
    }
}
