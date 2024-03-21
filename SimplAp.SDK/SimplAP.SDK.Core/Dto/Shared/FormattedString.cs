namespace SimplAP.SDK.Core.Dto.Shared
{
    public class FormattedString
    {
        public string Value { get; set; }
        public string FormattedValue { get; set; }

        public static implicit operator string(FormattedString input)
        {
            return input.FormattedValue;
        }
    }
}
