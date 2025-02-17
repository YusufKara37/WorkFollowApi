using AutoMapper;
using WorkFvApi.DTO.StageDTO;

public class StageMapper : Profile
{
    public StageMapper()
    {
        CreateMap<Stage, StageDto>().ReverseMap();

    }
}