using System.Collections.Generic;

namespace SimplAP.SDK.Core.Dto.Shared.IdCard
{
    public class IdCardInfo
    {
        public NationalIdCardFrontInfo NationalIdCardFrontInfo { get; set; }
        public NationalIdCardBackInfo NationalIdCardBackInfo { get; set; }
        public DriversLicenseFrontInfo DriversLicenseFrontInfo { get; set; }
        public DriversLicenseBackInfo DriversLicenseBackInfo { get; set; }
        public SmallTechnicalLicenseFrontInfo SmallTechnicalLicenseFrontInfo { get; set; }
        public SmallTechnicalLicenseBackInfo SmallTechnicalLicenseBackInfo { get; set; }
        public PassportInfo PassportInfo { get; set; }

        public Dictionary<string, List<object>> CombinedExtractedInfo { get; set; } = new Dictionary<string, List<object>>();

        /// <summary>
        /// Detection if the card was lost or stolen. Currently only available for slovakian passports and id cards.
        /// </summary>
        public bool? WasCardLostOrStolen { get; set; }
    }
}
