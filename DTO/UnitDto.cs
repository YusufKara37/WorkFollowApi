using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFvApi.Models;



namespace WorkFvApi.DTO.UnitDTO
{
    public class UnitDto
    {
        public int UnitId { get; set; }

        public string? UnitName { get; set; }

        public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();
        
    }
}