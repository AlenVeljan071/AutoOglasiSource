using AutoOglasiSource.Responses;

namespace AutoOglasiSource.Model.Advertisement
{
    public class AdvertisementModel
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Year { get; set; }
        public AddressEntity Address { get; set; }
        public EnumValue Color { get; set; }
        public EnumValue Fuel { get; set; }
        public EnumValue Drive { get; set; }
        public EnumValue ParkingSensors { get; set; }
        public decimal? Mileage { get; set; }
        public int? KW { get; set; }
        public int? CCM { get; set; }
        public int? HorsePower { get; set; }
        public int? Weight { get; set; }
        public EnumValue Shifter { get; set; }
        public EnumValue Gear { get; set; }
        public EnumValue Emission { get; set; }
        public EnumValue Seat { get; set; }
        public bool? Abs { get; set; }
        public bool? Airbag { get; set; }
        public bool? Alarm { get; set; }
        public bool? AluminiumRims { get; set; }
        public bool? CentralLock { get; set; }
        public bool? DpfFilter { get; set; }
        public bool? DigitalClimate { get; set; }
        public bool? RemoteUnlocking { get; set; }
        public bool? ElectricWindows { get; set; }
        public bool? ElectricMirrors { get; set; }
        public bool? SeatHeating { get; set; }
        public bool? SteeringWheelControls { get; set; }
        public bool? Navigation { get; set; }
        public bool? ParkAssist { get; set; }
        public bool? ServiceBook { get; set; }
        public bool? CruiseControl { get; set; }
        public bool? Damaged { get; set; }
        public bool? Registered { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ImageResponse> Images { get; set; }

        public string ErrorMessage { get; set; }
    }
}
