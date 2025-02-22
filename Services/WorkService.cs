using AutoMapper;
using WorkFvApi.DTO.WorkDTO;


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
        var dmoModel = _mapper.Map<Work>(work);
        var result = await _genericRepo.CreateAsync(dmoModel);
        return _mapper.Map<WorkDto>(result);
    }
    
    public async Task<WorkDto> Delete(int workId)
    {
        // Önce ilgili işi veri tabanından bulalım
        var work = await _genericRepo.GetByIdAsync(workId);
        if (work == null)
        {
            return null; // Eğer iş bulunamazsa null döndür
        }

        // Silme işlemini gerçekleştir
        await _genericRepo.DeleteAsync(workId);

        // Silinen işi DTO'ya çevirip geri döndür
        var deletedWorkDto = _mapper.Map<WorkDto>(work);
        return deletedWorkDto;
    }
}