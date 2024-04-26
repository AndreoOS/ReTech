using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ProductProjectionSpec : BaseSpec<ProductProjectionSpec, Product, ProductDTO>
{
    protected override Expression<Func<Product, ProductDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description,
        Price = e.Price,
        Quantity = e.Quantity,
        Color = e.Color,
        Size = e.Size,
        CategoryId = e.CategoryId,
        Category = new ()
        {
            Id = e.Category.Id,
            Name = e.Category.Name,
            Description = e.Category.Description
        },
        OrderId = e.OrderId,
        Order = new ()
        {
            Id = e.Order.Id,
            OrderDate = e.Order.OrderDate,
            Total = e.Order.Total,
            ShippingAddress = e.Order.ShippingAddress,
            Status = e.Order.Status,
            DeliveryDate = e.Order.DeliveryDate,
            CustomerId = e.Order.CustomerId
        },
        CartId = e.CartId,
        Cart = new ()
        {
            Id = e.Cart.Id,
            Total = e.Cart.Total,
            Status = e.Cart.Status,
            CustomerId = e.Cart.CustomerId
        },
        ProductBrands = e.ProductBrands.Select(f => new ProductBrandDTO 
        {
            Id = f.Id,
            ProductId = f.ProductId,
            BrandId = f.BrandId
        }).ToList(),
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public ProductProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public ProductProjectionSpec(Guid id) : base(id)
    {
    }

    public ProductProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr) ||
                         EF.Functions.ILike(e.Description, searchExpr) ||
                         EF.Functions.ILike(e.Color, searchExpr) ||
                         EF.Functions.ILike(e.Size, searchExpr));
    }

    public ProductProjectionSpec(decimal? search)
    {
        Query.Where(e => e.Price == search);
    }

    public ProductProjectionSpec(int? search)
    {
        Query.Where(e => e.Quantity == search);
    }
}
