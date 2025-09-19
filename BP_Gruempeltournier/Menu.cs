using BP_Gruempeltournier.Data;
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

                MenuPruefung.MenuPunkte(ref gameState, spielerRepo, teamRepo, ref validInput, ref menuInput);
            }
        }
    }
}
