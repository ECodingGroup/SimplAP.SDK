namespace ECoding.SimpleApi.Core.SDK.Dto
{
    [Obsolete]
    public class FileProcessingOutput
    {
        /// <summary>
        /// The result of the AI image processing.
        /// </summary>
        public IEnumerable<ProcessedFile> ProcessedFiles { get; set; } = new List<ProcessedFile>();
    }
}
