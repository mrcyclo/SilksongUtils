using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace SilksongUtils
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;
        internal static ConfigEntry<bool> configAutoCollect;
        internal static ConfigEntry<bool> configTakeNoDamage;
        internal static ConfigEntry<bool> configDrawHpBar;
        internal static ConfigEntry<bool> configDrawDeathCount;
        internal static ConfigEntry<int> configDeathCount;
        internal static ConfigEntry<bool> configInfiniteSilk;
        internal static ConfigEntry<bool> configFastAttack;
        internal static ConfigEntry<bool> configAttackToBounce;
        internal static ConfigEntry<bool> configChangeEquipAnywhere;
        internal static ConfigEntry<bool> configSkipIntro;
        internal static ConfigEntry<bool> configAlwaysCompass;
        internal static ConfigEntry<bool> configInfiniteAttackTool;

        private Harmony harmony;
        private GameObject ui;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            // Register config
            configSkipIntro = Config.Bind("General", "SkipIntro", false, "Skip Intro");
            configAutoCollect = Config.Bind("General", "AutoCollect", false, "Auto collect");
            configTakeNoDamage = Config.Bind("General", "TakeNoDamage", false, "Take no damage");
            configDrawHpBar = Config.Bind("General", "DrawHpBar", false, "Draw HP bar");
            configDrawDeathCount = Config.Bind("General", "DrawDeathCount", false, "Draw Death Count");
            configDeathCount = Config.Bind("General", "DeathCount", 0, "Death count");
            configInfiniteSilk = Config.Bind("General", "InfiniteSilk", false, "Infinite Silk");
            configFastAttack = Config.Bind("General", "FastAttack", false, "Fast Attack");
            configAttackToBounce = Config.Bind("General", "AttackToBounce", false, "Attack to Bounce");
            configChangeEquipAnywhere = Config.Bind("General", "ChangeEquipAnywhere", false, "Change Equip Anywhere");
            configAlwaysCompass = Config.Bind("General", "AlwaysCompass", false, "Always Compass");
            configInfiniteAttackTool = Config.Bind("General", "InfiniteAttackTool", false, "Infinite Attack Tool");

            // Register patches
            harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            foreach (var type in typeof(Plugin).Assembly.GetTypes())
            {
                if (type.Namespace != "SilksongUtils.Patches") continue;
                harmony.PatchAll(type);
            }

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
