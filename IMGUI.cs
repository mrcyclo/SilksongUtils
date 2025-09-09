using GlobalEnums;
using UnityEngine;

namespace SilksongUtils
{
    internal class IMGUI : MonoBehaviour
    {
        public static IMGUI instance;

        public bool AutoCollect { get; private set; } = false;
        public bool TakeNoDamage { get; private set; } = false;
        public bool DrawHpBar { get; private set; } = false;
        public bool DrawDeathCount { get; private set; } = false;
        public bool InfiniteSilk { get; private set; } = false;

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

            if (DrawDeathCount)
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

            AutoCollect = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), AutoCollect, " Auto Collect");
            startY += 30;

            TakeNoDamage = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), TakeNoDamage, " Take No Damage");
            startY += 30;

            DrawHpBar = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), DrawHpBar, " Draw HP bar");
            startY += 30;

            DrawDeathCount = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), DrawDeathCount, " Draw Death Count");
            startY += 30;

            InfiniteSilk = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), InfiniteSilk, " Infinite Silk");
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
