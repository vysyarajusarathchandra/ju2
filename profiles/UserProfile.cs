using AutoMapper;
using ju.Dto;
using ju.entity;

namespace ju.profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() { 
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
      }
    }
}
