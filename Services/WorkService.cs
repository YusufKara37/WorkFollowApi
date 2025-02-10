using WorkFvApi.Data;
using WorkFvApi.Models;

public class WorkService:EntityService<Work>,IWorkService
{
    public WorkService(ApplicationDbContext context): base(context)
    {
        
    }
}