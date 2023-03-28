using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace MageQuitFix
{
    [HarmonyPatch(typeof(RecapCard), nameof(RecapCard.UndockCard))]
    public static class RecapCard_UndockCard
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newInstructions = new List<CodeInstruction>(instructions);
            int index = newInstructions.FindIndex(x => x.opcode == OpCodes.Ldc_R4 && (float)x.operand == -420f);
            if (index > 0)
            {
                index++;
                newInstructions.Insert(index++, Transpilers.EmitDelegate<Func<float, float>>(x => x + UltrawidePatches.Offset));
            }
            return newInstructions;
        }
    }


    [HarmonyPatch(typeof(RecapCard), nameof(RecapCard.DockCard))]
    public static class RecapCard_DockCard
    {
        static FieldInfo dockLocationInfo = typeof(RecapCard).GetField("dockLocation", BindingFlags.Public | BindingFlags.Instance);

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newInstructions = new List<CodeInstruction>(instructions);
            int index = newInstructions.FindIndex(x => x.opcode == OpCodes.Ldfld && (x.operand as FieldInfo) == dockLocationInfo);
            if (index > 0)
            {
                index++;
                newInstructions.Insert(index++, Transpilers.EmitDelegate<Func<Vector3, Vector3>>(dockLocation => dockLocation.WithX(dockLocation.x + UltrawidePatches.Offset)));
            }
            return newInstructions;
        }
    }
}