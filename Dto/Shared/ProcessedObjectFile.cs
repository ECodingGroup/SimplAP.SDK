namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public class ProcessedObjectFile
    {
        /// <summary>
        /// If the input image document was a multi page document, this value indicates on which page was the object detected.
        /// </summary>
        public int? PageNo { get; set; }
        /// <summary>
        /// Null if no objects were detected or object detection option was not enabled.
        /// </summary>
        public List<DetectedObjectExtended> DetectedObjects { get; set; } = new List<DetectedObjectExtended>();
    }
}
