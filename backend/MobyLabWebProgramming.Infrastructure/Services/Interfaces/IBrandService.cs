using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IBrandService
{
    public Task<ServiceResponse<BrandDTO>> GetBrand(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<BrandDTO>>> GetBrands(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<int>> GetBrandCount(CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddBrand(BrandAddDTO brand, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateBrand(BrandUpdateDTO brand, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteBrand(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
