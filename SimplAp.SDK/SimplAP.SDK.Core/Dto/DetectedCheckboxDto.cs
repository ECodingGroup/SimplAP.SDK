using SimplAP.SDK.Core.Dto.Shared;

namespace SimplAP.SDK.Core.Dto
{
    public class DetectedCheckboxDto : CheckboxBaseDto
    {
        /// <summary>
        /// The value of the detected chechbox.
        /// If null then we were not able to detect the checkbox value
        /// </summary>
        public bool? IsChecked { get; set; }

        /// <summary>
        /// The detection confidence
        /// </summary>
        public double? Confidence { get; set; }

        /// <summary>
        /// The text in the form for the detected checkbox
        /// </summary>
        public string CheckboxText { get; set; }
    }

}
