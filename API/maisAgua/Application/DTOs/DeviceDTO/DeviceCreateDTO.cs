namespace maisAgua.Application.DTOs.DeviceDTO
{
    public record DeviceCreateDTO
    {
        public string Name { get; set; }
        public DateTime InstallationDate { get; set; }
    }
}
