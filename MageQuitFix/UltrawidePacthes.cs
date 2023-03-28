using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MageQuitFix
{
    [HarmonyPatch]
    public static class UltrawidePatches
    {
        public static float DefaultAspectRatio = (float)16 / 9;
        public static float NewAspectRatio => (float)Screen.width / Screen.height;
        public static float AspectMultiplier => NewAspectRatio / DefaultAspectRatio;
        public static float AspectDivider => DefaultAspectRatio / NewAspectRatio;
        public static float Offset => 800 * (1 + AspectMultiplier) * (1 - AspectMultiplier) * 0.25f; // 800 is default referenceResolution width


        [HarmonyPatch(typeof(CanvasScaler), "OnEnable")]
        [HarmonyPostfix]
        public static void CanvasScaler_OnEnable(CanvasScaler __instance)
        {
            if (NewAspectRatio > DefaultAspectRatio || NewAspectRatio < DefaultAspectRatio)
            {
                __instance.referenceResolution = __instance.referenceResolution.WithX(__instance.referenceResolution.x * AspectMultiplier);
            }
        }

        [HarmonyPatch(typeof(CameraContain), "Awake")]
        [HarmonyPostfix]
        public static void CameraContain_Awake(CameraContain __instance)
        {
            if (NewAspectRatio > DefaultAspectRatio || NewAspectRatio < DefaultAspectRatio)
            {
                __instance.paddingMultiplier *= AspectMultiplier;
            }
        }
    }
}
