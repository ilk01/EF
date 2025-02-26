
namespace ConsoleApp8;

public class Car
{
    public int? Id { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public decimal? Price { get; set; }
    
    public Car(int? id, string? brand, string? model, int? year, decimal? price)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
    }
    
    public Car() { }
}

