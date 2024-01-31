using System.Collections.Generic;

namespace SimplAP.SDK.Core.Dto
{
    /// <summary>
    /// This class represents a file that contains data to process. It can be a PDF file or an image file
    /// </summary>
    public class ProcessedEntities
    {
        /// <summary>
        /// If the input file was a multi page document, this value indicates on which page was the object detected.
        /// If the input file was not a multi page document, this value will be null
        /// </summary>
        public int? PageNo { get; set; }
        /// <summary>
        /// The result of the image processing.
        /// There can be multiple entities as the result of processing or none. (For example multiple detected objects)
        /// If no suitable entities to process were found as part of the image or the image was invalid, this will be empty.
        /// </summary>
        public List<ProcessedEntity> Entities { get; set; } = new List<ProcessedEntity>();
    }
}
