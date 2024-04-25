namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record ProductUpdateDto(Guid Id, string? Name = default, string? Description = default, int? Price = default, int? Quantity = default );