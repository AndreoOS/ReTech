using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public decimal Price { get; set; } = default!;

    public int Quantity { get; set; } = default!;

    public string Color { get; set; } = default!;

    public string Size { get; set; } = default!;

    /* Many-To-One relation with Category */
    public Guid CategoryId { get; set; }

    public Category Category { get; set; } = default!;

    /* Many-To-One relation with Cart */
    public Guid CartId { get; set; }

    public Cart Cart { get; set; } = default!;

    /* Many-To-One relation with Order */
    public Guid OrderId { get; set; }

    public Order Order { get; set; } = default!;

    /* Many-To-Many relation with Brand */
    public ICollection<ProductBrand> ProductBrands { get; set; } = default!;
}
