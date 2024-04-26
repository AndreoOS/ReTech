using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ICartService
{
    public Task<ServiceResponse<CartDTO>> GetCart(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<CartDTO>>> GetCarts(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<int>> GetCartCount(CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddCart(CartAddDTO cart, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateCart(CartUpdateDTO cart, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteCart(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
