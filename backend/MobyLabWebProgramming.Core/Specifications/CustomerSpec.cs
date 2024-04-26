using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CustomerSpec : BaseSpec<CustomerSpec, Customer>
{
    public CustomerSpec(Guid id) : base(id)
    {
    }
}