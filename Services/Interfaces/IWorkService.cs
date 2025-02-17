using WorkFvApi.DTO.WorkDTO;


public interface IWorkService
{
    Task<List<WorkDto>> GetAllWork();
    Task<WorkDto> GetById(int id);
    Task<WorkDto> Create(WorkDto work);


}