using WorkFvApi.Data;
using WorkFvApi.Models;

public class UnitService:EntityService<Unit>,IUnitService
{
    public UnitService(ApplicationDbContext context): base(context)
    {

    }
}