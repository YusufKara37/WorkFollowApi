using AutoMapper;
using WorkFvApi.Models;



public class PersonelService : IPersonelService
{
    private IGenericRepository<Personel> _genericRepo;
    private IMapper _mapper;

    public PersonelService(IGenericRepository<Personel> genericRepo, IMapper mapper)
    {
        _genericRepo = genericRepo;
        _mapper = mapper;
    }
    public async Task<PersonelDto> Create(PersonelDto personel)
    {
        var dmoModel = _mapper.Map<Personel>(personel);
        var result = await _genericRepo.CreateAsync(dmoModel);
        return _mapper.Map<PersonelDto>(result);
    }

    public async Task<bool> Delete(int personelId)
    {
        return await _genericRepo.DeleteAsync(personelId);
    }

    public async Task<List<PersonelDto>> GetAllPersonels()
    {
        var dmoList = await _genericRepo.GetAllAsync();
        return _mapper.Map<List<PersonelDto>>(dmoList);
    }

    public async Task<PersonelDto> GetById(int id)
    {
        var dmoModel = await _genericRepo.GetByIdAsync(id);
        return _mapper.Map<PersonelDto>(dmoModel);
    }
    public async Task<PersonelDto> GetByName(string name)
    {
        var personel = await _genericRepo.GetByNameAsync(p => p.PersonelName == name);

        if (personel == null)
        {
            return null;
        }

        return _mapper.Map<PersonelDto>(personel);
    }

    public async Task<bool> Update(PersonelDto personelDto)
    {
       

        var existingPersonel = await _genericRepo.GetByIdAsync(personelDto.PersonelId); 
        if (existingPersonel == null)
        {
            return false; 
        }
        
        if (!string.IsNullOrEmpty(personelDto.PersonelName)) 
            existingPersonel.PersonelName = personelDto.PersonelName; 

       
        if (!string.IsNullOrEmpty(personelDto.PersonelUserName))
            existingPersonel.PersonelUserName = personelDto.PersonelUserName;

        if (!string.IsNullOrEmpty(personelDto.PersonelPassword))
            existingPersonel.PersonelPassword = personelDto.PersonelPassword;

        if (personelDto.PersonelUnitId.HasValue)
            existingPersonel.PersonelUnitId = personelDto.PersonelUnitId.Value;

        if (personelDto.PersonelAuthoritesId != 0) 
            existingPersonel.PersonelAuthoritesId = personelDto.PersonelAuthoritesId;

        return await _genericRepo.UpdateAsync(existingPersonel); 
    }
}