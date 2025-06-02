namespace maisAgua.Application.DTOs.Readings
{
    public record ReadingCreateDTO
    {
        public int LevelPct { get; set; }
        public float TurbidityNtu { get; set; }
        public float PhLevel { get; set; }
        public DateTime ReadingDatetime { get; set; }
        public int IdDevice { get; set; }
    }
}
