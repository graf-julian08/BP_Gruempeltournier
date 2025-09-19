using Microsoft.Extensions.Configuration;
using BP_Gruempeltournier.Data;
using BP_Gruempeltournier;

namespace BP_Gruempeltournier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.SetProgramColors();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"C:\Users\graf_\source\repos\BP_Gruempeltournier\Gruempeltournier\Gruempeltournier\BP_Gruempeltournier\appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("GruempeliDb");
            Db.ConnectionString = connectionString;

            bool gameState = true;
            var spielerRepo = new SpielerRepository();
            var teamRepo = new TeamRepository();

            Menu.CreateMenu(ref gameState, spielerRepo, teamRepo);
        }
    }
}