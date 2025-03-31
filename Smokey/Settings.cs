using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Smokey
{
    public static class Settings
    {
        public static KeyCode guiToggleKey = KeyCode.Insert;
        public static bool showUI = false;
        public static Rect windowRect = new Rect(20, 20, 300, 400);
        public static int windowID = 1001;
    }
}
