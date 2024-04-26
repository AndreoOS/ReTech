using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ICustomerService
{
    public Task<ServiceResponse<CustomerDTO>> GetCustomer(Guid id, CancellationToken cancellationToken = default);
 
    public Task<ServiceResponse<PagedResponse<CustomerDTO>>> GetCustomers(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<int>> GetCustomerCount(CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddCustomer(CustomerAddDTO customer, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateCustomer(CustomerUpdateDTO customer, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteCustomer(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
