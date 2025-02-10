

public class PersonelDto
{
    public int PersonelId { get; set; }
    public string? PersonelName { get; set; }
    public string? PersonelUserName { get; set; }
    public string PersonelPassword { get; set; } = null!;
    public int? PersonelUnitId { get; set; }
    public int PersonelAuthoritesId { get; set; }

}
