using AutoMapper;
using WorkFvApi.Data;
using WorkFvApi.DTO.WorkDTO;
using WorkFvApi.Models;

public class WorkService : IWorkService
{
    private IGenericRepository<Work> _genericRepo;
    private IMapper _mapper;

    public WorkService(IGenericRepository<Work> genericRepo, IMapper mapper)
    {
        _genericRepo = genericRepo;
        _mapper = mapper;
    }
    public async Task<List<WorkDto>> GetAllWork()
    {
        var dmoList = await _genericRepo.GetAllAsync();
        return _mapper.Map<List<WorkDto>>(dmoList);
    }
    public async Task<WorkDto> GetById(int id)
    {
        var dmoModel = await _genericRepo.GetByIdAsync(id);
        return _mapper.Map<WorkDto>(dmoModel);
    }
   
   public async Task<WorkDto> Create(WorkDto work)
   {
    var dmoModel=_mapper.Map<Work>(work);
    var result = await _genericRepo.CreateAsync(dmoModel);
    return _mapper.Map<WorkDto>(result);
   }
}