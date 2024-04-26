using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class Brand : BaseEntity
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    /* Many-To-Many relation with Product */
    public ICollection<ProductBrand> ProductBrands { get; set; } = default!;
}
