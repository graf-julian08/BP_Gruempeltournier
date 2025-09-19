using BP_Gruempeltournier.Data;
using BP_Gruempeltournier;

namespace BP_Gruempeltournier
{
    public class Menu
    {
        public static void CreateMenu(ref bool gameState, SpielerRepository spielerRepo, TeamRepository teamRepo)
        {
            Console.WriteLine("|            MENU            |");
            Console.WriteLine("| ---------------------------|");
            Console.WriteLine("| (1) Spieler erfassen       |");
            Console.WriteLine("|                            |");
            Console.WriteLine("| (2) Team erfassen          |");
            Console.WriteLine("|                            |");
            Console.WriteLine("| (3) Spielplan generieren   |");
            Console.WriteLine("|                            |");
            Console.WriteLine("| (4) Tournier beenden       |");
            Console.WriteLine("+----------------------------+");
            Console.WriteLine();

            string menuInput = "";
            bool validInput = false;

            while (!validInput)
            {
                Console.Write("Bitte 1, 2, 3 oder 4 eingeben: ");
                var input = Console.ReadLine();

                if (input is "1" or "2" or "3" or "4")
                {
                    menuInput = input!;
                    validInput = true;
                }
            }

            MenuPruefung.MenuPunkte(ref gameState, spielerRepo, teamRepo, ref validInput, ref menuInput);
        }
    }
}