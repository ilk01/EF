using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Data.Models;

public class Car
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Brand { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Model { get; set; }
    
    [Required]
    public int Year { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
    
    public Car(string brand, string model, int year, decimal price)
    {
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
    }
}
