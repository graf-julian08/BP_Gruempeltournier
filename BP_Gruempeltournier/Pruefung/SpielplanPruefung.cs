using BP_Gruempeltournier.Data;
using System;

namespace BP_Gruempeltournier
{
    internal class SpielplanPruefung
    {
        public static (bool flowControl, bool value) SpielplanPruefen(bool gameState)
        {
            int teamCount = 0;
            using (var con = Db.GetConnection())
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(*) FROM dbo.Team;";
                con.Open();
                teamCount = (int)cmd.ExecuteScalar()!;
            }

            if (teamCount < 2)
            {
                ConsoleHelper.WriteLineColored(
                    "+----------------------------------+\n" +
                    "| MINDESTENS ZWEI TEAMS ERSTELLEN! |\n" +
                    "+----------------------------------+",
                    ConsoleColor.Red
                );

                // zurück zum Menü (flowControl = true, value = false z.B.)
                return (flowControl: true, value: false);
            }
            else
            {
                Spielplan.Generieren();
                gameState = false;

                // alles okay, weiter im Flow
                return (flowControl: true, value: true);
            }
        }
    }
}