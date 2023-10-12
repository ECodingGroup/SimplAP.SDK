namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public class DriversLicenseFrontInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string IdNo { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidUntil { get; set; }
        public List<string> LicenseAllowedCategories { get; set; } = new List<string>();
    }
}
