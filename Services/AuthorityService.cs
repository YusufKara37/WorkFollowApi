using WorkFvApi.Data;
using WorkFvApi.Models;

public class AuthorityService : EntityService<Authority>, IAuthorityService
{
    public AuthorityService(ApplicationDbContext context) : base(context)
    {
    }
}
