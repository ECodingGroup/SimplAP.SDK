namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public class SmallTechnicalLicenseBackInfo
    {
        public string Manufacturer { get; set; }
        public string Variant { get; set; }
        public string Model { get; set; }
        public string VIN { get; set; }
        public string LargestWeight { get; set; }
        public string OperationalWeight { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string Category { get; set; }
        public string TypeNumber { get; set; }
        public int? LargestTrailerTowingWeightO1Kg { get; set; }
        public int? LargestTrailerTowingWeightO2Kg { get; set; }
        public string EngineVolume { get; set; }
        public string EnginePerformance { get; set; }
        public string FuelType { get; set; }
        public string Paint { get; set; }
        public int? NumOfSeats { get; set; }
        public int? MaxSpeed { get; set; }
    }
}
