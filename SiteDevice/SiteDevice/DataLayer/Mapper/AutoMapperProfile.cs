using AutoMapper;
using SiteDevice.DataLayer.DTO;
using SiteDevice.DataLayer.Models;

namespace SiteDevice.DataLayer.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Device, DeviceDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Device, DeviceGetDTO>().ReverseMap();
    }
}