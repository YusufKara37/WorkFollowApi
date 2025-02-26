
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkFvApi.Models;


namespace WorkFvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStageService _stageService;

        public StageController(ApplicationDbContext context, IMapper mapper, IStageService stageService)
        {
            _context = context;
            _mapper = mapper;
            _stageService = stageService;
        }

        // GET: api/Stage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stage>>> GetStages()
        {
            return await _context.Stages.ToListAsync();
        }

        // GET: api/Stage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stage>> GetStage(int id)
        {
            var stage = await _context.Stages.FindAsync(id);

            if (stage == null)
            {
                return NotFound();
            }

            return stage;
        }

        // PUT: api/Stage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStage(int id, Stage stage)
        {
            if (id != stage.StageId)
            {
                return BadRequest();
            }

            _context.Entry(stage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Stage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stage>> PostStage(Stage stage)
        {
            _context.Stages.Add(stage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStage", new { id = stage.StageId }, stage);
        }

        // DELETE: api/Stage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStage(int id)
        {
            var stage = await _context.Stages.FindAsync(id);
            if (stage == null)
            {
                return NotFound();
            }

            _context.Stages.Remove(stage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StageExists(int id)
        {
            return _context.Stages.Any(e => e.StageId == id);
        }
    }
}
