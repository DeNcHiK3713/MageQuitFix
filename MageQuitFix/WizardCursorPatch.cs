using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace MageQuitFix
{
    public static class WizardCursorPatch
    {
        private static FieldInfo sensitivityInfo = typeof(WizardCursor).GetField("sensitivity", BindingFlags.Instance | BindingFlags.NonPublic);

        public static void SetSensitivity(this WizardCursor _this, float value)
        {
            sensitivityInfo.SetValue(_this, value);
        }
    }


    [HarmonyPatch(typeof(WizardCursor), "Awake")]
    public static class WizardCursor_Awake
    {
        public static void Postfix(WizardCursor __instance)
        {
            __instance.SetSensitivity(Plugin.MouseSensitivityEntry.Value);
        }
    }
}
