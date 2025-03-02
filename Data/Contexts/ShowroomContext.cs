using CodeFirst.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CodeFirst.Data.Contexts;

public class ShowroomContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<ServiceHistory> ServiceHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
            .GetConnectionString("Default");
        optionsBuilder.UseSqlServer(connectionString);
    }
    
    
    
}