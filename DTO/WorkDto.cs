using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFvApi.Models;


namespace WorkFvApi.DTO.WorkDTO
{
    public class WorkDto
    {
        public int WorkId { get; set; }

        public string WorkName { get; set; } = null!;

        public string? WorkComment { get; set; }

        public int? WorkPersonelId { get; set; }

        public DateTime? WorkStartDate { get; set; }

        public DateTime? WorkAndDate { get; set; }

        public int? WorkStageId { get; set; }

        public virtual Personel? WorkPersonel { get; set; }

        public virtual Stage? WorkStage { get; set; }

    }
}