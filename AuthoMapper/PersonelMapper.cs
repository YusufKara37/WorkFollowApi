using AutoMapper;
using WorkFvApi.DTO.PersonelDTO;
using WorkFvApi.Models;

public class PersonelMapper : Profile
{

    public PersonelMapper()
    {
        CreateMap<Personel,PersonelDto>().ReverseMap();
    }

}