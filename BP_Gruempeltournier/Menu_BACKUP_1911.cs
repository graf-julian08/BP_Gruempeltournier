using BP_Gruempeltournier.Data;
<<<<<<< HEAD
using BP_Gruempeltournier.Models;
using System.Globalization;

=======
>>>>>>> 8fa445c0e893725e7d55075dbb678b384b6f7dce
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

                bool validInput = false;
                string menuInput = "";

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

<<<<<<< HEAD
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Spiel beendet!");
                    Console.ForegroundColor = ConsoleColor.Black;
                    gameState = false;
                    
                }
=======
>>>>>>> 8fa445c0e893725e7d55075dbb678b384b6f7dce
            }
        }
    }
}
