using Raylib_cs;
using System;
using System.Numerics;
using static Raylib_cs.Raylib;

namespace afgc
{
    public class Defaults
    {

        internal static int _id = 0;
        internal static bool Autosize = false;
        internal static Color BackColor = Color.LIGHTGRAY;
        internal static float BlockSize = 4;
        internal static Color BorderColor = Color.DARKGRAY;
        internal static int BorderSize = 2;
        internal static float Camera2dRotation = 0.0f;
        internal static float Camera2dZoom = 1.0f;
        internal static float Camera2dZoomIncrement = 0.05f;
        internal static float Camera2dZoomMax = 20f;
        internal static float Camera2dZoomMin = 0.1f;
        internal static Color colAlphaGrey = new Color(200, 200, 200, 250);
        internal static Color colAlphaRed = new Color(255, 0, 0, 250);
        internal static Color colC64BackColor = new Color(73, 73, 193, 255);
        internal static Color colC64FrontColor = new Color(183, 183, 255, 255);
        internal static Color ControlFontColor = Color.RED;
        internal static int ControlFontSize = 20;
        internal static Font Font = GetFontDefault();
        internal static int FontSize = 20;
        internal static float FontSpacing = 2.00f;
        internal static int FPS = 120;
        internal static Vector2 GravityVector = new Vector2(0, 1f);
        internal static bool IsDebugRecVisible = false;
        internal static int[] KeyCanRepeat = new int[300];
        internal static int KeyRepeatThreshhold = 10;
        internal static int Margin = 1;
        internal static int Padding = 0;
        internal static Vector2 StopVector = new Vector2(0, 0);
        internal static int TextMaxChars = 255;
        internal static int WindowHeight = 900;
        internal static int WindowWidth = 1600;
        private static int _tick = 0;

        public static int ControlId { get { _id++; return _id; } }

        public static char Cursor()
        {
            char _ = DateTime.Now.Second % 2 != 0 ? '_' : ' ';
            return _;
        }
        public static bool IsNowOdd()
        {
            bool _ = DateTime.Now.Second % 2 != 0 ? true : false;
            return _;
        }
        public static bool IsTickOdd()
        {
            bool _ = _tick % 2 != 0 ? true : false;
            return _;
        }
        public static int Tick()
        {
            _tick++;
            return _tick;
        }
        internal static bool IsKeyCanRepeat(KeyboardKey key)
        {
            int k = (int)key;
            if (KeyCanRepeat[k] > KeyRepeatThreshhold) { KeyCanRepeat[k] = 0; } else { KeyCanRepeat[k]++; }
            return KeyCanRepeat[k] == 0 ? true : false;
        }

    }
}
