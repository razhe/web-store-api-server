using AutoMapper;
using web_store_server.Domain.Dtos.Users;
using web_store_server.Domain.Entities;

namespace web_store_server.Domain.Profiles
{
    public class UserProfile : 
        Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserDto>();

            CreateMap<CreateUpdateUserDto, User>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
