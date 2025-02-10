using WorkFvApi.Data;
using WorkFvApi.Models;

public class PersonelService:EntityService<Personel>,IPersonelService
{
    public PersonelService(ApplicationDbContext context): base(context)
    {

    }
}