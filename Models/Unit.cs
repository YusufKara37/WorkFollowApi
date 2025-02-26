

namespace WorkFvApi.Models;

public partial class Unit
{
    public int UnitId { get; set; }

    public string? UnitName { get; set; }

    public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();
}
