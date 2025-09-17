namespace BP_Gruempeltournier
{
    internal class Menu
    {
        internal static void CreateMenu()
        {
            Console.WriteLine("|            MENU            |");
            Console.WriteLine("| -------------------------- |");
            Console.WriteLine("| (1) Spieler erfassen       |");
            Console.WriteLine("|                            |");
            Console.WriteLine("| (2) Team erfassen          |");
            Console.WriteLine("|                            |");
            Console.WriteLine("| (3) Spielplan generieren   |");
            Console.WriteLine("+----------------------------+");

            bool correctNumber = false;

            while (!correctNumber)
            {
                Console.Write("\nBitte 1, 2 oder 3 eingeben: ");
                string menuInput = Console.ReadLine();

                if (menuInput == "1" || menuInput == "2" || menuInput == "3")
                {
                    correctNumber = true;
                    if (menuInput == "1")
                    {
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

                        Console.WriteLine(spielerVorname + " " + spielerName + " " + spielerGeburtstag + " " + spielerWohnort);
                    }
                    else if (menuInput == "2")
                    {
                        Console.WriteLine("2");
                    }
                    else
                    {
                        Console.WriteLine("3");
                    }
                }
            }
        }
    }
}

