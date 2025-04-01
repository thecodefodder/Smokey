using Il2CppScheduleOne.Police;
using Il2CppScheduleOne.PlayerScripts;

namespace Smokey
{
    public class Patches
    {
        private static HarmonyLib.Harmony _harmony;

        public static void ApplyInvestigatePatches()
        {
            if(_harmony == null)
            {
                _harmony = new HarmonyLib.Harmony("com.smokey.patches");
                _harmony.PatchAll(typeof(Patches));
            }
        }

        public static void RemoveInvestigatePatches()
        {
            if (_harmony != null)
            {
                _harmony.Unpatch(
                    HarmonyLib.AccessTools.Method(typeof(PoliceOfficer), nameof(PoliceOfficer.CanInvestigate)),
                    HarmonyLib.HarmonyPatchType.All,
                    "com.smokey.patches"
                );
                _harmony.Unpatch(
                    HarmonyLib.AccessTools.Method(typeof(PoliceOfficer), nameof(PoliceOfficer.CanInvestigatePlayer)),
                    HarmonyLib.HarmonyPatchType.All,
                    "com.smokey.patches"
                );
            }
        }
    }
}

[HarmonyLib.HarmonyPatch(typeof(PoliceOfficer))]
[HarmonyLib.HarmonyPatch(nameof(PoliceOfficer.CanInvestigate))]
class Patch_CanInvestigate
{
    static bool Prefix(ref bool __result)
    {
        __result = true;
        return false;
    }
}

[HarmonyLib.HarmonyPatch(typeof(PoliceOfficer))]
[HarmonyLib.HarmonyPatch(nameof(PoliceOfficer.CanInvestigatePlayer))]
class Patch_CanInvestigatePlayer
{
    static bool Prefix(ref bool __result)
    {
        __result = true;
        return false;
    }
}