using ECoding.SimpleApi.Core.SDK.Enums;

namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public class AvailableProcessingDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExampleUrl { get; set; }
        public string ProductCode { get; set; }
        public ImageAIProcessingType ProcessingTypeEnumValue { get; set; }
        public string ProcessingTypeStringValue { get; set; }
    }
}
