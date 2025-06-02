namespace maisAgua.Application.DTOs.Readings
{
    public record ReadingBasicInfoDTO
    {
        public int Id { get; set; }
        public DateTime ReadingDateTime { get; set; }

    }
}
