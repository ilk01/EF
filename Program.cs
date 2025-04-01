using EFCore_log_and_task.Data;
using EFCore_log_and_task.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EFCore_log_and_task
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<CarShopContext>((serviceProvider, options) =>
                    {
                        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                            .UseLazyLoadingProxies();
                    });

                    services.AddTransient<CarService>();
                })
                .Build();

            var carService = host.Services.GetRequiredService<CarService>();
            carService.PerformCrudOperations();
        }
    }
}