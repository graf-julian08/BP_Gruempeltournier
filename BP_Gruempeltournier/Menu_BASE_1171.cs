using System.Globalization;
<<<<<<< HEAD
using System.Data;
using BP_Gruempeltournier.Data;
using BP_Gruempeltournier.Models;
=======
>>>>>>> 8250d94de2ad969997d17683bda19f5a51cf4794

namespace BP_Gruempeltournier
{
    internal class Menu
    {
        internal static void CreateMenu()
        {
            bool gameState = true;
            var spielerRepo = new SpielerRepository();
            var teamRepo = new TeamRepository();

            while (gameState)
            {
                Console.WriteLine("|            MENU            |");
                Console.WriteLine("| -------------------------- |");
                Console.WriteLine("| (1) Spieler erfassen       |");
                Console.WriteLine("|                            |");
                Console.WriteLine("| (2) Team erfassen          |");
                Console.WriteLine("|                            |");
                Console.WriteLine("| (3) Spielplan generieren   |");
                Console.WriteLine("|                            |");
                Console.WriteLine("| (4) Tournier beenden       |");
                Console.WriteLine("+----------------------------+");

                string? menuInput = null;
                while (menuInput is null)
                {
                    Console.Write("\nBitte 1, 2, 3 oder 4 eingeben: ");
                    var input = Console.ReadLine();
                    if (input is "1" or "2" or "3" or "4")
                        menuInput = input;
                }

                if (menuInput == "1")
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
                            Console.WriteLine("Nur 8–16 Jährige dürfen teilnehmen!");
                        }
                        else
                        {
                            Console.WriteLine("Ungültiges Datum! Bitte im Format tt.mm.jjjj eingeben.");
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
                        Console.WriteLine($"Spieler erfasst!");
                        Console.WriteLine("");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Fehler beim Speichern: {ex.Message}");
                    }
                }
                else if (menuInput == "2")
                {
                    var alleSpieler = spielerRepo.GetAll();
                    if (alleSpieler.Count == 0)
                    {
                        Console.WriteLine("+----------------------------+");
                        Console.WriteLine("|  BITTE SPIELER HINZUFÜGEN  |");
                        Console.WriteLine("+----------------------------+");
                        Console.WriteLine("");
                        continue;
                    }

                    Console.WriteLine("");
                    Console.WriteLine("         Team erfassen        ");
                    Console.WriteLine("------------------------------");
                    Console.Write("Teamname: ");
                    string teamName = Console.ReadLine()!.Trim();

                    Console.WriteLine("");
                    Console.WriteLine("Verfügbare Spieler: ");
                    Console.WriteLine("------------------------------");
                    foreach (var s in alleSpieler)
                    Console.WriteLine($"{s.SpielerID}: {s.Vorname} {s.Nachname} ({s.Geburtstag:dd.MM.yyyy})");
                    Console.WriteLine("------------------------------");

                    Console.WriteLine("\nSpielerID(s) eingeben (kommagetrennt): ");

                    int teamId;
                    try
                    {
                        teamId = new TeamRepository().InsertTeam(teamName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Fehler beim Anlegen des Teams: {ex.Message}");
                        continue;
                    }

                    while (true)
                    {
                        var eingabe = Console.ReadLine() ?? string.Empty;
                        var idsRaw = eingabe.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                        if (idsRaw.Length == 0)
                        {
                            Console.WriteLine("Mindestens eine SpielerID eingeben:");
                            continue;
                        }

                        var ids = new List<int>();
                        bool ok = true;

                        foreach (var token in idsRaw)
                        {
                            if (!int.TryParse(token, out var id))
                            {
                                Console.WriteLine($"'{token}' ist keine gültige ID. Bitte erneut eingeben.");
                                ok = false; break;
                            }
                            if (!spielerRepo.Exists(id))
                            {
                                Console.WriteLine($"Diese ID ist ungültig. Bitte erneut eingeben.");
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
                            Console.WriteLine("Team erfasst!");
                            Console.WriteLine("");
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Fehler: {ex.Message}");
                            Console.WriteLine("Bitte IDs erneut eingeben:");
                        }
                    }
                }
                else if (menuInput == "3")
                {
                    Spielplan.Generieren();
                    gameState = false;
                }
                else
                {
                    Console.WriteLine("Spiel beendet!");
                    gameState = false;
                }
            }
        }
    }
}
