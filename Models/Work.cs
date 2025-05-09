﻿
namespace WorkFvApi.Models;

public partial class Work
{
    public int WorkId { get; set; }

    public string WorkName { get; set; } = null!;

    public string? WorkComment { get; set; }

    public int? WorkPersonelId { get; set; }

    public DateTime? WorkStartDate { get; set; }

    public DateTime? WorkAndDate { get; set; }

    public int WorkStageId { get; set; }

    public byte[]? Photo { get; set; }

    public string? PdfUrl { get; set; }

    public virtual Personel? WorkPersonel { get; set; }

    public virtual Stage WorkStage { get; set; } = null!;
}
