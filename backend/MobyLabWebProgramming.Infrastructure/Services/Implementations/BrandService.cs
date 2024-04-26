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

public class BrandService : IBrandService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public BrandService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<BrandDTO>> GetBrand(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new BrandProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<BrandDTO>.ForSuccess(result) :
            ServiceResponse<BrandDTO>.FromError(CommonErrors.BrandNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<BrandDTO>>> GetBrands(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new BrandProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<BrandDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetBrandCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Brand>(cancellationToken));

    public async Task<ServiceResponse> AddBrand(BrandAddDTO brand, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add brands!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new BrandSpec(brand.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The brand already exists!", ErrorCodes.BrandAlreadyExists));
        }

        await _repository.AddAsync(new Brand
        {
            Name = brand.Name,
            Description = brand.Description
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateBrand(BrandUpdateDTO brand, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin  can update the brand!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new BrandSpec(brand.Id), cancellationToken);

        if (entity != null)
        {
            entity.Name = brand.Name ?? entity.Name;
            entity.Description = brand.Description ?? entity.Description;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteBrand(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete the brand!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Brand>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
