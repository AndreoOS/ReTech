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

public class ProductBrandService : IProductBrandService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public ProductBrandService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<ProductBrandDTO>> GetProductBrand(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new ProductBrandProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<ProductBrandDTO>.ForSuccess(result) :
            ServiceResponse<ProductBrandDTO>.FromError(CommonErrors.ProductBrandNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<ProductBrandDTO>>> GetProductBrands(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ProductBrandProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<ProductBrandDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetProductBrandCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<ProductBrand>(cancellationToken));

    public async Task<ServiceResponse> AddProductBrand(ProductBrandAddDTO productBrand, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can add productBrands!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new ProductBrandSpec(productBrand.ProductId), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The productBrand already exists!", ErrorCodes.ProductBrandAlreadyExists));
        }

        await _repository.AddAsync(new ProductBrand
        {
            BrandId = productBrand.BrandId,
            ProductId = productBrand.ProductId
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateProductBrand(ProductBrandUpdateDTO productBrand, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin  can update the productBrand!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new ProductBrandSpec(productBrand.Id), cancellationToken);

        if (entity != null)
        {
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteProductBrand(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete the productBrand!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<ProductBrand>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
