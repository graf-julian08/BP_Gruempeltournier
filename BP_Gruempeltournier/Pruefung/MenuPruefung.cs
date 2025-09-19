using BP_Gruempeltournier.Data;
using BP_Gruempeltournier;

namespace BP_Gruempeltournier
{
    public class MenuPruefung
    {
        public static void MenuPunkte(ref bool gameState, SpielerRepository spielerRepo, TeamRepository teamRepo, string menuInput)
        {
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