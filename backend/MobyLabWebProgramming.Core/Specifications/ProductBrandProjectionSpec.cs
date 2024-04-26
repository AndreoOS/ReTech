using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ProductBrandProjectionSpec : BaseSpec<ProductBrandProjectionSpec, ProductBrand, ProductBrandDTO>
{
    protected override Expression<Func<ProductBrand, ProductBrandDTO>> Spec => e => new()
    {
        Id = e.Id,
        ProductId = e.ProductId,
        Product = new()
        {
            Id = e.Product.Id,
            Name = e.Product.Name,
            Description = e.Product.Description,
            Price = e.Product.Price,
            Quantity = e.Product.Quantity,
            Color = e.Product.Color,
            Size = e.Product.Size,
            CategoryId = e.Product.CategoryId,
            OrderId = e.Product.OrderId,
            CartId = e.Product.CartId
        },
        BrandId = e.BrandId,
        Brand = new()
        {
            Id = e.Product.Id,
            Name = e.Product.Name,
            Description = e.Product.Description
        },
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public ProductBrandProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public ProductBrandProjectionSpec(Guid id) : base(id)
    {
    }

    public ProductBrandProjectionSpec(string? search)
    {
    }
}
