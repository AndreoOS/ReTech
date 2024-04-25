namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ProductAddDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Price { get; set; } = default!;
    public int Quantity { get; set; } = default!;
}