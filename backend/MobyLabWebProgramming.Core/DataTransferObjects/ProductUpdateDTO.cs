namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record ProductUpdateDTO(Guid Id, string? Name = default, string? Description = default, decimal? Price = default, int? Quantity = default, string? Color = default, string? Size = default);
