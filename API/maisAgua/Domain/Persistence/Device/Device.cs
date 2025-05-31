namespace maisAgua.Domain.Device
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime InstallationDate { get; set; }

        // Leituras do dispositivo
        public List<Reading> Readings { get; set; } = new List<Reading>();


        // Construtores
        public Device() { }
        
        public Device(int idDevice, string name, DateTime installationDate)
        {
            Id = idDevice;
            Name = name;
            InstallationDate = installationDate;
        }


    }
}
