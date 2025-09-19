using BP_Gruempeltournier.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_Gruempeltournier
{
    internal class MenuPruefung
    {
        public static void MenuPunkte(ref bool gameState, SpielerRepository spielerRepo, TeamRepository teamRepo, ref bool validInput, ref string menuInput)
        {
            while (!validInput)
            {
                Console.Write("\nBitte 1, 2, 3 oder 4 eingeben: ");
                var input = Console.ReadLine();

                if (input is "1" or "2" or "3" or "4")
                {
                    menuInput = input;
                    validInput = true;
                }
            }

            switch (menuInput)
            {
                case "1":
                    SpielerPruefung.ErfasseSpieler(spielerRepo);
                    break;

                case "2":
                    TeamPruefung.ErfasseTeam(spielerRepo, teamRepo);
                    break;

                case "3":
                    SpielplanPruefung.SpielplanPruefen(gameState);
                    break;

                case "4":
                    ConsoleHelper.WriteLineColored("Spiel beendet!", ConsoleColor.Blue);
                    gameState = false;
                    break;
            }
        }
    }
}
