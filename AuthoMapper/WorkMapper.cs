using AutoMapper;
using WorkFvApi.DTO.WorkDTO;
using WorkFvApi.Models;

public class WorkMapper : Profile
{
    public WorkMapper()
    {
        CreateMap<Work, WorkDto>().ReverseMap();
        
    }
    
}


