

public class CreateWorkVM
{
    public string WorkName { get; set; } = null!;
    public string? WorkComment { get; set; }
    public DateTime WorkStartDate { get; set; } = DateTime.Now;
    public DateTime? WorkAndDate { get; set; }

}