using UnityEngine;

namespace Smokey
{
    public static class UI
    {
        public static void Label(string text)
        {
            GUILayout.Label(text);
        }

        public static bool Button(string text)
        {
            return GUILayout.Button(text);
        }

        public static bool Toggle(bool value, string text)
        {
            return GUILayout.Toggle(value, text);
        }

        public static float Slider(float value, float min, float max)
        {
            return GUILayout.HorizontalSlider(value, min, max);
        }

        public static void BeginVertical()
        {
            GUILayout.BeginVertical();
        }

        public static void EndVertical()
        {
            GUILayout.EndVertical();
        }

        public static void BeginHorizontal()
        {
            GUILayout.BeginHorizontal();
        }

        public static void EndHorizontal()
        {
            GUILayout.EndHorizontal();
        }

        public static void Window(int id, Rect rect, GUI.WindowFunction func, string title)
        {
            GUI.Window(id, rect, func, title);
        }

        public static void DragWindow()
        {
            GUI.DragWindow();
        }
    }
}
