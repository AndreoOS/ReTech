using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CartSpec : BaseSpec<CartSpec, Cart>
{
    public CartSpec(Guid id) : base(id)
    {
    }
}