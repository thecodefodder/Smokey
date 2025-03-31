using Il2CppToolBuddy.ThirdParty.VectorGraphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smokey
{
    public class Utils
    {
        public static void CheckMainScene(string sceneName, ref bool isMainScene, bool unloading)
        {
            if (sceneName == "Main" && unloading)
            {
                isMainScene = false;
            } else if (sceneName == "Main" && !unloading)
            {
                isMainScene = true;
            }
        }
    }
}
