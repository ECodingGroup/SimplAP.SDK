namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public class DetectedObjectExtended : DetectedObject
    {
        /// <summary>
        /// Null if face detection was turned off or no faces were detected
        /// </summary>
        public IEnumerable<FaceAnnotationDto> DetectedFaces { get; set; }

        /// <summary>
        /// Null if scanner option or lost / stolen feature was not enabled, or not the IdCard model was used for detection
        /// </summary>
        public IdCardInfo IdCardInfo { get; set; }

        /// <summary>
        /// Determines the image quality and whether the picture should be retaken.
        /// </summary>
        public bool? IsImageBlurred { get; set; }
    }
}
