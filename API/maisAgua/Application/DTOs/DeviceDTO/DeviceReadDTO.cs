namespace maisAgua.Application.DTOs.DeviceDTO
{
    public record DeviceReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime InstallationDate { get; set; }
    }
}
