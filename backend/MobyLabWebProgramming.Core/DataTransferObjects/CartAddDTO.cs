using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class CartAddDTO
{
    public decimal Total { get; set; } = default!;
    public string Status { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
}
