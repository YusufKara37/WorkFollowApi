


namespace WorkFvApi.DTO.UnitDTO
{
    public class UnitDto
    {
        public int UnitId { get; set; }

        public string? UnitName { get; set; }

        public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();

    }
}