using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class BrandSpec : BaseSpec<BrandSpec, Brand>
{
    public BrandSpec(Guid id) : base(id)
    {
    }

    public BrandSpec(string name)
    {
        Query.Where(e => e.Name == name);
    }
}