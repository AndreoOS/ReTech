using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ICategoryService
{
    public Task<ServiceResponse<CategoryDTO>> GetCategory(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<CategoryDTO>>> GetCategorys(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<int>> GetCategoryCount(CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddCategory(CategoryAddDTO category, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateCategory(CategoryUpdateDTO category, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteCategory(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
