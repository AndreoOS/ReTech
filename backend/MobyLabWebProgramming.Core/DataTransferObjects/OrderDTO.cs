using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using System.ComponentModel.DataAnnotations;

namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class OrderDTO
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; } = default!;
    public decimal Total { get; set; } = default!;
    public string ShippingAddress { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateTime DeliveryDate { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public CustomerDTO Customer { get; set; } = default!;
    public ICollection<ProductDTO> Products { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
