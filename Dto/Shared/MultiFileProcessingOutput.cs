namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public class MultiFileProcessingOutput
    {
        /// <summary>
        /// The result of the AI image processing.
        /// </summary>
        public IList<ProcessedObjectFile> ProcessedFiles { get; set; } = new List<ProcessedObjectFile>();
    }
}
