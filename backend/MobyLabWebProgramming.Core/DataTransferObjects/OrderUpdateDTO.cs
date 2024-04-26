namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record OrderUpdateDTO(Guid Id, DateTime? OrderDate = default, decimal? Total = default, string? ShippingAddress = default, string? Status = default, DateTime? DeliveryDate = default);
