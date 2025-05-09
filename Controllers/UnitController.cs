
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkFvApi.Models;


namespace WorkFvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitService _unitService;
        private readonly IMapper _mapper;

        public UnitController(ApplicationDbContext context, IUnitService unitService, IMapper mapper)
        {
            _context = context;
            _unitService = unitService;
            _mapper = mapper;
        }

        // GET: api/Unit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
            return await _context.Units.ToListAsync();
        }

        // GET: api/Unit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(int id)
        {
            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return unit;
        }

        // PUT: api/Unit/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit(int id, Unit unit)
        {
            if (id != unit.UnitId)
            {
                return BadRequest();
            }

            _context.Entry(unit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitExists(id))
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

        // POST: api/Unit
        
        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(Unit unit)
        {
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnit", new { id = unit.UnitId }, unit);
        }

        // DELETE: api/Unit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnitExists(int id)
        {
            return _context.Units.Any(e => e.UnitId == id);
        }
    }
}
