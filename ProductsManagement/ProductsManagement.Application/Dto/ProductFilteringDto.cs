namespace ProductsManagement.Application.Dto;

public class ProductFilteringDto
{
    public string? Name { get;  set; }
    public decimal? LowerPrice { get; set; }
    public decimal? UpperPrice { get; set; }
}