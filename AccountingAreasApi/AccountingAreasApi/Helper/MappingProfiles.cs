using AccountingAreasApi.Dto;
using AccountingAreasApi.Models;
using AutoMapper;

namespace AccountingAreasApi.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Area, AreaDto>();
        CreateMap<AreaDto, Area>();
        CreateMap<Region, RegionDto>();
        CreateMap<RegionDto, Region>();
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
    }
}