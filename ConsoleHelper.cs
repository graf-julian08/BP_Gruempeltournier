namespace BP_Gruempeltournier
{
    internal static class ConsoleHelper
    {
        private static readonly ConsoleColor defaultBackground;
        private static readonly ConsoleColor defaultForeground;

        // Standardfarben beim Programmstart merken
        static ConsoleHelper()
        {
            defaultBackground = Console.BackgroundColor;
            defaultForeground = Console.ForegroundColor;
        }

        // Am Anfang: Hintergrund = Weiß, Schrift = Schwarz
        public static void SetProgramColors()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
        }

        // Für Kommentare: farbige Ausgabe, dann zurück auf Schwarz
        public static void WriteLineColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);

            Console.ForegroundColor = ConsoleColor.Black;
        }

        // Am Ende: Terminal wieder auf ursprüngliche Default-Farben setzen
        public static void ResetToDefault()
        {
            Console.BackgroundColor = defaultBackground;
            Console.ForegroundColor = defaultForeground;
            Console.Clear();
        }
    }
}