using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFvApi.Models;



namespace WorkFvApi.DTO.PersonelDTO
{
    public class PersonelDto
    {
        public int PersonelId { get; set; }

        public string? PersonelName { get; set; }

        public string? PersonelUserName { get; set; }

        public int? PersonelUnitId { get; set; }

        public int PersonelAuthoritesId { get; set; }

        public virtual Authority PersonelAuthorites { get; set; } = null!;

        public virtual Unit? PersonelUnit { get; set; }

        public virtual ICollection<Work> Works { get; set; } = new List<Work>();
        
    }
}