using WorkFvApi.Data;
using WorkFvApi.Models;

public class StageSerevice:EntityService<Stage>,IStageService
{
    public StageSerevice(ApplicationDbContext context): base(context)
    {}
}