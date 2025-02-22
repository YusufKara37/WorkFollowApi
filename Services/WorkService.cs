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
    
     public async Task<WorkDto> Delete(int id)
    {
        // Silme işlemini repository üzerinden yapıyoruz
        var isDeleted = await _genericRepo.DeleteAsync(id);
        
        if (!isDeleted)
        {
            return null;  // İş bulunamadıysa null döneriz
        }

        // Silme başarılı olduğunda, DTO dönüyoruz
        // Burada, silinen işin ID'si ile işin geri dönüşünü sağlayabilirsiniz
        return new WorkDto
        {
            WorkId = id,
            WorkName = "Silinen İş", // veya silinen işin adı
            WorkComment = "Silinen İşin Açıklaması" // veya silinen işin açıklaması
        };
    }
}