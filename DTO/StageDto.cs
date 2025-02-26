
using WorkFvApi.Models;


namespace WorkFvApi.DTO.StageDTO
{
    public class StageDTo
    {
        public int StageId { get; set; }

        public string? StageName { get; set; }

        public virtual ICollection<Work> Works { get; set; } = new List<Work>();

    }
}