using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using BP_Gruempeltournier.Data;

namespace BP_Gruempeltournier
{
    internal class Spielplan
    {
        static TimeSpan RoundUpTo5(TimeSpan t)
        {
            var totalMinutesRounded = (int)Math.Ceiling(t.TotalMinutes / 5.0) * 5;
            return TimeSpan.FromMinutes(totalMinutesRounded);
        }

        internal static void Generieren()
        {
            int spielNummer = 1;
            Console.WriteLine("");
            Console.WriteLine("      Spielplan erstellen     ");
            Console.WriteLine("------------------------------");

            int spieldauer;
            while (true)
            {
                Console.Write("Spieldauer in Minuten: ");
                if (!int.TryParse(Console.ReadLine(), out spieldauer))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bitte einen Wert zwischen 5 und 90 eingeben.");
                    Console.ForegroundColor = ConsoleColor.Black;
                    continue;
                }
                if (spieldauer > 90)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Die maximale Spielzeit beträgt 90 Minuten.");
                    Console.ForegroundColor = ConsoleColor.Black;
                    continue;
                }
                else if (spieldauer < 5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Die minimale Spielzeit beträgt 5 Minuten.");
                    Console.ForegroundColor = ConsoleColor.Black;
                    continue;
                }
                break;
            }

            int pausenDauer;
            while (true)
            {
                Console.Write("Pausendauer in Minuten: ");
                if (!int.TryParse(Console.ReadLine(), out pausenDauer))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bitte einen Wert zwischen 1 und 15 eingeben!");
                    Console.ForegroundColor = ConsoleColor.Black;
                    continue;
                }
                if (pausenDauer > 15)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Die maximale Pausenzeit beträgt 15 Minuten!");
                    Console.ForegroundColor = ConsoleColor.Black;
                    continue;
                }
                else if (pausenDauer < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Die minimale Pausenzeit beträgt 1 Minute!");
                    Console.ForegroundColor = ConsoleColor.Black;
                    continue;
                }
                break;
            }

            double spielBeginnDouble;
            while (true)
            {
                Console.Write("Tournierbeginn (z.B. 8.00): ");
                if (!Double.TryParse(Console.ReadLine(), out spielBeginnDouble))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bitte einen Wert zwischen 8.00 und 14.00 eingeben!");
                    Console.ForegroundColor = ConsoleColor.Black;
                    continue;
                }
                spielBeginnDouble = Math.Round(spielBeginnDouble, 2);
                if (spielBeginnDouble > 14.00)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Das Tournier darf spätestens um 14:00 beginnen.");
                    Console.ForegroundColor = ConsoleColor.Black;
                    continue;
                }
                else if (spielBeginnDouble < 8.00)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Das Tournier darf frühestens um 8:00 Uhr beginnen.");
                    Console.ForegroundColor = ConsoleColor.Black;
                    continue;
                }
                break;
            }

            var teamRepo = new TeamRepository();
            var planRepo = new SpielplanRepository();
            var teams = teamRepo.GetAllWithSpieler();

            var teamNameById = teams.ToDictionary(t => t.TeamID, t => t.Teamname);

            var paarungen = new List<(int t1, int t2)>();
            for (int i = 0; i < teams.Count; i++)
                for (int j = i + 1; j < teams.Count; j++)
                    paarungen.Add((teams[i].TeamID, teams[j].TeamID));

            var rnd = new Random();
            paarungen = paarungen.OrderBy(_ => rnd.Next()).ToList();

            var interval = TimeSpan.FromMinutes(spieldauer + pausenDauer + 15);
            var ersterStart = RoundUpTo5(TimeSpan.FromHours(spielBeginnDouble));
            var basisDatum = DateTime.Today;

            var slots = new List<(TimeSpan start, HashSet<int> belegteTeams, List<(int t1, int t2)> spiele)>();
            slots.Add((ersterStart, new HashSet<int>(), new List<(int, int)>()));

            foreach (var p in paarungen)
            {
                bool platziert = false;
                foreach (var idx in Enumerable.Range(0, slots.Count))
                {
                    var slot = slots[idx];
                    if (slot.spiele.Count >= 3) continue;
                    if (slot.belegteTeams.Contains(p.t1) || slot.belegteTeams.Contains(p.t2)) continue;
                    slot.spiele.Add(p);
                    slot.belegteTeams.Add(p.t1);
                    slot.belegteTeams.Add(p.t2);
                    slots[idx] = slot;
                    platziert = true;
                    break;
                }
                if (!platziert)
                {
                    var neuerStart = RoundUpTo5(slots[^1].start + interval);
                    slots.Add((neuerStart, new HashSet<int>(new[] { p.t1, p.t2 }), new List<(int, int)> { p }));
                }
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Spielplan wird generiert...");
            Console.ForegroundColor = ConsoleColor.Black;
            Thread.Sleep(2000);

            foreach (var slot in slots)
            {
                foreach (var match in slot.spiele)
                {
                    planRepo.Insert(match.t1, match.t2, spieldauer, pausenDauer, basisDatum + slot.start);
                }
            }

            foreach (var slot in slots)
            {
                Console.WriteLine("");
                foreach (var match in slot.spiele)
                {
                    var start = basisDatum + slot.start;
                    var t1Name = teamNameById.TryGetValue(match.t1, out var n1) ? n1 : $"Team {match.t1}";
                    var t2Name = teamNameById.TryGetValue(match.t2, out var n2) ? n2 : $"Team {match.t2}";
                    Console.WriteLine($"Spiel {spielNummer:00}: {t1Name} vs. {t2Name}, Start: {start:HH\\:mm}, Spieldauer: {spieldauer} min, Pause: {pausenDauer} min");

                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");
                    spielNummer++;
                }
            }

            while (true)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("(1) Beenden und Spielplan zurücksetzen");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("(2) Beenden und Spielplan behalten");
                Console.Write("Auswahl: ");
                var choice = Console.ReadLine()?.Trim();
                if (choice == "1")
                {
                    try { planRepo.DeleteAll(); } catch { }
                    Environment.Exit(0);
                }
                else if (choice == "2")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bitte 1 oder 2 eingeben.");
                    Console.ForegroundColor = ConsoleColor.Black;
                }
            }
        }
    }
}