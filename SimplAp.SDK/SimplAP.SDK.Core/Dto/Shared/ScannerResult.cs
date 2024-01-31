using SimplAP.SDK.Core.Enums;
using System;
using System.Collections.Generic;

namespace SimplAP.SDK.Core.Dto.Shared
{
    public class ScannerResult
    {
        public string Address { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string BirthNumber { get; set; }
        public string BloodType { get; set; }
        public string CompanyName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }
        public string IdNumber { get; set; }
        public string IssuedBy { get; set; }
        public DateTime? IssuedDate { get; set; }
        public List<string> LicenseAllowedCategories { get; set; }
        public string MaidenName { get; set; }
        public string Title { get; set; }
        public string Nationality { get; set; }
        public string VIN { get; set; }
        public string PlaceOfBirth { get; set; }
        public string ScannerRawText { get; set; }
        public string Manufacturer { get; set; }
        public string Variant { get; set; }
        public string Model { get; set; }
        public int? LargestWeight { get; set; }
        public int? OperationalWeight { get; set; }
        public DateTime? ValidUntil { get; set; }
        public string Category { get; set; }
        public string TypeNumber { get; set; }
        public int? LargestTrailerTowingWeightO1Kg { get; set; }
        public int? LargestTrailerTowingWeightO2Kg { get; set; }
        public float? EngineVolume { get; set; }
        public string EngineVolumeMetric { get; set; }
        public int? EnginePerformance { get; set; }
        public string EnginePerformanceMetric { get; set; }
        public string FuelType { get; set; }
        public string Paint { get; set; }
        public int? NumOfSeats { get; set; }
        public int? MaxSpeed { get; set; }
        public string IBAN { get; set; }
        public decimal? VIGSK_PolicyId { get; set; }
    }
}
