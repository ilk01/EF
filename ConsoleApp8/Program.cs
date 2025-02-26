using Microsoft.Extensions.Configuration;

namespace ConsoleApp8; 

class Program 
{ 
    static void Main() 
    {
        var configBuilder = new ConfigurationBuilder(); 
        configBuilder.AddJsonFile("appsettings.json");
        var config = configBuilder.Build();
        var connectionString = config.GetConnectionString("Default");

        var navigationMenu = new NavigationMenu(connectionString);
        navigationMenu.ShowMenu();
    } 
}
