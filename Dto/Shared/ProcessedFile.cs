namespace ECoding.SimpleApi.Core.SDK.Dto
{
    [Obsolete]
    public class ProcessedFile
    {
        /// <summary>
        /// Null if scanner option was not enabled, or not the IdCard model was used for detection
        /// </summary>
        public IdCardInfo IdCardInfo { get; set; }
        /// <summary>
        /// Null if no objects were detected or object detection option was not enabled.
        /// </summary>
        public IList<DetectedObject> DetectedObjects { get; set; }
    }
}
