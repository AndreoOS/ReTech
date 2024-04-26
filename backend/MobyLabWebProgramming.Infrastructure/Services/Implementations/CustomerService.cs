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

public class CustomerService : ICustomerService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public CustomerService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<CustomerDTO>> GetCustomer(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new CustomerProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<CustomerDTO>.ForSuccess(result) :
            ServiceResponse<CustomerDTO>.FromError(CommonErrors.CustomerNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<CustomerDTO>>> GetCustomers(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new CustomerProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<CustomerDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetCustomerCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Customer>(cancellationToken));

    public async Task<ServiceResponse> AddCustomer(CustomerAddDTO customer, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add customers!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new CustomerSpec(customer.UserId), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The customer already exists!", ErrorCodes.CustomerAlreadyExists));
        }

        await _repository.AddAsync(new Customer
        {
            Email = customer.Email,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Password = customer.Password,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            DateOfBirth = customer.DateOfBirth,
            Gender = customer.Gender,
            Country = customer.Country,
            City = customer.City,
            UserId = customer.UserId
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateCustomer(CustomerUpdateDTO customer, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin  can update the customer!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new CustomerSpec(customer.Id), cancellationToken);

        if (entity != null) 
        {
            entity.Email = customer.Email ?? entity.Email;
            entity.FirstName = customer.FirstName ?? entity.FirstName;
            entity.LastName = customer.LastName ?? entity.LastName;
            entity.Password = customer.Password ?? entity.Password;
            entity.PhoneNumber = customer.PhoneNumber ?? entity.PhoneNumber;
            entity.Address = customer.Address ?? entity.Address;
            entity.DateOfBirth = customer.DateOfBirth ?? entity.DateOfBirth;
            entity.Gender = customer.Gender ?? entity.Gender;
            entity.Country = customer.Country ?? entity.Country;
            entity.City = customer.City ?? entity.City;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteCustomer(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete the customer!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Customer>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
