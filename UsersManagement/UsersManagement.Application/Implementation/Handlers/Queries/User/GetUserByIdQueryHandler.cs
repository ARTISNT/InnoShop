using AutoMapper;
using MediatR;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Dto.UserDto;
using UsersManagement.Application.Dto.UserDto.ProfileDto;
using UsersManagement.Application.Implementation.Queries;
using UsersManagement.Application.Implementation.Queries.User;

namespace UsersManagement.Application.Implementation.Handlers.Queries.User;

public class GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper) 
    : IRequestHandler<GetUserByIdQuery, UserResponseDto>
{
    public async Task<UserResponseDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id);
        if (user is null)
            throw new KeyNotFoundException($"User with id {request.Id} was not found");
        
        return mapper.Map<UserResponseDto>(user);
    }
}