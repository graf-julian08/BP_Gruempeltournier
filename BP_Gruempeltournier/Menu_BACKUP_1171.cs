using System.Globalization;
<<<<<<< HEAD
using BP_Gruempeltournier.Data;
using BP_Gruempeltournier.Models;
=======

using BP_Gruempeltournier.Data;
using BP_Gruempeltournier.Models;

>>>>>>> d80afb5fad395c3c494c3d06affe62dc9d364b33

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
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"Spieler erfasst!");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("");
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Fehler beim Speichern: {ex.Message}");
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                }
                else if (menuInput == "2")
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
                        continue;
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
                        continue;
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

                }
                else if (menuInput == "3")
                {
                    int teamCount = 0;
                    using (var con = Db.GetConnection())
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM dbo.Team;";
                        con.Open();
                        teamCount = (int)cmd.ExecuteScalar()!;
                    }

                    if (teamCount < 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("+----------------------------------+");
                        Console.WriteLine("| MINDESTENS ZWEI TEAMS ERSTELLEN! |");
                        Console.WriteLine("+----------------------------------+");
                        Console.WriteLine("");
                        Console.ForegroundColor = ConsoleColor.Black;
                        continue;
                    }

                    Spielplan.Generieren();
                    gameState = false;
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Spiel beendet!");
                    Console.ForegroundColor = ConsoleColor.Black;
                    gameState = false;
                }
            }
        }
    }
}
