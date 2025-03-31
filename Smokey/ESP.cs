using Il2CppScheduleOne.NPCs;
using Il2CppScheduleOne.PlayerScripts;
using UnityEngine;

namespace Smokey
{
    class ESP
    {
        public void DrawESPPlayers(Il2CppSystem.Collections.Generic.List<Player> players)
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
                        float distance = Vector3.Distance(mainCamera.transform.position, player.transform.position);
                        float boxHeight = Mathf.Clamp(200f / distance, 50f, 200f);
                        float boxWidth = Mathf.Clamp(80f / distance, 40f, 80f);

                        // Convert world position to screen position
                        Vector2 boxPos = new Vector2(screenPos.x - boxWidth / 2, Screen.height - screenPos.y - boxHeight / 2);

                        // Draw ESP Box
                        DrawBox(boxPos, boxWidth, boxHeight, Color.red);

                        // Draw Player Name
                        GUI.Label(new Rect(boxPos.x, boxPos.y - 20, 100, 20), player.name);
                        GUI.Label(new Rect(boxPos.x, boxPos.y - 10, 100, 20), distance.ToString());
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.Log($"ESP Error: {ex.Message}");
                }
            }
        }

        public void DrawESPNPC(Il2CppSystem.Collections.Generic.List<NPC> npcs)
        {
            Camera mainCamera = Camera.main;
            if (npcs == null || npcs.Count == 0 || mainCamera == null)
                return;

            foreach (NPC npc in npcs)
            {
                if (npc == null || npc.transform == null || npc.isInBuilding)
                    continue;

                try
                {
                    Vector3 npcPos = npc.transform.position;
                    Vector3 screenPos = mainCamera.WorldToScreenPoint(npcPos);

                    if (screenPos.z > 0)
                    {
                        float distance = Vector3.Distance(mainCamera.transform.position, npc.transform.position);
                        float boxHeight = Mathf.Clamp(200f / distance, 50f, 200f);
                        float boxWidth = Mathf.Clamp(80f / distance, 40f, 80f);

                        // Convert world position to screen position
                        Vector2 boxPos = new Vector2(screenPos.x - boxWidth / 2, Screen.height - screenPos.y - boxHeight / 2);

                        // Draw ESP Box
                        DrawBox(boxPos, boxWidth, boxHeight, Color.red);

                        // Draw Player Name
                        GUI.Label(new Rect(boxPos.x, boxPos.y - 20, 100, 20), npc.name);
                        GUI.Label(new Rect(boxPos.x, boxPos.y - 10, 100, 20), distance.ToString());
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
    }
}
