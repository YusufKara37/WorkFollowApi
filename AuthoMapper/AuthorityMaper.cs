using AutoMapper;
using WorkFvApi.DTO.AuthorityDTO;


public class AuthorityMapper : Profile
{
    public AuthorityMapper()
    {
        CreateMap<Authority, AuthorityDto>().ReverseMap();
    }
}