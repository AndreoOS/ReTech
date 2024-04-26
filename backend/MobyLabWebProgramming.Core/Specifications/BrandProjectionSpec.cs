using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class BrandProjectionSpec : BaseSpec<BrandProjectionSpec, Brand, BrandDTO>
{
    protected override Expression<Func<Brand, BrandDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description,
        ProductBrands = e.ProductBrands.Select(f => new ProductBrandDTO
        {
            Id = f.Id,
            BrandId = f.BrandId,
            ProductId = f.ProductId
        }).ToList(),
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public BrandProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public BrandProjectionSpec(Guid id) : base(id)
    {
    }

    public BrandProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr) ||
                         EF.Functions.ILike(e.Description, searchExpr));
    }
}
