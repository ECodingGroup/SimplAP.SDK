using SimplAP.SDK.Core.Enums;
using System;

namespace SimplAP.SDK.Core.Dto.Shared.IdCard
{
    public class PassportInfo
    {
        public string IssuedBy { get; set; }
        public string BirthNumber { get; set; }
        public DateTime? IssuedDate { get; set; }
        public string IdNumber { get; set; }
        public string PlaceOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string CountryCode { get; set; }
        public string Type { get; set; }
        public Gender? Gender { get; set; }
        public string Nationality { get; set; }
    }
}
