using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CartProjectionSpec : BaseSpec<CartProjectionSpec, Cart, CartDTO>
{
    protected override Expression<Func<Cart, CartDTO>> Spec => e => new()
    {
        Id = e.Id,
        Total = e.Total,
        Status = e.Status,
        CustomerId = e.CustomerId,
        Customer = new()
        {
            Id = e.Customer.Id,
            FirstName = e.Customer.FirstName,
            LastName = e.Customer.LastName,
            Email = e.Customer.Email,
            Password = e.Customer.Password,
            PhoneNumber = e.Customer.PhoneNumber,
            Address = e.Customer.Address,
            DateOfBirth = e.Customer.DateOfBirth,
            Gender = e.Customer.Gender,
            Country = e.Customer.Country,
            City = e.Customer.City,
            UserId = e.Customer.UserId
        },
        Products = e.Products.Select(f => new ProductDTO
        {
            Id = f.Id,
            Name = f.Name,
            Description = f.Description,
            Price = f.Price,
            Quantity = f.Quantity,
            Color = f.Color,
            Size = f.Size,
            CategoryId = f.CategoryId,
            OrderId = f.OrderId,
            CartId = f.CartId
        }).ToList(),
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public CartProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public CartProjectionSpec(Guid id) : base(id)
    {
    }

    public CartProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Status, searchExpr));
    }

    public CartProjectionSpec(decimal? search)
    {
        Query.Where(e => e.Total == search);
    }
}
