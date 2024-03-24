using System.Text.RegularExpressions;

namespace PetMan.Framework
{
    public static class Extension
    {
        public static int ToInt(string value) 
        {
            return int.Parse(value);
        }
        public static bool ToInt(string value, out int i)
        {
            return int.TryParse(value, out i);
        }
        public static bool ToBool(string value)
        {
            value = value.ToLower();
            if (value == "y" || value == "true") return true;
            return false;
        }
        public static string ToString(bool value, string format)
        {
            if (format == "y/n")
                return value ? "Yes" : "No";
            if (format == "c/k")
                return value ? "Có" : "Không";
            return value ? "True" : "False";
        }
    }
}