using System.Net.Http.Headers;
using MediatR;
using Microsoft.AspNetCore.Http;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Implementation.Commands.UserCommands;

namespace UsersManagement.Application.Implementation.Handlers.Commands.UserCommandsHandler;

public class ChangeStatusOfActivityUserCommandHandler(
    IUserRepository userRepository, 
    HttpClient client,
    IHttpContextAccessor httpContextAccessor) 
    : IRequestHandler<ChangeStatusOfActivityUserCommand>
{
    public async Task Handle(ChangeStatusOfActivityUserCommand request, CancellationToken cancellationToken)
    {  
        var token = httpContextAccessor.HttpContext!.Request.Headers["Authorization"].ToString();
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));
        }
        
        if(request.Id == Guid.Empty)
            throw new ArgumentNullException("Request Id cannot be empty", nameof(request.Id));
        
        await userRepository.ChangeStatusOfActivityAsync(request.Id, request.IsActive);
        if (!request.IsActive)
        { 
            await client.PutAsync($"http://localhost:5197/api/products/admin/deactivate-user/{request.Id}", null);
        }
        else
        { 
            await client.PutAsync($"http://localhost:5197/api/products/admin/activate-user/{request.Id}", null);
        }
    }
}