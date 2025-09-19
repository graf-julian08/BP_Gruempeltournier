using BP_Gruempeltournier.Data;
using BP_Gruempeltournier.Models;
using System.Globalization;
namespace BP_Gruempeltournier
{
    internal class SpielerPruefung
    {
        public static void ErfasseSpieler(SpielerRepository spielerRepo)
        {
            Console.WriteLine("");
            Console.WriteLine("       Spieler erfassen       ");
            Console.WriteLine("------------------------------");

            Console.Write("Vorname: ");
            string vorname = Console.ReadLine()!.Trim();

            Console.Write("Name: ");
            string nachname = Console.ReadLine()!.Trim();

            DateOnly geburtstag;
            while (true)
            {
                Console.Write("Geburtstag (tt.mm.jjjj): ");
                var gebInput = Console.ReadLine();
                if (DateOnly.TryParseExact(gebInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out geburtstag))
                {
                    var heute = DateOnly.FromDateTime(DateTime.Today);
                    var alter = heute.Year - geburtstag.Year;
                    if (geburtstag > heute.AddYears(-alter)) alter--;

                    if (alter is >= 8 and <= 16) break;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nur 8–16 Jährige dürfen teilnehmen!");
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ungültiges Datum! Bitte im Format tt.mm.jjjj eingeben.");
                    Console.ForegroundColor = ConsoleColor.Black;
                }
            }

            Console.Write("Wohnort: ");
            string wohnort = Console.ReadLine()!.Trim();

            var s = new Spieler
            {
                Vorname = vorname,
                Nachname = nachname,
                Geburtstag = geburtstag,
                Wohnort = wohnort
            };

            try
            {
                int newId = spielerRepo.Insert(s);
                Console.WriteLine("------------------------------");
                ConsoleHelper.WriteLineColored("Spieler erfasst!", ConsoleColor.DarkGreen);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fehler beim Speichern: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }
    }
}
