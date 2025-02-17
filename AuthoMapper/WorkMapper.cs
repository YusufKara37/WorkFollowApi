using AutoMapper;
using WorkFvApi.DTO.WorkDTO;
using WorkFvApi.Models;

public class WorkMapper : Profile
{
    public WorkMapper()
    {
        CreateMap<Work, WorkDto>().ReverseMap();
        CreateMap<WorkDto,CreatePersonelVM>().ReverseMap();
        CreateMap<CreateWorkVM, WorkDto>();
        CreateMap<WorkDto, Work>();
        CreateMap<Work, WorkDto>();
        
    }
    
}


