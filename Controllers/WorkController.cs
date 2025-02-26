using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WorkFvApi.DTO.WorkDTO;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.IO;


namespace WorkFvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IWorkService _workService;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public WorkController(ApplicationDbContext context, IMapper mapper, IWorkService workService, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _workService = workService;
            _environment = environment;
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

            var deletedWork = await _workService.Delete(id);

            if (deletedWork == null) // Eğer iş silinemediyse
            {
                return NotFound(new { message = "İş bulunamadı." });
            }


            return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateStage(UpdateWorkStage model)
        {
            var isOk = await _workService.Update(model);
            if (isOk)
            {
                return Ok("Guncellendir");
            }
            else
            {
                return BadRequest("HATA");
            }
        }

        [HttpPost("create-with-file")]
        public async Task<IActionResult> CreateWorkWithFile([FromForm] CreateWorkVM model)
        {
            string? fileUrl = null;

            if (model.File != null && model.File.Length > 0)
            {
                if (Path.GetExtension(model.File.FileName).ToLower() != ".pdf")
                {
                    return BadRequest("Sadece PDF dosyaları yüklenebilir.");
                }

                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}.pdf";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }

                fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{uniqueFileName}";
            }

            // 📌 İş oluşturma işlemi
            var newWork = new Models.Work
            {
                WorkName = model.WorkName,
                WorkComment = model.WorkComment,
                WorkStageId = model.WorkStageId ?? 3,
                WorkStartDate = model.WorkStartDate,
                WorkAndDate = model.WorkAndDate,
                PdfUrl = fileUrl // 📌 PDF URL'sini ekledik
            };

            _context.Works.Add(newWork);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "İş başarıyla eklendi!",
                workId = newWork.WorkId,
                pdfUrl = fileUrl
            });
        }



    }
}
