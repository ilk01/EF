using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Models;

public class Sale
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("CarId")]
    public int CarId { get; set; }

    [ForeignKey("CustomerId")]
    public int CustomerId { get; set; }

    [ForeignKey("EmployeeId")]
    public int EmployeeId { get; set; }

    [Required]
    public DateTime SaleDate { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    public Car Car { get; set; }
    public Customer Customer { get; set; }
    public Employee Employee { get; set; }

    public Sale(int carId, int customerId, int employeeId, DateTime saleDate, decimal price)
    {
    
        CarId = carId;
        CustomerId = customerId;
        EmployeeId = employeeId;
        SaleDate = DateTime.Now;
        Price = price;
    }


}
