using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IProductBrandService
{
    public Task<ServiceResponse<ProductBrandDTO>> GetProductBrand(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<ProductBrandDTO>>> GetProductBrands(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<int>> GetProductBrandCount(CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddProductBrand(ProductBrandAddDTO productBrand, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateProductBrand(ProductBrandUpdateDTO productBrand, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteProductBrand(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
