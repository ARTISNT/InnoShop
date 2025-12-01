namespace ProductsManagement.Application.Dto;

public class CreateProductDto
{
    public string Description { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}