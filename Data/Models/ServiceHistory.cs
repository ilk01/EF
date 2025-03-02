using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Models;

public class ServiceHistory
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("CarId")]
    public int CarId { get; set; }

    public DateTime ServiceDate { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }


    public Car Car { get; init; }

    public ServiceHistory(int carId, DateTime serviceDate, string description)
    {
        CarId = carId;
        ServiceDate = serviceDate;
        Description = description;
    }
}
