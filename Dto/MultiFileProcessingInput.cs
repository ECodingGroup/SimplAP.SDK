using ECoding.SimpleApi.Core.SDK.Enums;

namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public class MultiFileProcessingInput
    {
        public byte[] ImageData { get; set; }
        public ProcessedImageType ImageType { get; set; }
        public IEnumerable<ImageAIProcessingType> ProcessesToRun { get; set; }
    }
}
