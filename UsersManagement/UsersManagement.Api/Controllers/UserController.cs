using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Api.Extensions;
using UsersManagement.Application.Dto.UserDto;
using UsersManagement.Application.Dto.UserDto.PasswordDto;
using UsersManagement.Application.Dto.UserDto.ProfileDto;
using UsersManagement.Application.Implementation.Commands;
using UsersManagement.Application.Implementation.Commands.UserCommands;
using UsersManagement.Application.Implementation.Queries;
using UsersManagement.Application.Implementation.Queries.User;

namespace UsersManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(ISender sender) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await sender.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await sender.Send(new GetUserByIdQuery(id));
        return Ok(user);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserProfileDto updateUserProfileDto, [FromRoute] Guid id)
    {
        var returnedId = await sender.Send(new UpdateUserCommand(id, updateUserProfileDto));
        return CreatedAtAction(nameof(GetById), new { id = returnedId }, null);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        await sender.Send(new DeleteUserCommand(id));
        return NoContent();
    }

    [HttpPut("{id}/{isActive}/change-status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatusOffUserActivity([FromRoute] Guid id, [FromRoute] bool isActive)
    {
        await sender.Send(new ChangeStatusOfActivityUserCommand(id, isActive));
        return NoContent();
    }
    
    [HttpPut("profile")]
    [Authorize(Roles = "Admin, Seller")]
    public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileDto updateUserProfileDto)
    {
        var userId = User.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        await sender.Send(new UpdateUserCommand(userId, updateUserProfileDto));
        return NoContent();
    }

    [HttpPut("profile/password")]
    [Authorize(Roles = "Admin, Seller")]
    public async Task<IActionResult> UpdateUserPassword([FromBody] UpdateUserPasswordDto updateUserPasswordDto)
    {
        var userId = User.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        await sender.Send(new UpdatePasswordCommand(userId, updateUserPasswordDto));
        return NoContent();
    }
    
    [HttpDelete("profile")]
    [Authorize(Roles = "Admin, Seller")]
    public async Task<IActionResult> DeleteUser()
    {
        var userId = User.GetUserId();
        if (userId == Guid.Empty) return Unauthorized();
        
        await sender.Send(new DeleteUserCommand(userId));
        
        return NoContent(); 
    }
}