using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CustomerProjectionSpec : BaseSpec<CustomerProjectionSpec, Customer, CustomerDTO>
{
    protected override Expression<Func<Customer, CustomerDTO>> Spec => e => new()
    {
        Id = e.Id,
        Email = e.Email,
        FirstName = e.FirstName,
        LastName = e.LastName,
        Password = e.Password,
        PhoneNumber = e.PhoneNumber,
        Address = e.Address,
        DateOfBirth = e.DateOfBirth,
        Gender = e.Gender,
        Country = e.Country,
        City = e.City,
        UserId = e.UserId,
        User = new ()
        {
            Id = e.User.Id,
            Email = e.User.Email,
            System = e.User.System,
            Name = e.User.Name,
            Role = e.User.Role
        },
        Orders = e.Orders.Select(f => new OrderDTO 
        { 
            Id = f.Id,
            OrderDate = f.OrderDate,
            Total = f.Total,
            ShippingAddress = f.ShippingAddress,
            Status = f.Status,
            DeliveryDate = f.DeliveryDate,
            CustomerId = f.CustomerId
        }).ToList(),
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public CustomerProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public CustomerProjectionSpec(Guid id) : base(id)
    {
    }

    public CustomerProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.FirstName, searchExpr) ||
                         EF.Functions.ILike(e.LastName, searchExpr) || 
                         EF.Functions.ILike(e.Email, searchExpr) || 
                         EF.Functions.ILike(e.Password, searchExpr) || 
                         EF.Functions.ILike(e.PhoneNumber, searchExpr) || 
                         EF.Functions.ILike(e.Address, searchExpr) || 
                         EF.Functions.ILike(e.DateOfBirth, searchExpr) || 
                         EF.Functions.ILike(e.Gender, searchExpr) || 
                         EF.Functions.ILike(e.Country, searchExpr) || 
                         EF.Functions.ILike(e.City, searchExpr));
    }
}
