using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class ProductBrand : BaseEntity
{
    public Guid ProductId { get; set; }

    public Product Product { get; set; } = default!;

    public Guid BrandId { get; set; }

    public Brand Brand { get; set; } = default!;
}
