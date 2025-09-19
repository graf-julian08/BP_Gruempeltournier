using BP_Gruempeltournier.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_Gruempeltournier
{
    internal class TeamPruefung
    {
        public static bool ErfasseTeam(SpielerRepository spielerRepo, TeamRepository teamRepo)
        {
            var alleSpieler = spielerRepo.GetAll();
            if (alleSpieler.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("+----------------------------+");
                Console.WriteLine("|  BITTE SPIELER HINZUFÜGEN  |");
                Console.WriteLine("+----------------------------+");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Black;
                return false;
            }

            Console.WriteLine("");
            Console.WriteLine("         Team erfassen        ");
            Console.WriteLine("------------------------------");

            string teamName;

            while (true)
            {
                Console.Write("Teamname: ");
                teamName = Console.ReadLine()?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(teamName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bitte einen Teamnamen eingeben.");
                    Console.ForegroundColor = ConsoleColor.Black;
                    continue;
                }

                if (teamRepo.ExistsTeamname(teamName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Name bereits vorhanden. Bitte einen neuen eingeben.");
                    Console.ForegroundColor = ConsoleColor.Black;
                    continue;
                }

                break;
            }

            Console.WriteLine("");
            Console.WriteLine("Verfügbare Spieler: ");
            Console.WriteLine("------------------------------");
            foreach (var s in alleSpieler)
                Console.WriteLine($"{s.SpielerID} {s.Vorname} {s.Nachname} ({s.Geburtstag:dd.MM.yyyy})");
            Console.WriteLine("------------------------------");

            Console.WriteLine("\nSpielerID eingeben (kommagetrennt): ");

            int teamId;
            try
            {
                teamId = teamRepo.InsertTeam(teamName);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fehler beim Anlegen des Teams: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.Black;
                return false;
            }

            while (true)
            {
                var eingabe = Console.ReadLine() ?? string.Empty;
                var idsRaw = eingabe.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                if (idsRaw.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Mindestens eine SpielerID eingeben:");
                    Console.ForegroundColor = ConsoleColor.Black;
                    continue;
                }

                var ids = new List<int>();
                bool ok = true;

                foreach (var token in idsRaw)
                {
                    if (!int.TryParse(token, out var id))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Bitte eine gültige SpielerID eingeben.");
                        Console.ForegroundColor = ConsoleColor.Black;
                        ok = false; break;
                    }
                    if (!spielerRepo.Exists(id))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Diese ID ist ungültig. Bitte erneut eingeben.");
                        Console.ForegroundColor = ConsoleColor.Black;
                        ok = false; break;
                    }
                    ids.Add(id);
                }

                if (!ok) continue;

                try
                {
                    foreach (var id in ids)
                        teamRepo.AddSpielerZuTeam(teamId, id);

                    Console.WriteLine("------------------------------");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Team erfasst!");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("");
                    break;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{ex.Message} Bitte erneut angeben.");
                    Console.ForegroundColor = ConsoleColor.Black;
                }
            }

            return true;
        }
    }
}
