


namespace WorkFvApi.DTO.WorkDTO
{
    public class WorkDto
    {
        public int WorkId { get; set; }

        public string WorkName { get; set; } = null!;

        public string? WorkComment { get; set; }

        public int? WorkPersonelId { get; set; }

        public DateTime? WorkStartDate { get; set; }

        public DateTime? WorkAndDate { get; set; } = null;

        public int? WorkStageId { get; set; }

    }
}