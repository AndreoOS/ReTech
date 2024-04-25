namespace MobyLabWebProgramming.Core.Entities;

public class Product: BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Price { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    
    /* Many-to-Many relation with Category */
    public ICollection<Category> Categories { get; set; } = default!;
    
    /* Many-to-Many relation with OrderDetails */
    public ICollection<OrderDetails> OrderDetailsCollection { get; set; } = default!;
}