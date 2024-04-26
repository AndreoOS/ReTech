using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IOrderService
{
    public Task<ServiceResponse<OrderDTO>> GetOrder(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<OrderDTO>>> GetOrders(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<int>> GetOrderCount(CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddOrder(OrderAddDTO order, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateOrder(OrderUpdateDTO order, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteOrder(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
