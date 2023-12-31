﻿using System;

namespace SimplAP.SDK.Core.Dto.Shared.IdCard
{
    public class SmallTechnicalLicenseBackInfo
    {
        public string Manufacturer { get; set; }
        public string Variant { get; set; }
        public string Model { get; set; }
        public string VIN { get; set; }
        public int? LargestWeight { get; set; }
        public int? OperationalWeight { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string Category { get; set; }
        public string TypeNumber { get; set; }
        public int? LargestTrailerTowingWeightO1Kg { get; set; }
        public int? LargestTrailerTowingWeightO2Kg { get; set; }
        public float EngineVolume { get; set; }
        public string EngineVolumeMetric { get; set; }
        public int EnginePerformance { get; set; }
        public string EnginePerformanceMetric { get; set; }
        public string FuelType { get; set; }
        public string Paint { get; set; }
        public int? NumOfSeats { get; set; }
        public int? MaxSpeed { get; set; }
    }
}
