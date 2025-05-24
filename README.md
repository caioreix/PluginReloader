# PluginReloader

**PluginReloader** is a BepInEx plugin designed to streamline the development and debugging of other BepInEx plugins. It provides hot-reloading capabilities for plugins, configuration and data to make plugin iteration faster and safer.

## Why PluginReloader?

During plugin development, restarting the game to test the plugin, configuration or data changes is slow and tedious. PluginReloader enables you to reload the plugin, configs and data on the fly, toggle debug features.

## Key Features

- **Reload Game/Server Plugins**: Instantly reload the game (client) plugins by pressing a hotkey, or reload the server plugin by creating a file named `reload` in the `BepInEx/PluginReloader` folder. Works for both client and server in any game.
- **Hot-Reload Configs**: Reload plugin configuration files at runtime with a simple keypress or command.

## Installation

1. **Install BepInEx Bleeding Edge**
   Download and install the latest [BepInEx Bleeding Edge](https://builds.bepinex.dev/projects/bepinex_be).

2. **Install PluginReloader**
   Download the latest [PluginReloader.zip](https://github.com/caioreix/PluginReloader/releases) and extract it into your game's `BepInEx/plugins/PluginReloader` directory.

3. **Run the Game**
   Launch your game once to generate PluginReloader's config file.

## Usage

- **Reload Game/Server plugins**:
  - For client mods: Press the configured reload hotkey `F6` (customizable in the config) to restart the game instantly.
  - For servers: Create a file named `reload` inside the `BepInEx/PluginReloader` directory to trigger the server plugin reload. This works for dedicated server and client environments.

## Configuration

PluginReloader generates a config file (`PluginReloader.cfg`) where you can:

- Change the reload plugins hotkey
- Change the reload settings hotkey

## Contributing

Contributions and suggestions are welcome! Open an issue or pull request on [GitHub](https://github.com/caioreix/PluginReloader).

## Credits

PluginReloader was inspired by and borrows ideas from [Bloodstone](https://github.com/decaprime/bloodstone/) by decaprime.
