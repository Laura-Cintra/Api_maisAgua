using maisAgua.Application.DTOs.Readings;

namespace maisAgua.Application.DTOs.Devices;

public class DevicesReadDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime InstallationDate { get; set; }

    public List<ReadingBasicInfoDTO> Readings { get; set; }
}
