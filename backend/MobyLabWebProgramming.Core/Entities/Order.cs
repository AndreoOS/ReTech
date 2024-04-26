using MobyLabWebProgramming.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace MobyLabWebProgramming.Core.Entities;

public class Order : BaseEntity
{
    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime OrderDate { get; set; } = default!;

    public decimal Total { get; set; } = default!;

    public string ShippingAddress { get; set; } = default!;

    [Required]
    [RegularExpression("^(Pending|Processing|Shipped|Delivered|Cancelled)$", 
        ErrorMessage = "Status must be either Pending, Processing, Shipped, Delivered or Cancelled.")]
    public string Status { get; set; } = default!;

    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DeliveryDate { get; set; } = default!;

    /* Many-To-One relation with Customer */
    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; } = default!;

    /* One-To-Many relation with Product */
    public ICollection<Product> Products { get; set; } = default!;
}
