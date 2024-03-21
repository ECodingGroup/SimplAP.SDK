namespace SimplAP.SDK.Core.Dto.Shared
{
    public class DetectedObject
    {
        /// <summary>
        /// The bounding boxes of the detected object
        /// </summary>
        public BBox BBox { get; set; }
        /// <summary>
        /// The bounding box of the suggested crop area
        /// </summary>
        public BBox SuggestedCropBBox { get; set; }
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
