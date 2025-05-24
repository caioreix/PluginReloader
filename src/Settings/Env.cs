using UnityEngine;
using BepInExUtils = Utils.Settings;

namespace PluginReloader.Settings;

public static class ENV {
    // Mission_General
    private readonly static string settings = "0.⚙️ Settings";
    public static BepInExUtils.ConfigElement<bool> EnableReloadByFile;
    public static BepInExUtils.ConfigElement<KeyCode> ReloadKeyCode;
    public static BepInExUtils.ConfigElement<KeyCode> ReloadSettingsKey;

    public static class Settings {
        public static void Setup() {
            BepInExUtils.Config.AddConfigActions(load);
        }

        // Load the plugin config variables.
        private static void load() {
            EnableReloadByFile = BepInExUtils.Config.Bind(
                settings,
                nameof(EnableReloadByFile),
                true,
                "Enable reload by file, to reload the plugins when the file 'reload' exists in the PluginReloader directory."
            );

            ReloadKeyCode = BepInExUtils.Config.Bind(
                settings,
                nameof(ReloadKeyCode),
                KeyCode.F6,
                "The keycode to reload the plugin in game."
            );

            ReloadSettingsKey = BepInExUtils.Config.Bind(
                settings,
                nameof(ReloadSettingsKey),
                KeyCode.F5,
                "The keycode to reload the plugin settings in game."
            );

            BepInExUtils.Config.Save();
        }
    }
}
