using AutoMapper;
using WorkFvApi.DTO.AuthorityDTO;
using WorkFvApi.Models;


public class AuthorityMapper : Profile
{
    public AuthorityMapper()
    {
        CreateMap<Authority, AuthorityDto>().ReverseMap();
    }
}