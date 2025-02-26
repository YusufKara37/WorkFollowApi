using AutoMapper;
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
        var work = await _genericRepo.GetByIdAsync(id); 
        if (work == null)
        {
            return null;  
        }
        return _mapper.Map<WorkDto>(work);  
    }

    public async Task<WorkDto> Create(WorkDto work)
    {
        var dmoModel = _mapper.Map<Work>(work);
        var result = await _genericRepo.CreateAsync(dmoModel);
        return _mapper.Map<WorkDto>(result);
    }
    
     public async Task<WorkDto> Delete(int id)
    {
        
        var isDeleted = await _genericRepo.DeleteAsync(id);
        
        if (!isDeleted)
        {
            return null;  
        }

        
        return new WorkDto
        {
            WorkId = id,
            WorkName = "Silinen İş", 
            WorkComment = "Silinen İşin Açıklaması" 
        };
    }

    public async Task<bool> Update (UpdateWorkStage model)
    {
        var existingWork = await _genericRepo.GetByIdAsync(model.WorkId);
        if (existingWork == null)
        {
            return false;
        }
        existingWork.WorkStageId = model.StageId;
        if(model.StageId == 4)
        {
            existingWork.WorkAndDate = DateTime.Now;
        }
        if (model.StageId == 2)
        {
            existingWork.WorkAndDate = null;
        }

        return await _genericRepo.UpdateAsync(existingWork);
    }
}