

namespace WorkFvApi.Models;

public partial class Authority
{
    public int AuthoritiesId { get; set; }

    public string? AuthoritesName { get; set; }

    public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();
}
