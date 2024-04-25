using System.ComponentModel.DataAnnotations;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class Feedback : BaseEntity
{
    public FeedbackRatingEnum Rating { get; set; } = default!;
    public string Comments { get; set; } = default!;

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime FeedbackDate { get; set; } = default;
    
    /* One-to-One relation with Order*/
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;
    
    /* Many-to-One relation with User */
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

}