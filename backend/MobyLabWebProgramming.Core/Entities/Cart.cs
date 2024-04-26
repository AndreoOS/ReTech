using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using System.ComponentModel.DataAnnotations;

namespace MobyLabWebProgramming.Core.Entities;

public class Cart : BaseEntity
{
    public decimal Total { get; set; } = default!;

    [Required]
    [RegularExpression("^(Pending|Processing|Shipped|Delivered|Cancelled)$",
        ErrorMessage = "Status must be either Pending, Processing, Shipped, Delivered or Cancelled.")]
    public string Status { get; set; } = default!;

    /* One-To-One relation with Customer */
    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; } = default!;

    /* One-To-Many relation with Product */
    public ICollection<Product> Products { get; set; } = default!;
}
