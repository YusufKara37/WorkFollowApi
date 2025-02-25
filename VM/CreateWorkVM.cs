

public class CreateWorkVM
{
    public string WorkName { get; set; } = null!;
    public string? WorkComment { get; set; }
    public int? WorkStageId { get; set; } = 3;
    public DateTime WorkStartDate { get; set; } = DateTime.Now;
    public DateTime? WorkAndDate { get; set; } =null;

}