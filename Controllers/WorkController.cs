using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkDto>> GetWorkById(int id)
        {
            var work = await _workService.GetById(id);
            if (work == null)
            {
                return NotFound();  
            }
            return Ok(work);  
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
            // Silme işlemi
            var deletedWork = await _workService.Delete(id);

            if (deletedWork == null) // Eğer iş silinemediyse
            {
                return NotFound(new { message = "İş bulunamadı." });
            }

            // Silme işlemi başarılı, 204 NoContent dönülür
            return NoContent();
        }



    }
}
