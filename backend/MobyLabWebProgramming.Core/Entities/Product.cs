namespace MobyLabWebProgramming.Core.Entities;

public class Product: BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Price { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    
    
}