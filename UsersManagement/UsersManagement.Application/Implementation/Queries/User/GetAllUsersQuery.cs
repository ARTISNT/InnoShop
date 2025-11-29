using MediatR;
using UsersManagement.Application.Dto.UserDto.ProfileDto;

namespace UsersManagement.Application.Implementation.Queries.User;

public record GetAllUsersQuery() : IRequest<IReadOnlyCollection<UserResponseDto>>;