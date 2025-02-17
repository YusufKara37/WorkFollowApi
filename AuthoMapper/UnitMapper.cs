using AutoMapper;
using WorkFvApi.DTO.UnitDTO;


public class UnitMapper : Profile
{
    public UnitMapper()
    {
        CreateMap<Unit, UnitDto>().ReverseMap();
    }
}