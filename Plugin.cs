using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using SilksongUtils.Patches;
using UnityEngine;

namespace SilksongUtils
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;
        internal static ConfigEntry<int> configDeathCount;

        private Harmony harmony;
        private GameObject ui;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            // Register config
            configDeathCount = Config.Bind("General", "DeathCount", 0, "Death count");

            // Register patches
            harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll(typeof(ShellShard_AutoCollect));
            harmony.PatchAll(typeof(HealthManager_Patches));
            harmony.PatchAll(typeof(HeroController_Patches));

            // Create UI object
            ui = new GameObject();
            ui.AddComponent<IMGUI>();
            DontDestroyOnLoad(ui);
        }

        private void OnDestroy()
        {
            harmony?.UnpatchSelf();
            GameObject.Destroy(ui);
        }
    }
}
