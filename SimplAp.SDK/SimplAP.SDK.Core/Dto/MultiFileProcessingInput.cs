using SimplAP.SDK.Core.Enums;
using System.Collections.Generic;

namespace SimplAP.SDK.Core.Dto
{
    public class MultiFileProcessingInput
    {
        public AIModelType ModelType { get; set; }
        public byte[] ImageData { get; set; }
        public ProcessedImageType ImageType { get; set; } = ProcessedImageType.Image;
        public IEnumerable<ImageAIProcessingType> ProcessesToRun { get; set; }
    }
}
