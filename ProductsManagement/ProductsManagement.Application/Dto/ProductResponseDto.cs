namespace ProductsManagement.Application.Dto;

public class ProductResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool Available { get; set; }
}