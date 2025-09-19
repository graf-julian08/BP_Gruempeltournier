using System;

namespace BP_Gruempeltournier
{
    internal static class ConsoleHelper
    {
        private static readonly ConsoleColor defaultBackground;
        private static readonly ConsoleColor defaultForeground;

        static ConsoleHelper()
        {
            defaultBackground = Console.BackgroundColor;
            defaultForeground = Console.ForegroundColor;
        }

        public static void SetProgramColors()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
        }

        public static void WriteLineColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);

            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static void ResetToDefault()
        {
            Console.BackgroundColor = defaultBackground;
            Console.ForegroundColor = defaultForeground;
            Console.Clear();
        }
    }
}