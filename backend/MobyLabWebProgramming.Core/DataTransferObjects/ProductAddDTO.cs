using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ProductAddDTO
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public string Color { get; set; } = default!;
    public string Size { get; set; } = default!;
    public Guid CategoryId { get; set; } = default!;
    public Guid CartId { get; set; } = default!;
    public Guid OrderId { get; set; } = default!;
}
