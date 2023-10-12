using ECoding.SimpleApi.Core.SDK.Enums;

namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public class NationalIdCardFrontInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string IdNumber { get; set; }
        public string Nationality { get; set; }
        public string BirthNumber { get; set; }
        public string IssuedBy { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
