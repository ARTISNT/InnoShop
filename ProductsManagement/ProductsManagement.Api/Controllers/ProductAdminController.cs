using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsManagement.Application.Implementation.Cqrs.Commands.AdminProductsActionsCommands;

namespace ProductsManagement.Api.Controllers;

[ApiController]
[Route("api/products/admin")]
[Authorize(Roles = "Admin")]
public class ProductAdminController(ISender sender) : ControllerBase
{
    [HttpPut("deactivate-user/{userId}")]
    public async Task<IActionResult> DeactivateUserProducts([FromRoute]Guid userId)
    {
        Console.WriteLine($"User {userId} deactivated");
        await sender.Send(new DeactivateUserProductsCommand(userId));
        return NoContent();
    }

    [HttpPut("activate-user/{userId}")]
    public async Task<IActionResult> ActivateUserProducts([FromRoute]Guid userId)
    {
        await sender.Send(new ActivateUserProductsCommand(userId));
        return NoContent();
    }
}