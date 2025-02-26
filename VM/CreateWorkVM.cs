

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class CreateWorkVM
{
    [Required]
    public string WorkName { get; set; } = null!;

    public string? WorkComment { get; set; }

    public int? WorkStageId { get; set; } = 3;

    public DateTime WorkStartDate { get; set; } = DateTime.Now;

    public DateTime? WorkAndDate { get; set; } = null;

    public IFormFile? File { get; set; } 
}
