using System.Globalization;

namespace BP_Gruempeltournier
{
    internal class Menu
    {
        internal static void CreateMenu()
        {
            // Variabeln
            bool gameState = true;
            List<string> spielerListe = new List<string>();
            int spielerID = 1;

            List<string> teamListe = new List<string>();
            int teamID = 1;

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
                            Console.WriteLine("");
                            Console.WriteLine("       Spieler erfassen       ");
                            Console.WriteLine("------------------------------");
                            Console.Write("Vorname: ");
                            string spielerVorname = Console.ReadLine();
                            Console.Write("Name: ");
                            string spielerName = Console.ReadLine();

                            // Die Überprüfung des Geburtstages wurde mit Hilfe von KI erstellt, wir haben den Code jedoch durchgelesen und verstanden
                            DateOnly spielerGeburtstag; //                                                                                           |
                            int alter = 0; //                                                                                                        |
                            //                                                                                                                       |
                            while (true) //                                                                                                          |
                            {  //                                                                                                                    |
                                Console.Write("Geburtstag (tt.mm.jjjj): "); //                                                                       |
                                string geburtstag = Console.ReadLine(); //                                                                           |
                                //                                                                                                                   |
                                if (DateOnly.TryParseExact( //                                                                                       |
                                        geburtstag, //                                                                                               |
                                        "dd.MM.yyyy",  //                                                                                            |
                                        CultureInfo.InvariantCulture, //                                                                             |
                                        DateTimeStyles.None, //                                                                                      |
                                        out spielerGeburtstag)) //                                                                                   |
                                { //                                                                                                                 |
                                    DateOnly heute = DateOnly.FromDateTime(DateTime.Today); //                                                       |
                                    alter = heute.Year - spielerGeburtstag.Year; //                                                                  |
                                    //                                                                                                               |
                                    if (spielerGeburtstag > heute.AddYears(-alter)) //                                                               |
                                    { //                                                                                                             |
                                        alter--;  //                                                                                                 |
                                    } //                                                                                                             |
                                    //                                                                                                               |
                                    if (alter >= 8 && alter <= 16) //                                                                                |
                                    { //                                                                                                             |
                                        break; //                                                                                                    |
                                    } //                                                                                                             |
                                    else //                                                                                                          |
                                    { //                                                                                                             |
                                        Console.WriteLine("Nur 8–16 Jährige dürfen teilnehmen!"); //                                                 |
                                    } //                                                                                                             |
                                } //                                                                                                                 |
                                else //                                                                                                              |
                                { //                                                                                                                 |
                                    Console.WriteLine("Ungültiges Datum! Bitte im Format tt.mm.jjjj eingeben."); //                                  |
                                } //                                                                                                                 |
                            } //                                                                                                                     |
                            // -----------------------------------------------------------------------------------------------------------------------

                            Console.Write("Wohnort: ");
                            string spielerWohnort = Console.ReadLine();
                            Console.WriteLine("------------------------------");
                            Console.WriteLine("Spieler erfasst!");
                            Console.WriteLine("");

                            spielerListe.Add(spielerID + " " + spielerVorname + " " + spielerName + " " + spielerGeburtstag + " " + spielerWohnort);
                            spielerID++;
                        }
                        else if (menuInput == "2")
                        {
                            if (spielerListe.Count() == 0)
                            {
                                Console.WriteLine("+----------------------------+");
                                Console.WriteLine("|  BITTE SPIELER HINZUFÜGEN  |");
                                Console.WriteLine("+----------------------------+");
                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine("");
                                Console.WriteLine("         Team erfassen        ");
                                Console.WriteLine("------------------------------");
                                Console.Write("Teamname: ");
                                string teamName = Console.ReadLine();
                                Console.WriteLine("");
                                Console.WriteLine("Verfügbare Spieler: ");
                                Console.WriteLine("------------------------------");
                                foreach (string spieler in spielerListe)
                                {
                                    Console.WriteLine(spieler);
                                    Console.WriteLine("------------------------------");
                                }
                                Console.WriteLine("\nSpielerID eingeben (kommagetrennt): ");

                                string[] teamSpieler;

                                while (true) 
                                {
                                    string eingabe = Console.ReadLine();

                                    string[] ids = eingabe.Split(',');
                                    teamSpieler = new string[ids.Length];
                                    for (int i = 0; i < ids.Length; i++)
                                    {
                                        teamSpieler[i] = ids[i].Trim();
                                    }

                                    bool alleGefunden = true;
                                    foreach (string spieler in teamSpieler)
                                    {
                                        bool gefunden = false;

                                        foreach (string eintrag in spielerListe)
                                        {
                                            if (eintrag.StartsWith(spieler + " "))
                                            {
                                                gefunden = true;
                                                break;
                                            }
                                        }

                                        if (!gefunden)
                                        {
                                            Console.WriteLine($"Spieler {spieler} nicht gefunden, bitte erneut eingeben!");
                                            alleGefunden = false;
                                            break;
                                        }
                                    }

                                    if (!alleGefunden)
                                    {
                                        continue;
                                    }

                                    bool schonDrin = false;
                                    foreach (string spieler in teamSpieler)
                                    {
                                        bool bereitsImTeam = false;

                                        foreach (string eintrag in teamListe)
                                        {
                                            if (eintrag.Contains(spieler + " ") || eintrag.EndsWith(spieler))
                                            {
                                                bereitsImTeam = true;
                                                break;
                                            }
                                        }

                                        if (bereitsImTeam)
                                        {
                                            Console.WriteLine($"Spieler {spieler} ist bereits in einem Team! Bitte erneut eingeben.");
                                            schonDrin = true;
                                            break;
                                        }
                                    }

                                    if (schonDrin)
                                    {
                                        continue;
                                    }

                                    break;
                                }

                                Console.WriteLine("------------------------------");
                                Console.WriteLine("Team erfasst!");
                                Console.WriteLine();

                                string spielerString = "";
                                for (int i = 0; i < teamSpieler.Length; i++)
                                {
                                    spielerString += teamSpieler[i];
                                    if (i < teamSpieler.Length - 1)
                                        spielerString += ",";
                                }

                                teamListe.Add(teamID + " " + teamName + " " + spielerString);
                                teamID++;
                            }

                        }
                        else if (menuInput == "3")
                        {
                            if (spielerListe.Count() < 2) {
                                Console.WriteLine("+----------------------------+");
                                Console.WriteLine("|ZUERST MIN. 2 TEAMS ERFASSEN|");
                                Console.WriteLine("+----------------------------+");
                                Console.WriteLine("");
                            }
                            else {
                                Spielplan.Generieren();
                                gameState = false;
                            }
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