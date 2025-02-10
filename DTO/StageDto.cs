using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFvApi.Models;


namespace WorkFvApi.DTO.StageDTO
{
    public class StageDto
    {
        public int StageId { get; set; }

        public string? StageName { get; set; }

        public virtual ICollection<Work> Works { get; set; } = new List<Work>();
        
    }
}