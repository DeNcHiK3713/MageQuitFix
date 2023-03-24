using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.Reflection;

namespace MageQuitFix
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {

        public static ConfigEntry<float> MouseSensitivityEntry;

        private void Awake()
        {
            MouseSensitivityEntry = Config.Bind("Input", "MouseSensitivity", 0.05f, new ConfigDescription("Mouse sensitivity", new AcceptableValueRange<float>(0.001f, 1f)));
            MouseSensitivityEntry.SettingChanged += (_, _) => Globals.wizard_cursor?.SetSensitivity(MouseSensitivityEntry.Value);
            var harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
