using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using ProductsManagement.Application.Abstractions;

namespace ProductsManagement.Application.Implementation.Services;

public class UserContext(IHttpContextAccessor contextAccessor) : IUserContext
{
    public Guid GetUserId()
    {
        var user = contextAccessor.HttpContext?.User;
        var claim = user?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(claim) || !Guid.TryParse(claim, out var userId))
            throw new SecurityTokenException("Invalid token: NameIdentifier claim missing");

        return userId;
    }

    public void EnsureOwnerOrAdmin(Guid ownerId)
    {
        if(IsAdmin())
            return;
        
        var userId = GetUserId();
        if (userId != ownerId)
            throw new UnauthorizedAccessException("You are not authorized to access this resource");
    }

    private bool IsAdmin()
    {
        var role = contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
        return role == "Admin";
    }
}