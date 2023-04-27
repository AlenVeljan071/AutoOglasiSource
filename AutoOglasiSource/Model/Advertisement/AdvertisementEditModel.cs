using AutoOglasiSource.Model.Enums;

namespace AutoOglasiSource.Model.Advertisement
{
    public class AdvertisementEditModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Year { get; set; }
        public AddressEntity Address { get; set; }
        public int Color { get; set; }
        public Fuel Fuel { get; set; }
        public Drive Drive { get; set; }
        public ParkingSensors ParkingSensors { get; set; }
        public decimal? Mileage { get; set; }
        public int? KW { get; set; }
        public int? CCM { get; set; }
        public int? HorsePower { get; set; }
        public int? Weight { get; set; }
        public int Shifter { get; set; }
        public int Gear { get; set; }
        public int Emission { get; set; }
        public int Seat { get; set; }
        public bool Abs { get; set; }
        public bool Airbag { get; set; }
        public bool Alarm { get; set; }
        public bool AluminiumRims { get; set; }
        public bool CentralLock { get; set; }
        public bool DpfFilter { get; set; }
        public bool DigitalClimate { get; set; }
        public bool RemoteUnlocking { get; set; }
        public bool ElectricWindows { get; set; }
        public bool ElectricMirrors { get; set; }
        public bool SeatHeating { get; set; }
        public bool SteeringWheelControls { get; set; }
        public bool Navigation { get; set; }
        public bool ParkAssist { get; set; }
        public bool ServiceBook { get; set; }
        public bool CruiseControl { get; set; }
        public bool Damaged { get; set; }
        public bool Registered { get; set; }
        public string Note { get; set; }
    }
}

