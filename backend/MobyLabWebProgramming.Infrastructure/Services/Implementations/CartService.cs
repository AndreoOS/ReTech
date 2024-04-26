using System.Diagnostics.Metrics;
using System.Net;
using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using Org.BouncyCastle.Utilities.Net;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class CartService : ICartService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public CartService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<CartDTO>> GetCart(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new CartProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<CartDTO>.ForSuccess(result) :
            ServiceResponse<CartDTO>.FromError(CommonErrors.CartNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<CartDTO>>> GetCarts(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new CartProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<CartDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetCartCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Cart>(cancellationToken));

    public async Task<ServiceResponse> AddCart(CartAddDTO cart, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add carts!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new CartSpec(cart.CustomerId), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The cart already exists!", ErrorCodes.CartAlreadyExists));
        }

        await _repository.AddAsync(new Cart
        {
            Total = cart.Total,
            Status = cart.Status,
            CustomerId = cart.CustomerId
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateCart(CartUpdateDTO cart, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin  can update the cart!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new CartSpec(cart.Id), cancellationToken);

        if (entity != null)
        {
            entity.Total = cart.Total ?? entity.Total;
            entity.Status = cart.Status ?? entity.Status;            

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteCart(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete the cart!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Cart>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
