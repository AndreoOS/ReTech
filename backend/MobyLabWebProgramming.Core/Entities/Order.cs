using System.ComponentModel.DataAnnotations;

namespace MobyLabWebProgramming.Core.Entities;

public class Order : BaseEntity
{
    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime OrderDate { get; set; } = default!;

    public decimal Total { get; set; } = default!;
    
    [Required]
    [RegularExpression("^(Pending|Processing|Shipped|Delivered|Cancelled)$", 
        ErrorMessage = "Status must be either Pending, Processing, Shipped, Delivered or Cancelled.")]
    public string Status { get; set; } = default!;

    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DeliveryDate { get; set; } = default!;
    
    /* Many-to-One relation with User */
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    
    /* One-to-One relation with Feedback */
    public Feedback? OrderFeedback { get; set; }
    
    /* One-to-One relation with OrderDetails */
    public OrderDetails? OrderDetails { get; set; }
    
}