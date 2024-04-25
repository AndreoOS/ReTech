using System.Net;
using Microsoft.AspNetCore.Mvc;
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

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public ProductService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<PagedResponse<ProductDto>>> GetProducts(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result =
            await _repository.PageAsync(pagination, new ProductProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<ProductDto>>.ForSuccess(result);

    }

    public async Task<ServiceResponse<ProductDto>> GetProductById(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new ProductProjectionSpec(id), cancellationToken);

        return result != null
            ? ServiceResponse<ProductDto>.ForSuccess(result)
            : ServiceResponse<ProductDto>.FromError(CommonErrors.UserNotFound);
    }

    public async Task<ServiceResponse> AddProduct(ProductAddDto product, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && (requestingUser.Role != UserRoleEnum.Admin || requestingUser.Role != UserRoleEnum.Personnel))
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins/personnel can add products!",
                ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new ProductSpec(product.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict,
                "A product with the same name exists already!", ErrorCodes.ProductAlreadyExists));
        }

        await _repository.AddAsync(new Product
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateProduct(ProductUpdateDto product, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && (requestingUser.Role != UserRoleEnum.Admin || requestingUser.Role != UserRoleEnum.Personnel))
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins/personnel can update products!",
                ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new ProductSpec(product.Id), cancellationToken);

        if (entity != null)
        {
            entity.Name = product.Name ?? entity.Name;
            entity.Description = product.Description ?? entity.Description;
            entity.Quantity = product.Quantity ?? entity.Quantity;
            entity.Price = product.Price ?? entity.Price;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteProduct(Guid id, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && (requestingUser.Role != UserRoleEnum.Admin || requestingUser.Role != UserRoleEnum.Personnel))
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins/personnel can delete products!",
                ErrorCodes.CannotAdd));
        }

        await _repository.DeleteAsync<Product>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}