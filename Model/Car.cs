using System.ComponentModel.DataAnnotations;

namespace EFCore_log_and_task.Model;

public class Car
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string? Make { get; set; }

    [Required]
    public string? Model { get; set; }

    [Range(1900, 2100)]
    public int Year { get; set; }

    public int DealerId { get; set; }
    public virtual Dealer Dealer { get; set; }

    public bool IsDeleted { get; set; }

    public virtual List<CarOrder> CarOrders { get; set; } = new();
}
