using ECoding.SimpleApi.Core.SDK.Enums;

namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public class MultiFileProcessingInputExtended : MultiFileProcessingInput
    {
        public string AIType { get; set; }

        public MultiFileProcessingInputExtended (MultiFileProcessingInput input, AIModelType type) {
            ImageData = input.ImageData;
            ImageType = input.ImageType;
            ProcessesToRun = input.ProcessesToRun;
            AIType = type.ToString().ToLower();
        }
    }
}
