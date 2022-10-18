using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Pawsome.SideGameplayTool
{
    public static class MoneyDisplayFormat
    {
        public enum CharInsert { Prepend, Append }

        private static string[] shortChars = new string[] { "k", "m", "b" };
        private const int PRUNING_LENGHT = 3;

        public static string Format(int value, CharInsert insertType = CharInsert.Append, bool upper = true)
        {
            int charID = Mathf.Clamp((value.ToString().Length - 1) / PRUNING_LENGHT - 1, -1, shortChars.Length);
            float updateValue = value / (charID == -1 ? 1 : Mathf.Pow(10, PRUNING_LENGHT * (charID + 1)));
            string shortChar = charID == -1 ? "" : upper ? shortChars[charID].ToUpper() : shortChars[charID].ToLower();
            string result = charID >= 0 ? String.Format("{0:0.0}", FloorValue(updateValue, 1)) : updateValue.ToString();
            return (insertType == CharInsert.Prepend ? shortChar : "") + result + (insertType == CharInsert.Append ? shortChar : "");
        }
        public static int Parse(string value)
        {
            value = value.ToLower();
            for (int i = 0; i < shortChars.Length; i++)
            {
                bool result = value.Contains(shortChars[i]);
                if (!result) continue;
                
                value = value.Replace(shortChars[i], "");
                int parsed = Mathf.CeilToInt((float.Parse(value, NumberStyles.Any) * Mathf.Pow(10, i + PRUNING_LENGHT)));
                return parsed;
            }
            return int.Parse(value);
        }

        private static float FloorValue(float value, int places)
        {
            float mult = Mathf.Pow(10, places);
            return Mathf.Floor(value * mult) / mult;
        }
    }
}