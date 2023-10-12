using SimplAP.SDK.Core.Enums;
using System.Collections.Generic;
using System.IO;

namespace SimplAP.SDK.Core.Dto
{
    public class MultiFileProcessingMultipartInput
    {
        public Stream FileStream { get; set; }
        public IEnumerable<ImageAIProcessingType> ProcessesToRun { get; set; }
        public ProcessedImageType ImageType { get; set; }
    }
}
