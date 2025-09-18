using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_Gruempeltournier
{
    internal class Spielplan
    {
        internal static void Generieren()
        {
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
                }
                else if (spieldauer < 5)
                {
                    Console.WriteLine("Die minimale Spielzeit beträgt 5 Minuten.");
                }
                else
                {
                    break;
                }
                Console.Write("Spieldauer in Minuten: ");
                if (!int.TryParse(Console.ReadLine(), out var pausenDauer))
                {
                    Console.WriteLine("Bitte einen Wert zwischen 1 und 15 eingeben!");
                    continue;
                }

                if (pausenDauer > 15)
                {
                    Console.WriteLine("Die maximale Pausenzeit beträgt 15 Minuten!");
                }
                else if (spieldauer < 1)
                {
                    Console.WriteLine("Die minimale Pausenzeit beträgt 1 Minute!");
                }
                else
                {
                    break;
                }
            }
        }
    }
}
