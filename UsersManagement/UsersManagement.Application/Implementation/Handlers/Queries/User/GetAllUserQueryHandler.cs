using AutoMapper;
using MediatR;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Dto.UserDto;
using UsersManagement.Application.Dto.UserDto.ProfileDto;
using UsersManagement.Application.Implementation.Queries;
using UsersManagement.Application.Implementation.Queries.User;

namespace UsersManagement.Application.Implementation.Handlers.Queries.User;

public class GetAllUserQueryHandler(IUserRepository userRepository, IMapper mapper) 
    : IRequestHandler<GetAllUsersQuery, IReadOnlyCollection<UserResponseDto>>
{
    public async Task<IReadOnlyCollection<UserResponseDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync();
        if(users is null)
            throw new KeyNotFoundException("Users not found");

        return mapper.Map<IReadOnlyCollection<UserResponseDto>>(users);
    }
}