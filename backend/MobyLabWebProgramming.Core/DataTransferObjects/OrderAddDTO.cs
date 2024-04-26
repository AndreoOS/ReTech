using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class OrderAddDTO
{
    public DateTime OrderDate { get; set; } = default!;
    public decimal Total { get; set; } = default!;
    public string ShippingAddress { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime DeliveryDate { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
}