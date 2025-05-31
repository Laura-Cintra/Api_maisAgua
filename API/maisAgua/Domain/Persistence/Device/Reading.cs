namespace maisAgua.Domain.Device
{
    public class Reading
    {
        public int Id { get; set; }
        public int LevelPct { get; set; } // Nivel em porcentagem (%Pct)
        public float TurbidityNtu { get; set; } // Turbidez medida em NTU
        public float PhLevel { get; set; } // Nível de PH medido em números flutuantes
        public DateTime ReadingDatetime { get; set; }

        // Sensores
        public int IdDevice { get; set; }
        public Device Device { get; set; }


        // Construtores
        public Reading(){}

        public Reading(int idReading, int levelPct, float turbidityNtu, float phLevel, DateTime readingDateTime, int idDevice)
        {
            Id = idReading;
            LevelPct = levelPct;
            TurbidityNtu = turbidityNtu;
            PhLevel = phLevel;
            ReadingDatetime = readingDateTime;
            IdDevice = idDevice;
        }

    }
}
