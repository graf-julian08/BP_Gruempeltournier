using Microsoft.Extensions.Configuration;
using BP_Gruempeltournier.Data;

namespace BP_Gruempeltournier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"C:\Schule\BP\Gruempelturnier\appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("GruempeliDb");
            Db.ConnectionString = connectionString;

            //Console.WriteLine(connectionString);

            Menu.CreateMenu();
        }
    }
}