using AutoMapper;
using WorkFvApi.DTO.StageDTO;
using WorkFvApi.Models;

public class StageMapper : Profile
{
    public StageMapper()
    {
        CreateMap<Stage, StageDTo>().ReverseMap();

    }
}