namespace maisAgua.Domain.Exceptions
{
    internal class DeviceNotFoundException : DomainException
    {
        public int DeviceID { get; set; }
        public DeviceNotFoundException(int deviceId)
            : base($"Dispositivo não encontrado para o id: {deviceId}.") 
        { 
            DeviceID = deviceId;
        }
    }
}
