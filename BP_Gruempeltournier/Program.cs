using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.AddEnvironmentVariables();

var config = builder.Build();
var connectionString = config.GetConnectionString("GruempeliDb");


namespace BP_Gruempeltournier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu.CreateMenu();
        }
    }
}