using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ProductBrandAddDTO
{
    public Guid ProductId { get; set; } = default!;
    public Guid BrandId { get; set; } = default!;
}
