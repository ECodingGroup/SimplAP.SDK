using System.Collections.Generic;

namespace SimplAP.SDK.Core.Dto.Shared
{
    public class MultiFileProcessingOutput
    {
        /// <summary>
        /// The result of the AI image processing.
        /// </summary>
        public IList<ProcessedObjectFile> ProcessedFiles { get; set; } = new List<ProcessedObjectFile>();
    }
}
