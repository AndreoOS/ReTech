using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using System.ComponentModel.DataAnnotations;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class CartDTO
{
    public Guid Id { get; set; }
    public decimal Total { get; set; } = default!;
    public string Status { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public CustomerDTO Customer { get; set; } = default!;
    public ICollection<ProductDTO> Products { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
