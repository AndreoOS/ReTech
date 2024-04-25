namespace MobyLabWebProgramming.Core.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    /* Many-to-many relation with Product */
    public ICollection<Product> Products { get; set; } = default!;
    
}