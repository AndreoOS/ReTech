using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class CustomerAddDTO
{
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
}