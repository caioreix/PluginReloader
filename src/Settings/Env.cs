using Utils.Settings;

namespace PluginReloader.Settings;

public static class ENV {
    // Mission_General
    private readonly static string example = "0.🧪 Example";
    public static ConfigElement<float> Example;

    public static class Settings {
        public static void Setup() {
            Utils.Settings.Config.AddConfigActions(load);
        }

        // Load the plugin config variables.
        private static void load() {
            // Mission_General
            Example = Utils.Settings.Config.Bind(
                example,
                nameof(Example),
                2F,
                "Just an example value :)"
            );

            Utils.Settings.Config.Save();
        }
    }
}
