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

public class ProductService : IProductService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public ProductService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<ProductDTO>> GetProduct(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new ProductProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<ProductDTO>.ForSuccess(result) :
            ServiceResponse<ProductDTO>.FromError(CommonErrors.ProductNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<ProductDTO>>> GetProducts(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ProductProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<ProductDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetProductCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Product>(cancellationToken));

    public async Task<ServiceResponse> AddProduct(ProductAddDTO product, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can add products!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new ProductSpec(product.CategoryId), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The product already exists!", ErrorCodes.ProductAlreadyExists));
        }

        await _repository.AddAsync(new Product
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity,
            Color = product.Color,
            Size = product.Size,
            CategoryId = product.CategoryId,
            OrderId = product.OrderId,
            CartId = product.CartId
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateProduct(ProductUpdateDTO product, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin  can update the product!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new ProductSpec(product.Id), cancellationToken);

        if (entity != null)
        {
            entity.Name = product.Name ?? entity.Name;
            entity.Description = product.Description ?? entity.Description;
            entity.Price = product.Price ?? entity.Price;
            entity.Quantity = product.Quantity ?? entity.Quantity;
            entity.Color = product.Color ?? entity.Color;
            entity.Size = product.Size ?? entity.Size;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteProduct(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete the product!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Product>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
