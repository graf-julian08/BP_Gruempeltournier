using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BP_Gruempeltournier.Data
{
    public class SpielplanRepository
    {
        public void Insert(int team1, int team2, int spieldauer, int pausendauer, DateTime spielstart)
        {
            using var con = Db.GetConnection();
            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
INSERT INTO dbo.Spielplan (Team1, Team2, Spieldauer, Pausendauer, Spielstart)
VALUES (@T1, @T2, @SD, @PD, @SS);";
            cmd.Parameters.Add("@T1", SqlDbType.Int).Value = team1;
            cmd.Parameters.Add("@T2", SqlDbType.Int).Value = team2;
            cmd.Parameters.Add("@SD", SqlDbType.Int).Value = spieldauer;
            cmd.Parameters.Add("@PD", SqlDbType.Int).Value = pausendauer;
            cmd.Parameters.Add("@SS", SqlDbType.DateTime2).Value = spielstart;

            con.Open();
            cmd.ExecuteNonQuery();
        }
        public void DeleteAll()
        {
            using var con = Db.GetConnection();
            con.Open();

            try
            {
                using var tran = con.BeginTransaction();
                using (var cmd = new SqlCommand("TRUNCATE TABLE dbo.Spielplan;", con, tran))
                {
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                return;
            }
            catch (SqlException)
            {
            }

            using (var tran = con.BeginTransaction())
            {
                try
                {
                    using (var cmdDel = new SqlCommand("DELETE FROM dbo.Spielplan;", con, tran))
                    {
                        cmdDel.ExecuteNonQuery();
                    }

                    using (var cmdReseed = new SqlCommand("DBCC CHECKIDENT ('dbo.Spielplan', RESEED, 0);", con, tran))
                    {
                        cmdReseed.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}