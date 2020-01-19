﻿using System;
using System.Globalization;
using UnityEngine;

namespace JimmysUnityUtilities
{
    /// <summary>
    /// Like UnityEngine.Color32 but without a transparency byte.
    /// </summary>
    [Serializable]
    public partial struct Color24
    {
        public byte r;
        public byte g;
        public byte b;

        public Color24(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public Color24(int hex)
        {
            this.r = (byte)((hex & 0xFF0000) >> 16);
            this.g = (byte)((hex & 0xFF00) >> 8);
            this.b = (byte)(hex & 0xFF);
        }

        public Color32 WithOpacity(byte opacity = byte.MaxValue)
            => new Color32(r, g, b, opacity);


        public override string ToString() 
            => $"#{r:X2}{g:X2}{b:X2}";

        /// <summary>
        /// Text can be a hex code or a color name (see Color24.AllNamedColors). 
        /// Hex codes can be preceded with a # or not. 
        /// Color names are case-insensitive.
        /// Spaces in color names are ignored.
        /// </summary>
        public static bool TryParse(string text, out Color24 value)
        {
            text = text.Replace(" ", "");

            if (AllNamedColors.TryGetValue(text, out value))
                return true;

            text = text.TrimStart('#');
            value = default;

            if (text.Length != 6)
                return false;

            if (Int32.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int hexNumber))
            {
                value = new Color24(hexNumber);
                return true;
            }

            return false;
        }
    }
}
