using AutoMapper;

public class LoginService:ILoginService 
{
     private IGenericRepository<Personel> _genericRepo;

    public LoginService(IGenericRepository<Personel> genericRepository)
    {
        _genericRepo = genericRepository;
    }

    public async Task<bool> LoginAsync(LoginDto loginDto)
    {
        var personel = await _genericRepo.GetByNameAsync(x => x.PersonelUserName == loginDto.PersonelUserName);

        if (personel == null)
        {
            return false;
        }

        if (personel.PersonelPassword != loginDto.PersonelPassword)
        {
            return false;
        }

        return true;
    }
}

