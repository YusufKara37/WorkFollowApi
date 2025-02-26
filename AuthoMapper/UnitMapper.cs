using AutoMapper;
using WorkFvApi.DTO.UnitDTO;
using WorkFvApi.Models;


public class UnitMapper : Profile
{
    public UnitMapper()
    {
        CreateMap<Unit, UnitDto>().ReverseMap();
    }
}