using AutoMapper;
using UsersManagement.Application.Dto.UserDto;
using UsersManagement.Application.Dto.UserDto.ProfileDto;
using UsersManagement.Domain.Models.Entities;

namespace UsersManagement.Application.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserEntity, UserResponseDto>();
        CreateMap<UpdateUserProfileDto, UserEntity>();
    }
}