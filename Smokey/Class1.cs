using MelonLoader;
using UnityEngine;

namespace Smokey
{
    public class Class1 : MelonMod
    {
        private bool playerEspEnabled = false;
        private bool npcEspEnabled = false;
        private bool disablePolice = false;
        private static float selectedTime = 1;
        private bool isMainScene = false;
        private Utils.FlipFlop undetectedFlipFlop = new Utils.FlipFlop(false);

        ESP esp = new ESP();

        private int currentTab = 99;
        private string[] tabNames = { "Money", "XP", "ESP", "Time", "Misc" };

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            base.OnSceneWasLoaded(buildIndex, sceneName);
            Utils.CheckMainScene(sceneName, ref isMainScene, false);
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            base.OnSceneWasUnloaded(buildIndex, sceneName);

            Utils.CheckMainScene(sceneName, ref isMainScene, true);
        }

        public override void OnGUI()
        {
            if (Settings.showUI)
            {
                GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);

                Settings.windowRect = GUI.Window(Settings.windowID, Settings.windowRect, (GUI.WindowFunction)DrawWindow, "Smokey Menu");
            }

            if (playerEspEnabled)
            {
                esp.DrawESPPlayers(Features.GetPlayers());
            }
            if (npcEspEnabled)
            {
                esp.DrawESPNPC(Features.GetNPCS());
            }
        }

        private void DrawWindow(int id)
        {
            GUILayout.BeginVertical();

            // Tab system
            if(isMainScene)
            {
                GUILayout.BeginHorizontal();
                for (int i = 0; i < tabNames.Length; i++)
                {
                    if (GUILayout.Button(tabNames[i]))
                    {
                        currentTab = i;
                    }
                }
                GUILayout.EndHorizontal();
            } else
            {
                GUILayout.Label("Load the game to get access to cheats.");
            }

                // Tab contents
                switch (currentTab)
                {
                    case 0:
                        DrawMoneyTab();
                        break;
                    case 1:
                        DrawXPTab();
                        break;
                    case 2:
                        DrawESPTab();
                        break;
                    case 3:
                        DrawTimeTab();
                        break;
                    case 4:
                        DrawMiscTab();
                        break;
                }

            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        private void DrawMoneyTab()
        {
                GUILayout.Label("Money options");

                if (GUILayout.Button("Add 100"))
                {
                    Features.GiveMoney(100);
                }

                if (GUILayout.Button("Add 1000"))
                {
                    Features.GiveMoney(1000);
                }

                if (GUILayout.Button("Add 10000"))
                {
                    Features.GiveMoney(10000);
                }

            if (GUILayout.Button("Add 100000"))
            {
                Features.GiveMoney(100000);
            }
        }

        private void DrawXPTab()
        {
                GUILayout.Label("XP options");

                if (GUILayout.Button("Add 100"))
                {
                    Features.GiveXP(100);
                }

                if (GUILayout.Button("Add 1000"))
                {
                    Features.GiveXP(1000);
                }

                if (GUILayout.Button("Add 10000"))
                {
                    Features.GiveXP(10000);
                }

            if (GUILayout.Button("Add 100000"))
            {
                Features.GiveXP(100000);
            }
        }

        private void DrawESPTab()
        {
            GUILayout.Label("ESP");
            playerEspEnabled = GUILayout.Toggle(playerEspEnabled, "Enable Player ESP");
            npcEspEnabled = GUILayout.Toggle(npcEspEnabled, "Enable NPC ESP");
        }

        private void DrawTimeTab()
        {
            GUILayout.Label("Time");

            GUILayout.Label($"Selected: {Mathf.RoundToInt(selectedTime)}h");

            selectedTime = GUILayout.HorizontalSlider(selectedTime, 0f, 24f);

            if (GUILayout.Button("Set Time"))
            {
                int selectedTimeInt = Mathf.RoundToInt(selectedTime);
                Features.SetTime(selectedTimeInt);
            }
        }

        private void DrawMiscTab()
        {
            GUILayout.Label("Miscellaneous");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(Settings.guiToggleKey))
            {
                Settings.showUI = !Settings.showUI;
                if(isMainScene)
                {
                    Cursor.visible = Settings.showUI;
                    if (Settings.showUI)
                    {
                        Cursor.lockState = CursorLockMode.None;
                    }
                    else
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                }
            }
        }
    }
}