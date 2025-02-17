using System;
using System.Collections.Generic;



public partial class Personel
{
    public int PersonelId { get; set; }

    public string? PersonelName { get; set; }

    public string? PersonelUserName { get; set; }

    public string? PersonelPassword { get; set; }

    public int? PersonelUnitId { get; set; }

    public int PersonelAuthoritesId { get; set; }

    public virtual Authority PersonelAuthorites { get; set; } = null!;

    public virtual Unit? PersonelUnit { get; set; }

    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}
