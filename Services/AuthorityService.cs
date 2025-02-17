using AutoMapper;

using WorkFvApi.DTO.AuthorityDTO;


public class AuthorityService : IAuthorityService
{
    private IGenericRepository<Authority> _genericRepo;
    private IMapper _mapper;

    public AuthorityService(IGenericRepository<Authority> genericRepository, IMapper mapper)
    {
        _genericRepo = genericRepository;
        _mapper = mapper;
    }

    public async Task<List<AuthorityDto>> GetAllAuthority()
    {
        var authorities = await _genericRepo.GetAllAsync();
        return _mapper.Map<List<AuthorityDto>>(authorities);
    }

    public async Task<AuthorityDto> GetByAuthority(int id)
    {
        var authority = await _genericRepo.GetByIdAsync(id);
        return _mapper.Map<AuthorityDto>(authority);
    }

    public async Task<AuthorityDto> Create(AuthorityDto authorityDto)
    {
        var authority = _mapper.Map<Authority>(authorityDto);
        await _genericRepo.CreateAsync(authority);
        var createdAuthorityDto = _mapper.Map<AuthorityDto>(authority);
        return createdAuthorityDto;
    }

    public async Task<bool> UpdateAuthorityAsync(int id, AuthorityDto dto)
    {
        var existingAuthority = await _genericRepo.GetByIdAsync(id);
        if (existingAuthority == null) return false;

        _mapper.Map(dto, existingAuthority);
        return await _genericRepo.UpdateAsync(existingAuthority);
    }


}


