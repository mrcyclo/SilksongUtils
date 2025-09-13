using GlobalEnums;
using UnityEngine;

namespace SilksongUtils
{
    internal class IMGUI : MonoBehaviour
    {
        public static IMGUI instance;

        private const int WIDTH = 220;
        private const int HEIGHT = 150;
        private const int PADDING = 10;

        private bool drawOptionsGui = true;
        private GameManager gameManager = null;
        private GUIStyle deathCountStyle = null;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            drawOptionsGui = false;

            if (gameManager == null) gameManager = Object.FindAnyObjectByType<GameManager>();
            if (gameManager == null) return;

            if (gameManager.ui == null) return;
            if (gameManager.ui.uiState != UIState.PAUSED) return;

            drawOptionsGui = true;
        }

        private void OnGUI()
        {
            if (drawOptionsGui)
            {
                DrawOptionsGUI();
            }

            if (Plugin.configDrawDeathCount.Value)
            {
                deathCountStyle ??= new GUIStyle(GUI.skin.label) { fontSize = 20 };
                GUI.Label(new Rect(20, Screen.height - 40, 200, 40), $"Death Count: {Plugin.configDeathCount.Value}", deathCountStyle);
            }
        }

        private void DrawOptionsGUI()
        {
            int startX = 50; // Screen.width / 2 - WIDTH / 2;
            int startY = 50; // Screen.height / 2 - HEIGHT / 2;

            GUI.Box(new Rect(startX, startY, WIDTH, HEIGHT), "Silksong Utilities");
            startY += 30;

            Plugin.configSkipIntro.Value = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), Plugin.configSkipIntro.Value, " Skip Intro");
            startY += 30;

            Plugin.configAutoCollect.Value = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), Plugin.configAutoCollect.Value, " Auto Collect");
            startY += 30;

            Plugin.configTakeNoDamage.Value = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), Plugin.configTakeNoDamage.Value, " Take No Damage");
            startY += 30;

            Plugin.configDrawHpBar.Value = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), Plugin.configDrawHpBar.Value, " Draw HP bar");
            startY += 30;

            Plugin.configDrawDeathCount.Value = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), Plugin.configDrawDeathCount.Value, " Draw Death Count");
            startY += 30;

            Plugin.configInfiniteSilk.Value = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), Plugin.configInfiniteSilk.Value, " Infinite Silk");
            startY += 30;

            Plugin.configFastAttack.Value = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), Plugin.configFastAttack.Value, " Fast Attack");
            startY += 30;

            Plugin.configAttackToBounce.Value = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), Plugin.configAttackToBounce.Value, " Attack to Bounce");
            startY += 30;

            Plugin.configChangeEquipAnywhere.Value = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), Plugin.configChangeEquipAnywhere.Value, " Change Equip Anywhere");
            startY += 30;

            Plugin.configAlwaysCompass.Value = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), Plugin.configAlwaysCompass.Value, " Always Compass");
            startY += 30;

            Plugin.configInfiniteAttackTool.Value = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), Plugin.configInfiniteAttackTool.Value, " Infinite Attack Tool");
            startY += 30;

            var saveGame = GUI.Button(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), "Force Save Game");
            if (saveGame)
            {
                gameManager.SaveGame(null);
            }
            startY += 30;
        }
    }
}
