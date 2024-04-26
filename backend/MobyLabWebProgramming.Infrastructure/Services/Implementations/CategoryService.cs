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

public class CategoryService : ICategoryService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public CategoryService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<CategoryDTO>> GetCategory(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new CategoryProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<CategoryDTO>.ForSuccess(result) :
            ServiceResponse<CategoryDTO>.FromError(CommonErrors.CategoryNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<CategoryDTO>>> GetCategorys(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new CategoryProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<CategoryDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetCategoryCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Category>(cancellationToken));

    public async Task<ServiceResponse> AddCategory(CategoryAddDTO category, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add categorys!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new CategorySpec(category.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The category already exists!", ErrorCodes.CategoryAlreadyExists));
        }

        await _repository.AddAsync(new Category
        {
            Name = category.Name,
            Description = category.Description
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateCategory(CategoryUpdateDTO category, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin  can update the category!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new CategorySpec(category.Id), cancellationToken);

        if (entity != null)
        {
            entity.Name = category.Name ?? entity.Name;
            entity.Description = category.Description ?? entity.Description;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteCategory(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete the category!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Category>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
