namespace maisAgua.Application.DTOs.DeviceDTO
{
    public record DeviceUpdateDTO
    {
        public string? Name { get; set; }
        public DateTime? InstallationDate { get; set; }
    }
}
