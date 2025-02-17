using System;
using System.Collections.Generic;



public partial class Authority
{
    public int AuthoritiesId { get; set; }

    public string? AuthoritesName { get; set; }

    public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();
}
