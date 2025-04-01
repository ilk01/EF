using EFCore_log_and_task.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCore_log_and_task.Data;
public class CarShopContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Car> Cars { get; set; }
    public DbSet<Dealer> Dealers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CarOrder> CarOrders { get; set; }

    public CarShopContext(DbContextOptions<CarShopContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                .UseLazyLoadingProxies();
        }
    }
}
