using Il2CppScheduleOne.Money;
using Il2CppScheduleOne.NPCs;
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

        private bool playerEspEnabled = false;
        private bool npcEspEnabled = false;
        private int selectedPlayer = 0;
        private static Camera? localPlayerCamera;

        ESP esp = new ESP();

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            base.OnSceneWasLoaded(buildIndex, sceneName);
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

            #region ESP
            GUILayout.Label("ESP");
            playerEspEnabled = GUILayout.Toggle(playerEspEnabled, "Enable Player ESP");
            npcEspEnabled = GUILayout.Toggle(npcEspEnabled, "Enable NPC ESP");
            #endregion
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

        private void GiveMoney(int moneyValue)
        {
            MoneyManager.Instance.ChangeCashBalance(moneyValue);
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