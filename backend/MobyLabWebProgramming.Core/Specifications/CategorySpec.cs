using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CategorySpec : BaseSpec<CategorySpec, Category>
{
    public CategorySpec(Guid id) : base(id)
    {
    }

    public CategorySpec(string name)
    {
        Query.Where(e => e.Name == name);
    }
}