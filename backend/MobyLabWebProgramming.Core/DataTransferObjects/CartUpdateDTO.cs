namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record CartUpdateDTO(Guid Id, decimal? Total = default, string? Status = default);
