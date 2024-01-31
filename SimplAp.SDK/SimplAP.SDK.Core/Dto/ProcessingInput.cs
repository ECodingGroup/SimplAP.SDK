using SimplAP.SDK.Core.Enums;
using System.Collections.Generic;

namespace SimplAP.SDK.Core.Dto
{
    public class ProcessingInput
    {
        /// <summary>
        /// The raw image data to run processing on
        /// </summary>
        public byte[] ImageData { get; set; }
        /// <summary>
        /// The file extension of the image attachment. 
        /// Currently we support png, jpg, tif, gif, bmp and pdf
        /// </summary>
        public ProcessedImageType ImageType { get; set; }
        /// <summary>
        /// Select what type processing you want to run on your image
        /// </summary>
        public IEnumerable<ImageAIProcessingType> ProcessesToRun { get; set; }

        public string AIType { get; set; }
    }
}
