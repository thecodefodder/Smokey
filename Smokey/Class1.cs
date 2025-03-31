using Il2CppScheduleOne.Money;
using Il2CppScheduleOne.PlayerScripts;
using MelonLoader;
using UnityEngine;

namespace Smokey
{
    public class Class1 : MelonMod
    {
        private static KeyCode guiToggleKey = KeyCode.Insert;
        private static bool showUI = false;
        private static Rect windowRect = new Rect(20, 20, 300, 400);
        private static int windowID = 1001;

        private bool espEnabled = false;

        public override void OnGUI()
        {
            if (showUI)
            {
                GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);

                windowRect = GUI.Window(windowID, windowRect, (GUI.WindowFunction)DrawWindow, "Smokey Menu");
            }

            if(espEnabled)
            {
                DrawESP(GetPlayers());
            }
        }

        private void DrawWindow(int id)
        {
            GUILayout.BeginVertical();

            #region Money Options
            GUILayout.Label("Money options");

            if (GUILayout.Button("Add 100"))
            {
                GiveMoney(100);
            }

            if (GUILayout.Button("Add 1000"))
            {
                GiveMoney(1000);
            }

            if (GUILayout.Button("Add 10000"))
            {
                GiveMoney(10000);
            }

            if (GUILayout.Button("Add 100000"))
            {
                GiveMoney(100000);
            }
            #endregion

            if (GUILayout.Button("DEBUG: Print players"))
            {
                foreach (Player player in GetPlayers())
                {
                    LoggerInstance.Msg($"{player.name} at {player.transform.position}");
                }
            }

            espEnabled = GUILayout.Toggle(espEnabled, "Enable Player ESP");

            GUILayout.EndVertical();

            GUI.DragWindow();
        }

        private void GiveMoney(int moneyValue)
        {
            MoneyManager.Instance.ChangeCashBalance(moneyValue);
        }

        private static Il2CppSystem.Collections.Generic.List<Player> GetPlayers()
        {
            return Player.PlayerList;
        }

        private void DrawESP(Il2CppSystem.Collections.Generic.List<Player> players)
        {
            Camera mainCamera = Camera.main;
            if (players == null || players.Count == 0 || mainCamera == null)
                return;

            foreach (Player player in players)
            {
                if (player == null || player.transform == null)
                    continue;

                try
                {
                    Vector3 playerPos = player.transform.position;
                    Vector3 screenPos = mainCamera.WorldToScreenPoint(playerPos);

                    if (screenPos.z > 0)
                    {
                        float boxHeight = 200f;
                        float boxWidth = 80f;

                        // Convert world position to screen position
                        Vector2 boxPos = new Vector2(screenPos.x - boxWidth / 2, Screen.height - screenPos.y - boxHeight / 2);

                        // Draw ESP Box
                        DrawBox(boxPos, boxWidth, boxHeight, Color.red);

                        // Draw Player Name
                        GUI.Label(new Rect(boxPos.x, boxPos.y - 20, 100, 20), player.name);
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.Log($"ESP Error: {ex.Message}");
                }
            }
        }
        private void DrawBox(Vector2 pos, float width, float height, Color color)
        {
            GUI.color = color;
            GUI.DrawTexture(new Rect(pos.x, pos.y, width, height), Texture2D.whiteTexture);
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(guiToggleKey))
            {
                showUI = !showUI;
                Cursor.visible = showUI;
                if (showUI)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                else { 
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }
}