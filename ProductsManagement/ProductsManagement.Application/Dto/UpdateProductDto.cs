namespace ProductsManagement.Application.Dto;

public class UpdateProductDto
{
    public string Description { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool Available { get; set; } 
}