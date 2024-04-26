namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record BrandUpdateDTO(Guid Id, string? Name = default, string? Description = default);
