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
                .AddJsonFile(@"C:\Users\graf_\source\repos\M320_OoP\BP_Gruempeltournier\appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("GruempeliDb");
            Db.ConnectionString = connectionString;

            //Console.WriteLine(connectionString);

            Menu.CreateMenu();
        }
    }
}