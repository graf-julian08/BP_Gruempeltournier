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
<<<<<<< HEAD

=======
>>>>>>> 217063d2dabc9f1239aafddd99ac6a9df5f55c30
        }
    }
}