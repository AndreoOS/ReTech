using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using System.ComponentModel.DataAnnotations;

namespace MobyLabWebProgramming.Core.DataTransferObjects;
public class CustomerDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string DateOfBirth { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;
    public Guid UserId { get; set; } = default!;
    public UserDTO User { get; set; } = default!;
    public ICollection<OrderDTO> Orders { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}