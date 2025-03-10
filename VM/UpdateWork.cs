namespace WorkFvApi.VM
{
    public class UpdateWork
    {
        public int WorkId { get; set; }  
        public string? WorkName { get; set; }
        public string? WorkComment { get; set; }
        public int? WorkPersonelId { get; set; }
        public DateTime? WorkStartDate { get; set; }
        public DateTime? WorkAndDate { get; set; }
        public string? PdfUrl { get; set; }
        public int? WorkStageId { get; set; }
    }
}
