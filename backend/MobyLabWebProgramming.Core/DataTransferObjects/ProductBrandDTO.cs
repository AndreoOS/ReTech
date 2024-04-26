using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ProductBrandDTO
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; } = default!;
    public ProductDTO Product { get; set; } = default!;
    public Guid BrandId { get; set; } = default!;
    public BrandDTO Brand { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
