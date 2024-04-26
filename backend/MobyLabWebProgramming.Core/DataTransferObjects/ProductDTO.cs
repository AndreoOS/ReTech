using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ProductDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public string Color { get; set; } = default!;
    public string Size { get; set; } = default!;
    public Guid CategoryId { get; set; } = default!;
    public CategoryDTO Category { get; set; } = default!;
    public Guid CartId { get; set; } = default!;
    public CartDTO Cart { get; set; } = default!;
    public Guid OrderId { get; set; } = default!;
    public OrderDTO Order { get; set; } = default!;
    public ICollection<ProductBrandDTO> ProductBrands { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
