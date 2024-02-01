namespace SimplAP.SDK.Core.Dto.Shared
{
    public class DetectedObject
    {
        /// <summary>
        /// The bounding boxes of the detected object
        /// </summary>
        public BBox BBox { get; set; }
        /// <summary>
        /// The bounding boxes of the estimated optimal crop area.
        /// Please be aware that if we are not able to calculate this then it will be null and BBox should be then used instead.
        /// </summary>
        //public Bbox EstimatedOptimalCropBBox { get; set; }
        /// <summary>
        /// The detected object category
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// The detection confidence score
        /// </summary>
        public double Score { get; set; }
    }
}
