

using UnityEngine;

namespace PluginReloader.Behaviors;

public class Reload : MonoBehaviour {
    public void Update() {
        if (Input.GetKeyDown(Settings.ENV.ReloadSettingsKey.Value)) {
            Utils.Settings.Config.Reload();
        }

        if (Input.GetKeyDown(Settings.ENV.ReloadKeyCode.Value)) {
            Systems.Reload.ReloadPlugins();
        }

        if (!Settings.ENV.EnableReloadByFile.Value) return;

        if (Utils.Database.Cache.IsBlocked("ReloadIfReloadFileExists", 5 * 1000)) return;

        Systems.Reload.ReloadIfReloadFileExists();
    }
}
