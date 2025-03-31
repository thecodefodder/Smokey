using Il2CppScheduleOne.GameTime;
using Il2CppScheduleOne.Levelling;
using Il2CppScheduleOne.Money;
using Il2CppScheduleOne.NPCs;
using Il2CppScheduleOne.Persistence.Datas;
using Il2CppScheduleOne.PlayerScripts;
using Il2CppScheduleOne.UI;
using Il2CppSystem;
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

        private bool playerEspEnabled = false;
        private bool npcEspEnabled = false;
        private static float selectedTime = 1;
        private bool isMainScene = false;

        ESP esp = new ESP();

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            base.OnSceneWasLoaded(buildIndex, sceneName);
            if(sceneName == "Main")
            {
                isMainScene = true;
            } else
            {
                isMainScene = false;
            }
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            base.OnSceneWasUnloaded(buildIndex, sceneName);

            if (sceneName == "Main")
            {
                isMainScene = false;
            }
        }

        public override void OnGUI()
        {
            if (showUI)
            {
                GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);

                windowRect = GUI.Window(windowID, windowRect, (GUI.WindowFunction)DrawWindow, "Smokey Menu");
            }

            if(playerEspEnabled)
            {
                esp.DrawESPPlayers(GetPlayers());
            }
            if (npcEspEnabled)
            {
                esp.DrawESPNPC(GetNPCS());
            }
        }

        private void DrawWindow(int id)
        {
            GUILayout.BeginVertical();

            if (isMainScene)
            {
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

                #region Money Options
                GUILayout.Label("XP options");

                if (GUILayout.Button("Add 100"))
                {
                    GiveXP(100);
                }

                if (GUILayout.Button("Add 1000"))
                {
                    GiveXP(1000);
                }

                if (GUILayout.Button("Add 10000"))
                {
                    GiveXP(10000);
                }

                if (GUILayout.Button("Add 100000"))
                {
                    GiveXP(100000);
                }
                #endregion

                #region ESP
                GUILayout.Label("ESP");
                playerEspEnabled = GUILayout.Toggle(playerEspEnabled, "Enable Player ESP");
                npcEspEnabled = GUILayout.Toggle(npcEspEnabled, "Enable NPC ESP");
                #endregion

                #region Time
                GUILayout.Label("Time");
                GUILayout.Label($"Selected: {selectedTime.ToString()}");
                selectedTime = GUILayout.HorizontalSlider(selectedTime, 0f, 24f);
                if (GUILayout.Button("Set Time"))
                {
                    SetTime((int)selectedTime);
                }
                #endregion
            } else
            {
                GUILayout.Label("Load the game to get the sweet cheats!");
            }
                GUILayout.EndVertical();

            GUI.DragWindow();
        }

        //private static Player GetLocalPlayer()
        //{
        //    Player player = null;

        //    foreach (Player index in GetPlayers())
        //    {
        //        if (index.IsLocalPlayer)
        //        {
        //            player = index;
        //            break;
        //        }
        //        continue;
        //    }

        //    return player!;
        //}

        private void GiveMoney(float moneyValue)
        {
            MoneyManager.Instance.ChangeCashBalance(moneyValue);
        }

        private void GiveXP(int xpValue)
        {
            DailySummary.Instance.AddXP(xpValue);
        }

        private void SetTime(int time)
        {
            TimeManager.Instance.SetTime(Mathf.Clamp(time, 1, 24));
        }

        private static Il2CppSystem.Collections.Generic.List<Player> GetPlayers()
        {
            return Player.PlayerList;
        }

        private static Il2CppSystem.Collections.Generic.List<NPC> GetNPCS()
        {
            return NPCManager.NPCRegistry;
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