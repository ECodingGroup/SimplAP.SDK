using SimplAP.SDK.Core.Dto.Shared;
using System.Text.RegularExpressions;

namespace SimplAP.SDK.Core.Dto
{
    public class CheckboxDetectionInput : CheckboxBaseDto
    {
        /// <summary>
        /// The Regex that will allow us to match the text of the checkbox in the form.
        /// </summary>
        public string CheckboxMatchingRegex { get; set; }

        /// <summary>
        /// Regex options to specify for the matching.
        /// By default these flags are turned on:
        ///  - Multiline
        ///  - Culture invariant (ignores diacritics)
        ///  - Ignore case (case insensitive)
        /// </summary>
        public RegexOptions RegexOptions { get; set; } = (RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
    }
}
