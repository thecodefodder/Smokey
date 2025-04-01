using Il2CppScheduleOne.Money;
using Il2CppScheduleOne.NPCs;
using Il2CppScheduleOne.UI;
using Il2CppScheduleOne.GameTime;
using UnityEngine;
using Il2CppScheduleOne.PlayerScripts;
using Il2CppScheduleOne.Police;

namespace Smokey
{
    public class Features
    {
        public static bool undetectedbool = false;

        public static void GiveMoney(float moneyValue)
        {
            MoneyManager.Instance.ChangeCashBalance(moneyValue);
        }

        public static void GiveXP(int xpValue)
        {
            DailySummary.Instance.AddXP(xpValue);
        }

        public static void SetTime(float time)
        {
            TimeManager.Instance.SetTime((int)time * 100);
        }

        public static Il2CppSystem.Collections.Generic.List<Player> GetPlayers()
        {
            return Player.PlayerList;
        }

        public static Il2CppSystem.Collections.Generic.List<NPC> GetNPCS()
        {
            return NPCManager.NPCRegistry;
        }

        public static void Undetected()
        {
            if (undetectedbool)
            {
                Patches.ApplyInvestigatePatches();
            }
            else
            {
                Patches.RemoveInvestigatePatches();
            }
        }
    }
}
