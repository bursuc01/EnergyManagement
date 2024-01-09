using AutoMapper;
using SiteMonitor.DataLayer.DTO;
using SiteMonitor.DataLayer.Models;

namespace SiteMonitor.DataLayer.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<EnergyConsumption, EnergyConsumptionDTO>().ReverseMap();
    }
}