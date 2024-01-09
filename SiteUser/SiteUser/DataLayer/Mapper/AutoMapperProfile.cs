using AutoMapper;
using SiteUser.DataLayer.Models;
using SiteUser.DataLayer.DTO;

namespace Site.DataLayer.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<UserDTO, UserDeviceDTO>().ReverseMap();
            CreateMap<User, UserFrontendDTO>().ReverseMap();
        }
        
    }
}