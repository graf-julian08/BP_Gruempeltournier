using BP_Gruempeltournier.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("+----------------------------------+");
                Console.WriteLine("| MINDESTENS ZWEI TEAMS ERSTELLEN! |");
                Console.WriteLine("+----------------------------------+");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Black;
                return (flowControl: false, value: default);
            }

            Spielplan.Generieren();
            gameState = false;
            return (flowControl: true, value: default);
        }
    }
}
