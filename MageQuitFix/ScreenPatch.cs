using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MageQuitFix
{
    [HarmonyPatch(typeof(Screen), "resolutions", MethodType.Getter)]
    public static class Screen_get_resolutions
    {
        public static void Postfix(ref Resolution[] __result)
        {
            var resolutions = new List<Resolution>(__result);

            var customResolutions = new []
            {
                new Resolution { width = 2560, height = 1080, refreshRate = 60 },
                new Resolution { width = 2560, height = 1080, refreshRate = 120 },
                new Resolution { width = 2560, height = 1080, refreshRate = 144 },
                new Resolution { width = 2560, height = 1080, refreshRate = 240 },
                new Resolution { width = 3440, height = 1440, refreshRate = 60 },
                new Resolution { width = 3440, height = 1440, refreshRate = 120 },
                new Resolution { width = 3440, height = 1440, refreshRate = 144 },
                new Resolution { width = 3440, height = 1440, refreshRate = 240 },
                new Resolution { width = 3840, height = 1080, refreshRate = 60 },
                new Resolution { width = 3840, height = 1080, refreshRate = 120 },
                new Resolution { width = 3840, height = 1080, refreshRate = 144 },
                new Resolution { width = 3840, height = 1080, refreshRate = 240 },
                new Resolution { width = 5120, height = 1440, refreshRate = 60 },
                new Resolution { width = 5120, height = 1440, refreshRate = 120 },
                new Resolution { width = 5120, height = 1440, refreshRate = 144 },
                new Resolution { width = 5120, height = 1440, refreshRate = 240 },
            };

            foreach (var customResolution in customResolutions)
            {
                if (!resolutions.Any(x => x.width == customResolution.width && x.height == customResolution.height && x.refreshRate == customResolution.refreshRate))
                {
                    resolutions.Add(customResolution);
                }
            }

            __result = resolutions.ToArray();
        }
    }
}
