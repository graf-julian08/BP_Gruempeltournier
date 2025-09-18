using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP_Gruempeltournier.Data
{
    using System.Data;
    using Microsoft.Data.SqlClient;
    using BP_Gruempeltournier.Models;
    using System.Collections.Generic;

    public class TeamRepository
    {
        public int InsertTeam(string teamname)
        {
            using var con = Db.GetConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
INSERT INTO dbo.Team (Teamname)
OUTPUT INSERTED.TeamID
VALUES (@Teamname);";
            cmd.Parameters.Add("@Teamname", SqlDbType.NVarChar, 100).Value = teamname;
            con.Open();
            return (int)cmd.ExecuteScalar()!;
        }

        public void AddSpielerZuTeam(int teamId, int spielerId)
        {
            using var con = Db.GetConnection();
            con.Open();

            using (var check = con.CreateCommand())
            {
                check.CommandText = "SELECT 1 FROM dbo.TeamSpieler WHERE SpielerID = @S;";
                check.Parameters.Add("@S", SqlDbType.Int).Value = spielerId;
                var already = check.ExecuteScalar();
                if (already != null)
                    throw new InvalidOperationException($"Spieler {spielerId} ist bereits in einem Team.");
            }

            using var cmd = con.CreateCommand();
            cmd.CommandText = @"INSERT INTO dbo.TeamSpieler (TeamID, SpielerID) VALUES (@T, @S);";
            cmd.Parameters.Add("@T", SqlDbType.Int).Value = teamId;
            cmd.Parameters.Add("@S", SqlDbType.Int).Value = spielerId;
            cmd.ExecuteNonQuery();
        }

        public List<Team> GetAllWithSpieler()
        {
            var teams = new Dictionary<int, Team>();
            using var con = Db.GetConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
SELECT t.TeamID, t.Teamname, s.SpielerID, s.Vorname, s.Nachname, s.Geburtstag, s.Wohnort
FROM dbo.Team t
LEFT JOIN dbo.TeamSpieler ts ON ts.TeamID = t.TeamID
LEFT JOIN dbo.Spieler s     ON s.SpielerID = ts.SpielerID
ORDER BY t.TeamID, s.SpielerID;";
            con.Open();
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                int teamId = r.GetInt32(0);
                if (!teams.TryGetValue(teamId, out var team))
                {
                    team = new Team { TeamID = teamId, Teamname = r.GetString(1) };
                    teams[teamId] = team;
                }
                if (!r.IsDBNull(2))
                {
                    team.Spieler.Add(new Spieler
                    {
                        SpielerID = r.GetInt32(2),
                        Vorname = r.GetString(3),
                        Nachname = r.GetString(4),
                        Geburtstag = DateOnly.FromDateTime(r.GetDateTime(5)),
                        Wohnort = r.GetString(6)
                    });
                }
            }
            return teams.Values.ToList();
        }
    }
}

