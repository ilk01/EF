namespace EFCore_log_and_task.Model;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<CarOrder> CarOrders { get; set; }
}