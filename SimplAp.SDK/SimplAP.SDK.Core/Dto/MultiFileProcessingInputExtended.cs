using SimplAP.SDK.Core.Enums;

namespace SimplAP.SDK.Core.Dto
{
    public class MultiFileProcessingInputExtended : MultiFileProcessingInput
    {
        public string AIType { get; set; }

        public MultiFileProcessingInputExtended(MultiFileProcessingInput input, AIModelType type)
        {
            ImageData = input.ImageData;
            ImageType = input.ImageType;
            ProcessesToRun = input.ProcessesToRun;
            AIType = type.ToString().ToLower();
        }
    }
}
