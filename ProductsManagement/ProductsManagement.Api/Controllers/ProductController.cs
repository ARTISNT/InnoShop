using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsManagement.Api.Dto;
using ProductsManagement.Application.Dto;
using ProductsManagement.Application.Implementation.Cqrs.Commands;
using ProductsManagement.Application.Implementation.Cqrs.Commands.Products;
using ProductsManagement.Application.Implementation.Cqrs.Queries;
using ProductsManagement.Application.Implementation.Cqrs.Queries.Products;

namespace ProductsManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProducts([FromQuery] ProductFilteringDto productFilteringDto)
    {
        var products = await sender.Send(new GetAllProductsQuery(productFilteringDto));
        return Ok(products);
    }

    [HttpGet]
    [Route("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetProductById([FromRoute] Guid id)
    {
        var product = await sender.Send(new GetProductByIdQuery(id));
        return Ok(product);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProduct([FromBody]CreateProductDto dto)
    {
        var result = await sender.Send(new CreateProductCommand(dto, User));
        return CreatedAtAction(nameof(GetProductById), new { id = result }, null);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateProduct([FromRoute]Guid id, [FromBody] UpdateProductDto dto)
    {
        var result = await sender.Send(new UpdateProductCommand(id, User, dto));
        return CreatedAtAction(nameof(GetProductById), new { id = result }, null);
    }

    [HttpPut]
    [Route("{id}/status")]
    [Authorize]
    public async Task<IActionResult> UpdateStatusOfProduct([FromRoute] Guid id,
        [FromBody] UpdateStatusOfProductApiDto dto)
    {
        await sender.Send(new UpdateStatusOfProductCommand(id, dto.Status));
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
    {
        await sender.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}