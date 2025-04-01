namespace EFCore_log_and_task.Model;

public class CarOrder
{
    public int CarOrderId { get; set; }
    public int CarId { get; set; }

    public virtual Car Car { get; set; }

    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}