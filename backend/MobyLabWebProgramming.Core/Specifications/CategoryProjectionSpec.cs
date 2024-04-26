using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CategoryProjectionSpec : BaseSpec<CategoryProjectionSpec, Category, CategoryDTO>
{
    protected override Expression<Func<Category, CategoryDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description,
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

    public CategoryProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public CategoryProjectionSpec(Guid id) : base(id)
    {
    }

    public CategoryProjectionSpec(string? search)
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
