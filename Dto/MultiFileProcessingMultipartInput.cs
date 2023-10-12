using ECoding.SimpleApi.Core.SDK.Enums;

namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public class MultiFileProcessingMultipartInput
    {
        public Stream FileStream { get; set; }
        public IEnumerable<ImageAIProcessingType> ProcessesToRun { get; set; }
        public ProcessedImageType ImageType { get; set; }
    }
}
