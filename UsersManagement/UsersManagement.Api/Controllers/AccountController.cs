using MediatR;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Api.Dto;
using UsersManagement.Application.Dto.UserDto;
using UsersManagement.Application.Dto.UserDto.AuthDto;
using UsersManagement.Application.Dto.UserDto.PasswordDto;
using UsersManagement.Application.Implementation.Commands;
using UsersManagement.Application.Implementation.Commands.AuthCommands;
using UsersManagement.Application.Implementation.Commands.PasswordTokensCommand;

namespace UsersManagement.Api.Controllers;

[Controller]
[Route("api/[controller]")]
public class AccountController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody]RegisterUserDto user)
    {
        await sender.Send(new RegisterUserCommand(user));
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody]LoginUserDto user)
    {
        var token = await sender.Send(new LoginUserCommand(user));
        return Ok(token);
    }

    [HttpGet("verify-email/{token}"), EndpointName("VerifyEmail")]
    public async Task<IActionResult> VerifyEmail([FromRoute]Guid token)
    {
        var success = await sender.Send(new VerifyEmailCommand(token));
        return success ? Ok("Email verification succeed") : BadRequest("Email verification failed");
    }

    [HttpGet("forgotPassword/request/{token}"), EndpointName("ForgotPassword")]
    public async Task<IActionResult> ResetPassword([FromRoute] Guid token)
    {
        return Ok(new { token });
    }
    
    [HttpPost("forgotPassword/request")]
    public async Task<IActionResult> RequestPasswordReset([FromBody]ResetPasswordEmailDto email)
    {
        await sender.Send(new ForgotPasswordTokenCommand(email.Email));
        return Ok("If this email exists, a reset link has been sent");
    }
    
    [HttpPost("reset-password/confirm"), EndpointName("ResetPassword")]
    public async Task<IActionResult> ResetPasswordConfirm([FromBody] ResetUserPasswordDto resetUserPasswordDto)
    {
        var success = await sender.Send(new ResetPasswordCommand(resetUserPasswordDto));
        return success ? Ok("Password was successfully changed") : BadRequest("Password reset failed");
    }
}