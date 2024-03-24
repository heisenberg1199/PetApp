namespace PetMan.Framework
{
    public static class ViewHelp
    {


        public static int InputInt(string label, int oldValue, ConsoleColor labelColor = ConsoleColor.Magenta, ConsoleColor valueColor = ConsoleColor.White)
        {
            
            Write($"{label}:", labelColor);
            WriteLine($"{oldValue}");
            Write($"New value ({label}) >>:", ConsoleColor.Yellow);
            Console.ForegroundColor = valueColor;
            string newValue = Console.ReadLine() ?? "";
            if (string.IsNullOrEmpty(newValue)) return oldValue;
            if (Extension.ToInt(newValue, out int i)) return i;
            return oldValue;
        }
        public static bool InputBool(string label, bool oldValue, ConsoleColor labelColor = ConsoleColor.Magenta, ConsoleColor valueColor = ConsoleColor.White)
        {
            Write($"{label}:", labelColor);
            WriteLine(Extension.ToString(oldValue, "y/n"), ConsoleColor.Yellow);
            Write($"New value ({label}) >>:", ConsoleColor.Yellow);
            Console.ForegroundColor = valueColor;
            string newValue = Console.ReadLine() ?? "";
            if (string.IsNullOrEmpty(newValue)) return oldValue;
            return Extension.ToBool(newValue);
        }
        public static string InputString(string label, string oldValue, ConsoleColor labelColor = ConsoleColor.Magenta, ConsoleColor valueColor = ConsoleColor.White)
        {
            Write($"{label}:", labelColor);
            WriteLine(oldValue);
            Console.ForegroundColor = valueColor;
            Write($"New value ({label}) >>:", ConsoleColor.Yellow);
            string newValue = Console.ReadLine() ?? "";
            return string.IsNullOrEmpty(newValue.Trim()) ? oldValue : newValue;
        }

        public static void WriteLine(string text, ConsoleColor color = ConsoleColor.Magenta, bool resetColor = true)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            if (resetColor) Console.ResetColor();
        }
        public static void Write(string text, ConsoleColor color = ConsoleColor.Magenta, bool resetColor = true)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            if (resetColor) Console.ResetColor();
        }
    }
}