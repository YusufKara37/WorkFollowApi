
using WorkFvApi.Models;

public interface IPersonelService
{
    Task<List<PersonelDto>> GetAllPersonels();
    Task<PersonelDto> GetById(int id);
    Task<PersonelDto> GetByName(string name);
    Task<bool> Update(PersonelDto personel);
    Task<PersonelDto> Create(PersonelDto personel);
    Task<bool> Delete(int personelId);
    
}