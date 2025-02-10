using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkFvApi.Data;
using WorkFvApi.DTO.PersonelDTO;
using WorkFvApi.Models;

namespace WorkFvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonelController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IPersonelService _personelService;

        public PersonelController(IMapper mapper, IPersonelService personelService)
        {

            _mapper = mapper;
            _personelService = personelService;

        }

        // GET: api/Personel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonelDto>>> GetPersonel()
        {
            var personel = await _personelService.GetAllAsync();

            // Veritabanında hiç personel yoksa NotFound dön
            if (personel == null || !personel.Any())
            {
                return NotFound("Personel bulunamadı.");
            }

            var personelDto = _mapper.Map<List<PersonelDto>>(personel);
            return Ok(personelDto);
        }

        // GET: api/Personel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonelDto>> GetPersonel(int id)
        {
            var personel = await _personelService.GetByIdAsync(id);

            if (personel == null)
            {
                return NotFound($"ID {id} olan personel bulunamadı.");
            }

            var personelDto = _mapper.Map<PersonelDto>(personel);
            return Ok(personelDto);
        }

        // PUT: api/Personel/5

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonel(int id, [FromBody] Personel personel)
        {
            // URL'deki id ile gelen nesnenin ID'si eşleşmeli
            if (id != personel.PersonelId)
            {
                return BadRequest("Güncellenmek istenen personelin ID'si uyuşmuyor.");
            }

            // Güncellenecek personelin var olup olmadığını kontrol et
            var existingPersonel = await _personelService.GetByIdAsync(id);
            if (existingPersonel == null)
            {
                return NotFound("Güncellenmek istenen personel bulunamadı.");
            }

            // Mevcut personel bilgilerini güncelle
            existingPersonel.PersonelName = personel.PersonelName;
            existingPersonel.PersonelUserName = personel.PersonelUserName;
            existingPersonel.PersonelUnitId = personel.PersonelUnitId;
            existingPersonel.PersonelAuthoritesId = personel.PersonelAuthoritesId;
            existingPersonel.PersonelAuthorites = personel.PersonelAuthorites;
            existingPersonel.PersonelUnit = personel.PersonelUnit;
            existingPersonel.Works = personel.Works;

            // Güncelleme işlemi
            await _personelService.UpdateAsync(existingPersonel);

            return NoContent();
        }

        // POST: api/Personel

        [HttpPost]
        public async Task<ActionResult<Personel>> PostPersonel([FromBody] Personel personel)
        {
            var createdPersonel = await _personelService.CreateAsync(personel);

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
            var personel = await _personelService.DeleteAsync(id);

            if (personel == null)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
