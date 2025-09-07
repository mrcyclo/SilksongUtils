using GlobalEnums;
using HarmonyLib;
using UnityEngine;
using static HutongGames.PlayMaker.Actions.Vector2RandomValue;

namespace SilksongUtils
{
    internal class IMGUI : MonoBehaviour
    {
        public static IMGUI instance;

        public bool AutoCollect { get; private set; } = false;
        public bool TakeNoDamage { get; private set; } = false;
        public bool DrawHpBar { get; private set; } = false;

        private const int WIDTH = 220;
        private const int HEIGHT = 150;
        private const int PADDING = 10;

        private bool showGui = true;
        private GameManager gameManager = null;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            showGui = false;

            if (gameManager == null) gameManager = Object.FindAnyObjectByType<GameManager>();
            if (gameManager == null) return;

            if (gameManager.ui == null) return;
            if (gameManager.ui.uiState != UIState.PAUSED) return;

            showGui = true;
        }

        private void OnGUI()
        {
            if (!showGui) return;

            int startX = 50; // Screen.width / 2 - WIDTH / 2;
            int startY = 50; // Screen.height / 2 - HEIGHT / 2;

            GUI.Box(new Rect(startX, startY, WIDTH, HEIGHT), "Silksong Utilities");
            startY += 30;

            var saveGame = GUI.Button(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), "Force Save Game");
            if (saveGame)
            {
                GameManager.instance.SaveGame(null);
            }
            startY += 30;

            AutoCollect = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), AutoCollect, " Auto Collect");
            startY += 30;

            TakeNoDamage = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), TakeNoDamage, " Take No Damage");
            startY += 30;

            DrawHpBar = GUI.Toggle(new Rect(startX + PADDING, startY, WIDTH - PADDING * 2, 20), DrawHpBar, " Draw HP bar");
            startY += 30;
        }
    }
}
