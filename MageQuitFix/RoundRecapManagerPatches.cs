using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;

namespace MageQuitFix
{
    [HarmonyPatch(typeof(RoundRecapManager), nameof(RoundRecapManager.Initialize))]
    public static class RoundRecapManager_Initialize
    {
        static ManualLogSource logSource = BepInEx.Logging.Logger.CreateLogSource("RoundRecapManager");

        public static void Prefix(bool ___initialized, out bool __state)
        {
            __state = ___initialized;
        }

        public static void Postfix(RoundRecapManager __instance, bool __state, Dictionary<int, RecapCard> ___playerCards)
        {
            if (__state)
            {
                return;
            }

            var heading = __instance.transform.Find("Panel").Find("Heading");
            heading.localScale = heading.localScale.WithX(heading.localScale.x * UltrawidePatches.AspectMultiplier);
            var title = __instance.transform.Find("Panel").Find("Title");
            title.localPosition = title.localPosition.WithX(title.localPosition.x + UltrawidePatches.Offset);
        }
    }
}
