using HarmonyLib;
using UnityEngine;

namespace SilksongUtils.Objects
{
    internal class ESP : MonoBehaviour
    {
        private HealthManager healthManager = null;
        private GUIStyle textFieldNoBorder = null;
        private GameManager gameManager = null;

        private void Awake()
        {
            healthManager = GetComponent<HealthManager>();
        }

        private void OnGUI()
        {
            if (gameManager == null) gameManager = Object.FindAnyObjectByType<GameManager>();
            if (gameManager == null) return;

            if (gameManager.ui == null) return;
            if (gameManager.ui.uiState != GlobalEnums.UIState.PLAYING) return;

            if (gameObject == null) return;
            if (gameObject.transform == null) return;
            if (Camera.main == null) return;
            if (healthManager.isDead) return;
            if (!IMGUI.instance.DrawHpBar) return;

            if (textFieldNoBorder == null)
            {
                textFieldNoBorder = new GUIStyle(GUI.skin.textField)
                {
                    normal = { background = null },
                    border = new RectOffset(0, 0, 0, 0),
                    focused = { background = null },
                    active = { background = null },
                    hover = { background = null }
                };
            }

            // Lấy vị trí đối tượng ở thế giới sang toạ độ màn hình
            Vector3 worldPos = gameObject.transform.position + Vector3.up * 2.0f; // Nâng lên trên đầu đối tượng
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

            // Kiểm tra đối tượng nằm trước camera
            if (screenPos.z > 0)
            {
                // Đảo hệ trục Y cho IMGUI
                float guiX = screenPos.x - 50f; // Thanh dài 100px, căn giữa đầu
                float guiY = Screen.height - screenPos.y - 10f;

                float barWidth = 100f;
                float barHeight = 10f;

                int initHp = (int)AccessTools.Field(typeof(HealthManager), "initHp").GetValue(healthManager);
                float healthPercent = Mathf.Clamp01(healthManager.hp * 1.0f / initHp);

                Color oldColor = GUI.color;

                // Nền đen
                GUI.color = Color.black;
                GUI.DrawTexture(new Rect(guiX, guiY, barWidth, barHeight), Texture2D.whiteTexture);

                // Thanh máu màu đỏ
                GUI.color = Color.red;
                GUI.DrawTexture(new Rect(guiX, guiY, barWidth * healthPercent, barHeight), Texture2D.whiteTexture);

                // Vẽ text
                GUI.color = Color.white;
                GUI.TextField(new Rect(guiX, guiY - 20, barWidth, 20), $"{healthManager.hp}/{initHp}", textFieldNoBorder);

                // Reset màu
                GUI.color = oldColor;
            }
        }
    }
}
