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
            UI.BeginVertical();

            // Tab system
            if(isMainScene)
            {
                UI.BeginHorizontal();
                for (int i = 0; i < tabNames.Length; i++)
                {
                    if (UI.Button(tabNames[i]))
                    {
                        currentTab = i;
                    }
                }
                UI.EndHorizontal();
            } else
            {
                UI.Label("Load the game to get access to cheats.");
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

            UI.EndVertical();
            GUI.DragWindow();
        }

        private void DrawMoneyTab()
        {
                UI.Label("Money options");

                if (UI.Button("Add 100"))
                {
                    Features.GiveMoney(100);
                }

                if (UI.Button("Add 1000"))
                {
                    Features.GiveMoney(1000);
                }

                if (UI.Button("Add 10000"))
                {
                    Features.GiveMoney(10000);
                }

            if (UI.Button("Add 100000"))
            {
                Features.GiveMoney(100000);
            }
        }

        private void DrawXPTab()
        {
                UI.Label("XP options");

                if (UI.Button("Add 100"))
                {
                    Features.GiveXP(100);
                }

                if (UI.Button("Add 1000"))
                {
                    Features.GiveXP(1000);
                }

                if (UI.Button("Add 10000"))
                {
                    Features.GiveXP(10000);
                }

            if (UI.Button("Add 100000"))
            {
                Features.GiveXP(100000);
            }
        }

        private void DrawESPTab()
        {
            UI.Label("ESP");
            playerEspEnabled = UI.Toggle(playerEspEnabled, "Enable Player ESP");
            npcEspEnabled = UI.Toggle(npcEspEnabled, "Enable NPC ESP");
        }

        private void DrawTimeTab()
        {
            UI.Label("Time");

            UI.Label($"Selected: {Mathf.RoundToInt(selectedTime)}h");

            selectedTime = UI.Slider(selectedTime, 0f, 24f);

            if (UI.Button("Set Time"))
            {
                int selectedTimeInt = Mathf.RoundToInt(selectedTime);
                Features.SetTime(selectedTimeInt);
            }
        }

        private void DrawMiscTab()
        {
            UI.Label("Miscellaneous");
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