using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IProductService
{
    public Task<ServiceResponse<PagedResponse<ProductDto>>> GetProducts(PaginationSearchQueryParams pagination,
        CancellationToken cancellationToken = default);

    public Task<ServiceResponse<ProductDto>> GetProductById(Guid id,
        CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddProduct([FromBody] ProductAddDto product, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateProduct([FromBody] ProductUpdateDto product, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteProduct([FromRoute] Guid id, UserDTO? requestingUser = default,
        CancellationToken cancellationToken = default);

}