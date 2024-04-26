using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using System.ComponentModel.DataAnnotations;

namespace MobyLabWebProgramming.Core.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = default!;

    [Required]
    [StringLength(32, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long and at most 32.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [Required]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
    public string PhoneNumber { get; set; } = default!;

    public string Address { get; set; } = default!;

    [Required]
    [DataType(DataType.Date)]
    public string DateOfBirth { get; set; } = default!;

    [Required]
    [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "Gender must be either Male, Female, or Other.")]
    public string Gender { get; set; } = default!;

    public string Country { get; set; } = default!;

    public string City { get; set; } = default!;

    /* One-To-One relation with User */
    public Guid UserId { get; set; }

    public User User { get; set; } = default!;

    /* One-To-One relation with Cart */
    public Cart Cart { get; set; } = default!;

    /* One-To-Many relation with Order */
    public ICollection<Order> Orders { get; set; } = default!;
}
