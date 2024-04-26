using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ProductBrandSpec : BaseSpec<ProductBrandSpec, ProductBrand>
{
    public ProductBrandSpec(Guid id) : base(id)
    {
    }
}