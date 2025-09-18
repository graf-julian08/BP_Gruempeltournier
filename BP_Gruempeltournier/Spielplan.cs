namespace BP_Gruempeltournier
{
    internal class Spielplan
    {
        internal static void Generieren()
        {
            int spielNummer = 1;
            Console.WriteLine("");
            Console.WriteLine("      Spielplan erstellen     ");
            Console.WriteLine("------------------------------");

            while (true)
            {
                Console.Write("Spieldauer in Minuten: ");
                if (!int.TryParse(Console.ReadLine(), out var spieldauer))
                {
                    Console.WriteLine("Bitte einen Wert zwischen 5 und 90 eingeben.");
                    continue;
                }

                if (spieldauer > 90)
                {
                    Console.WriteLine("Die maximale Spielzeit beträgt 90 Minuten.");
                    continue;
                }
                else if (spieldauer < 5)
                {
                    Console.WriteLine("Die minimale Spielzeit beträgt 5 Minuten.");
                    continue;
                }
                break;
            }

            while (true)
            {
                Console.Write("Pausendauer in Minuten: ");
                if (!int.TryParse(Console.ReadLine(), out var pausenDauer))
                {
                    Console.WriteLine("Bitte einen Wert zwischen 1 und 15 eingeben!");
                    continue;
                }

                if (pausenDauer > 15)
                {
                    Console.WriteLine("Die maximale Pausenzeit beträgt 15 Minuten!");
                    continue;
                }
                else if (pausenDauer < 1)
                {
                    Console.WriteLine("Die minimale Pausenzeit beträgt 1 Minute!");
                    continue;
                }
                break;
            }

            while (true)
            {
                Console.Write("Spielbeginn (z.B. 8.00): ");
                if (!Double.TryParse(Console.ReadLine(), out var spielBeginn))
                {
                    Console.WriteLine("Bitte einen Wert zwischen 8.00 und 18.00 eingeben!");
                    continue;
                }

                spielBeginn = Math.Round(spielBeginn, 2);

                if (spielBeginn > 18.00)
                {
                    Console.WriteLine("Das letzte Spiel darf spätestens um 18:00 beginnen.");
                    continue;
                }
                else if (spielBeginn < 8.00)
                {
                    Console.WriteLine("Das Tournier beginnt erst um 8:00 Uhr");
                    continue;
                }
                break;
            }
            
            Console.WriteLine("Zwei Teams werden gezogen...");
            Thread.Sleep(2000);
        }
    }
}
