using ECoding.SimpleApi.Core.SDK.Enums;

namespace ECoding.SimpleApi.Core.SDK.Dto
{
    [Obsolete]
    public class FileProcessingInput
    {
        /// <summary>
        /// The model type that can detect objects
        /// </summary>
        public AIModelType AIModel { get; set; }
        /// <summary>
        /// The raw image data to run processing on
        /// </summary>
        public byte[] ImageData { get; set; }
        /// <summary>
        /// The file extension of the image attachment. 
        /// Currently we support png, jpg, tif, gif, bmp and pdf
        /// </summary>
        public string FileExtension { get; set; }
        /// <summary>
        /// Select what type processing you want to run on your image
        /// </summary>
        public List<string> ProcessesToRun { get; set; }
    }
}
