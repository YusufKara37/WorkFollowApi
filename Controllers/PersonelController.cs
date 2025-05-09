
using AutoMapper;
using Microsoft.AspNetCore.Mvc;



namespace WorkFvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonelController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IPersonelService _personelService;
        private readonly ILoginService _loginService;

        public PersonelController(IMapper mapper, IPersonelService personelService, ILoginService loginService)
        {

            _mapper = mapper;
            _personelService = personelService;
            _loginService = loginService;

        }

        // GET: api/Personel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonelDto>>> GetPersonel()
        {
            var personel = await _personelService.GetAllPersonels();

            // Veritabanında hiç personel yoksa NotFound dön
            if (personel == null || !personel.Any())
            {
                return NotFound("Personel bulunamadı.");
            }

            var personelDto = _mapper.Map<List<PersonelDto>>(personel);
            return Ok(personelDto);
        }

        // GET: api/Personel/5
        [HttpGet("get/{id}")]
        public async Task<ActionResult<PersonelDto>> GetPersonel(int id)
        {
            var personel = await _personelService.GetById(id);

            if (personel == null)
            {
                return NotFound($"ID {id} olan personel bulunamadı.");
            }

            var personelDto = _mapper.Map<PersonelDto>(personel);
            return Ok(personelDto);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var personel = await _personelService.GetByName(name);
            if (personel == null)
            {
                return NotFound($"'{name}' isminde bir personel bulunamadı.");
            }
            return Ok(personel);
        }

        // PUT: api/Personel/5

        [HttpPatch]
        public async Task<IActionResult> UpdatePersonel([FromBody] PersonelDto personel)
        {
            // Güncellenecek personelin var olup olmadığını kontrol et
            var existingPersonel = await _personelService.GetById(personel.PersonelId);
            if (existingPersonel == null)
            {
                return NotFound("Güncellenmek istenen personel bulunamadı.");
            }

            if (personel.PersonelName != null) existingPersonel.PersonelName = personel.PersonelName;
            if (personel.PersonelUserName != null) existingPersonel.PersonelUserName = personel.PersonelUserName;
            if (personel.PersonelPassword != null) existingPersonel.PersonelPassword = personel.PersonelPassword;
            if (personel.PersonelUnitId != 0) existingPersonel.PersonelUnitId = personel.PersonelUnitId;
            if (personel.PersonelAuthoritesId != 0) existingPersonel.PersonelAuthoritesId = personel.PersonelAuthoritesId;


            // Güncelleme işlemi
            var result = await _personelService.Update(existingPersonel);

            if (result)
            {
                return Ok("Kayit Basarili");
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Personel güncellenirken bir hata oluştu.");

        }

        // POST: api/Personel
        [HttpPost("login")]
        public async Task<IActionResult> PostLogin([FromBody] LoginDto personel)
        {
            var dtoModel=_mapper.Map<LoginDto>(personel);
            dtoModel.PersonelPassword=HashHelper.HashPassword(dtoModel.PersonelPassword);
            var result = await _loginService.LoginAsync(dtoModel);
            if (result == true){
                return Ok("Giris Basar");
            }
            return BadRequest("Basarisiz");

        
            
        }
        [HttpPost]
        public async Task<IActionResult> PostPersonel([FromBody] CreatePersonelVM personel)
        {
            var dtoModel = _mapper.Map<PersonelDto>(personel);
            dtoModel.PersonelPassword = HashHelper.HashPassword(dtoModel.PersonelPassword);
            var createdPersonel = await _personelService.Create(dtoModel);

            if (createdPersonel == null)
            {
                return BadRequest("Personel oluşturulamadı.");
            }

            return CreatedAtAction(nameof(GetPersonel), new { id = createdPersonel.PersonelId }, createdPersonel);
        }

        // DELETE: api/Personel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonel(int id)
        {
            var personel = await _personelService.Delete(id);

            if (personel)
            {
                return Ok("Silme Islemi Basarili");
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Personel güncellenirken bir hata oluştu.");
        }


    }
}
