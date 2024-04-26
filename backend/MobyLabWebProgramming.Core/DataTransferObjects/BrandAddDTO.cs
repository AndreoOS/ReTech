using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class BrandAddDTO
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}
