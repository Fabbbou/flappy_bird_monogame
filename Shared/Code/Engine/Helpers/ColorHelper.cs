using Microsoft.Xna.Framework;

namespace Extensions
{
    public static class ColorHelper
    {
        public static Color FromHex(string hex)
        {
            // Remove the hash at the start if it's there
            hex = hex.Replace("#", "");

            byte r = 0, g = 0, b = 0, a = 255;

            if (hex.Length == 6)
            {
                r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            }
            else if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                r = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                g = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                b = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            }

            return new Color(r, g, b, a);
        }
    }
}
