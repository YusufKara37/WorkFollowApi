using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkFvApi.DTO.WorkDTO;


namespace WorkFvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IWorkService _workService;

        public WorkController(ApplicationDbContext context, IMapper mapper, IWorkService workService)
        {
            _mapper = mapper;
            _workService = workService;
        }

        // GET: api/Work
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkDto>>> GetWorks()
        {
            var work = await _workService.GetAllWork();
            if (work == null || !work.Any())
            {
                return NotFound("Work bulunamadı");
            }
            var WorkDto = _mapper.Map<List<WorkDto>>(work);
            return Ok(WorkDto);

        }

        // GET: api/Work/5
        [HttpGet("get/{id}")]
        public async Task<ActionResult<WorkDto>> GetWork(int id)
        {
            var work = await _workService.GetById(id);
            if (work == null)
            {
                return NotFound($"ID {id} olan work bulunamadı.");
            }
            var workDto = _mapper.Map<WorkDto>(work);
            return Ok(workDto);
        }
        [HttpPost]
        public async Task<ActionResult<WorkDto>> PostWork([FromBody] CreateWorkVM work)
        {
            var dtoModel = _mapper.Map<WorkDto>(work);
            var createdWork = await _workService.Create(dtoModel);

            if (createdWork == null)
            {
                return BadRequest("Work oluşturalamı.");
            }
            return Ok("Work oluşturuldu.");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWork(int id)
        {
            var deletedWork = await _workService.Delete(id);
            if (deletedWork == null)
            {
                return NotFound(new { message = "İş bulunamadı." });
            }
            return Ok(deletedWork);
        }


    }
}
