using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFvApi.Models;



namespace WorkFvApi.DTO.AuthorityDTO
{
    public class AuthorityDto
    {
         public int AuthoritiesId { get; set; }

        public string? AuthoritesName { get; set; }

        public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();
        
    }
}