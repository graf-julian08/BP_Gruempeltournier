namespace BP_Gruempeltournier
{
    internal class Menu
    {
        internal static void CreateMenu()
        {

            bool gameState = true;

            while (gameState) {

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

            bool correctNumber = false;

                while (!correctNumber)
                {
                    Console.Write("\nBitte 1, 2, 3 oder 4 eingeben: ");
                    string menuInput = Console.ReadLine();

                    if (menuInput == "1" || menuInput == "2" || menuInput == "3" || menuInput == "4")
                    {
                        correctNumber = true;

                        if (menuInput == "1")
                        {

                            List<string> spielerListe = new List<string>();

                            Console.WriteLine("");
                            Console.WriteLine("       Spieler erfassen       ");
                            Console.WriteLine("------------------------------");
                            Console.Write("Vorname: ");
                            string spielerVorname = Console.ReadLine();
                            Console.Write("Name: ");
                            string spielerName = Console.ReadLine();
                            Console.Write("Geburtstag (tt.mm.jjjj): ");
                            string spielerGeburtstag = Console.ReadLine();
                            Console.Write("Wohnort: ");
                            string spielerWohnort = Console.ReadLine();

                            spielerListe.Add(spielerVorname + " " + spielerName + " " + spielerGeburtstag + " " + spielerWohnort);
                            spielerVorname = "";
                            spielerName = "";
                            spielerGeburtstag = "";
                            spielerWohnort = "";

                            Console.WriteLine(spielerListe.Count());


                        }
                        else if (menuInput == "2")
                        {
                            Console.WriteLine("");
                            Console.WriteLine("         Team erfassen        ");
                            Console.WriteLine("------------------------------");
                            Console.Write("Teamname: ");
                            string teamName = Console.ReadLine();
                            Console.WriteLine("Spieler (kommagetrennt): ");
                            string teamSpieler = Console.ReadLine();

                            Console.WriteLine(teamName + " " + teamSpieler);

                        }
                        else if (menuInput == "3")
                        {
                            Console.WriteLine("3");
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
    }
}

