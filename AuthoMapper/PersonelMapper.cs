using AutoMapper;
using WorkFvApi.Models;


public class PersonelMapper : Profile
{

    public PersonelMapper()
    {
        CreateMap<Personel, PersonelDto>().ReverseMap();
        CreateMap<PersonelDto, CreatePersonelVM>().ReverseMap();
    }

}