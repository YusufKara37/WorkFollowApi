using System;
using System.Collections.Generic;



public partial class Work
{
    public int WorkId { get; set; }

    public string WorkName { get; set; } = null!;

    public string? WorkComment { get; set; }

    public int? WorkPersonelId { get; set; }

    public DateTime? WorkStartDate { get; set; }

    public DateTime? WorkAndDate { get; set; } = null;

    public int? WorkStageId { get; set; }

    public byte[]? Photo { get; set; }

    public virtual Personel? WorkPersonel { get; set; }

    public virtual Stage? WorkStage { get; set; }
}
