namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record CustomerUpdateDTO(Guid Id, string? FirstName = default, string? LastName = default, string? Email = default, string? Password = default, string? PhoneNumber = default, string? Address = default, string? DateOfBirth = default, string? Gender = default, string? Country = default, string? City = default);
