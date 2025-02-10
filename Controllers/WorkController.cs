using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkFvApi.Data;
using WorkFvApi.DTO.WorkDTO;
using WorkFvApi.Models;

namespace WorkFvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWorkService _workService;

        public WorkController(ApplicationDbContext context, IMapper mapper,IWorkService workService)
        {
            _mapper=mapper;
            _context = context;
            _workService = workService;
        }

        // GET: api/Work
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Work>>> GetWorks()
        {
            return await _context.Works.ToListAsync();
        }

        // GET: api/Work/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Work>> GetWork(int id)
        {
            var work = await _context.Works.FindAsync(id);

            if (work == null)
            {
                return NotFound();
            }

            return work;
        }

        // PUT: api/Work/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWork(int id, Work work)
        {
            if (id != work.WorkId)
            {
                return BadRequest();
            }

            _context.Entry(work).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkExists(id))
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

        // POST: api/Work
        [HttpPost]
        public async Task<ActionResult<WorkDto>> PostWork(Work work)
        {
            _context.Works.Add(work);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WorkExists(work.WorkId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWork", new { id = work.WorkId }, work);
        }

   
        

        // DELETE: api/Work/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWork(int id)
        {
            var work = await _context.Works.FindAsync(id);
            if (work == null)
            {
                return NotFound();
            }

            _context.Works.Remove(work);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkExists(int id)
        {
            return _context.Works.Any(e => e.WorkId == id);
        }
    }
}
