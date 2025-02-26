
namespace WorkFvApi.Models;

public partial class Stage
{
    public int StageId { get; set; }

    public string? StageName { get; set; }

    public virtual ICollection<Work> Works { get; set; } = new List<Work>();
}
