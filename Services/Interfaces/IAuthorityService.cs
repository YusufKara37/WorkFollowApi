using WorkFvApi.DTO.AuthorityDTO;
using WorkFvApi.Models;

public interface IAuthorityService
{
    Task<List<AuthorityDto>> GetAllAuthority();
    Task<AuthorityDto> GetByAuthority(int id);
  Task<AuthorityDto> Create(AuthorityDto authorityDto);
    Task<bool> UpdateAuthorityAsync(int id, AuthorityDto dto);
   
}
