using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkFvApi.Data;
using WorkFvApi.DTO.AuthorityDTO;
using WorkFvApi.Models;

namespace WorkFvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        private readonly IMapper _mapper;
        
       private readonly IAuthorityService _authorityService;
        

        public AuthorityController(IMapper mapper, IAuthorityService authorityService)
        {
            
            _mapper = mapper;
            _authorityService = authorityService;
        }

        // GET: api/Authority
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorityDto>>> GetAuthorities()
        {
            var authority = await _authorityService.GetAllAsync();

            // Veritabanında hiç personel yoksa NotFound dön
            if (authority == null || !authority.Any())
            {
                return NotFound("Personel bulunamadı.");
            }

            var personelDto = _mapper.Map<List<AuthorityMapper>>(authority);
            return Ok(personelDto);
        }

        // GET: api/Authority/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorityDto>> GetAuthority(int id)
        {
            var authority = await _authorityService.GetByIdAsync(id);

            if (authority == null)
            {
                return NotFound($"ID {id} olan authority bulunamadı.");
            }

            var authorityDto = _mapper.Map<AuthorityDto>(authority);
            return Ok(authorityDto);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthority(int id,[FromBody] Authority authority)
        {
            if (id != authority.AuthoritiesId)
            {
                return BadRequest("Güncellenmek istenen authority ID'si uyuşmuyor.");
            }

            // Personel veritabanında var mı?
            var existingAuthority = await _authorityService.GetByIdAsync(id);
            if (existingAuthority == null)
            {
                return NotFound("Güncellenmek istenen personel bulunamadı.");
            }

            // Güncelleme işlemi
            var isUpdated = await _authorityService.UpdateAsync(authority);
            if (!isUpdated)
            {
                return StatusCode(500, "Authority güncellenirken bir hata oluştu.");
            }

            return NoContent();
            
        }

       
        [HttpPost]
        public async Task<ActionResult<Authority>> PostAuthority([FromBody] Authority authority)
        {
            var createdAuthority = await _authorityService.CreateAsync(authority);

            if (createdAuthority == null)
            {
                return BadRequest("Authority oluşturulamadı.");
            }

            return CreatedAtAction(nameof(GetAuthorities), new { id = createdAuthority.AuthoritiesId }, createdAuthority);
        }

        // DELETE: api/Authority/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthority(int id)
        {
            var authority = await _authorityService.DeleteAsync(id);

            if (authority == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
