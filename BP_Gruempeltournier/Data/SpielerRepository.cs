using System.Data;
using BP_Gruempeltournier.Models;

namespace BP_Gruempeltournier.Data
{
    public class SpielerRepository
    {
        public int Insert(Spieler s)
        {
            using var con = Db.GetConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = """
                
                INSERT INTO dbo.Spieler (Vorname, Nachname, Geburtstag, Wohnort)
                OUTPUT INSERTED.SpielerID
                VALUES (@Vorname, @Nachname, @Geburtstag, @Wohnort);
                
                """;

            cmd.Parameters.Add("@Vorname", SqlDbType.NVarChar, 50).Value = s.Vorname;
            cmd.Parameters.Add("@Nachname", SqlDbType.NVarChar, 50).Value = s.Nachname;

            //var dt = s.Geburtstag.ToDateTime(TimeOnly.MinValue);
            var pGeb = cmd.Parameters.Add("@Geburtstag", SqlDbType.Date);
            pGeb.Value = s.Geburtstag;

            cmd.Parameters.Add("@Wohnort", SqlDbType.NVarChar, 100).Value = s.Wohnort;

            con.Open();
            return (int)cmd.ExecuteScalar()!;
        }

        public List<Spieler> GetAll()
        {
            var result = new List<Spieler>();
            using var con = Db.GetConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT SpielerID, Vorname, Nachname, Geburtstag, Wohnort FROM dbo.Spieler ORDER BY SpielerID;";
            con.Open();
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                result.Add(new Spieler
                {
                    SpielerID = r.GetInt32(0),
                    Vorname = r.GetString(1),
                    Nachname = r.GetString(2),
                    Geburtstag = DateOnly.FromDateTime(r.GetDateTime(3)),
                    Wohnort = r.GetString(4)
                });
            }
            return result;
        }

        public bool Exists(int spielerId)
        {
            using var con = Db.GetConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT 1 FROM dbo.Spieler WHERE SpielerID = @Id;";
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = spielerId;
            con.Open();
            using var r = cmd.ExecuteReader();
            return r.Read();
        }
    }
}
