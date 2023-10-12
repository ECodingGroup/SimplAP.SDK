﻿namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public class DetectedObject
    {
        /// <summary>
        /// The bounding boxes of the detected object
        /// </summary>
        public BBox BBox { get; set; }
        /// <summary>
        /// The detected object category
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// The detection confidence score
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// If the input image document was a multi page document, this value indicates on which page was the object detected.
        /// </summary>
        public int? PageNo { get; set; }
        /// <summary>
        /// The angle at which the detected object is rotated relative to the image
        /// </summary>
        public double? RollAngle { get; set; }
    }
}