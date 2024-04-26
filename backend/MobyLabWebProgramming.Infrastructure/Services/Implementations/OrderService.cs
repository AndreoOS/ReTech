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

public class OrderService : IOrderService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public OrderService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<OrderDTO>> GetOrder(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new OrderProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<OrderDTO>.ForSuccess(result) :
            ServiceResponse<OrderDTO>.FromError(CommonErrors.OrderNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<OrderDTO>>> GetOrders(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new OrderProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<OrderDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetOrderCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Order>(cancellationToken));

    public async Task<ServiceResponse> AddOrder(OrderAddDTO order, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add orders!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new OrderSpec(order.CustomerId), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The order already exists!", ErrorCodes.OrderAlreadyExists));
        }

        await _repository.AddAsync(new Order
        {
            OrderDate = order.OrderDate,
            Total = order.Total,
            ShippingAddress = order.ShippingAddress,
            Status = order.Status,
            DeliveryDate = order.DeliveryDate,
            CustomerId = order.CustomerId
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateOrder(OrderUpdateDTO order, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin  can update the order!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new OrderSpec(order.Id), cancellationToken);

        if (entity != null)
        {
            entity.OrderDate = order.OrderDate ?? entity.OrderDate;
            entity.Total = order.Total ?? entity.Total;
            entity.ShippingAddress = order.ShippingAddress ?? entity.ShippingAddress;
            entity.Status = order.Status ?? entity.Status;
            entity.DeliveryDate = order.DeliveryDate ?? entity.DeliveryDate;           

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteOrder(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete the order!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Order>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
