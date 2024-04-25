namespace MobyLabWebProgramming.Core.Entities;

public class OrderDetails : BaseEntity
{
    public string Address { get; set; } = default!;
    
    /* One-to-one relation with Order */
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;
    
    /* Many-to-Many relation with Product*/
    private ICollection<Product> OrderProducts { get; set; } = default!;

    public int Quantity => OrderProducts.Count;
    
}