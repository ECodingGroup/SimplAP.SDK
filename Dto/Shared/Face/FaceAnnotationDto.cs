using ECoding.SimpleApi.Core.SDK.Enums;

namespace ECoding.SimpleApi.Core.SDK.Dto
{
    public class FaceAnnotationDto
    {
        /// <summary>
        /// The bounding polygon around the face. The coordinates of the bounding box are
        ///     in the original image's scale. The bounding box is computed to "frame" the face
        ///     in accordance with human expectations. It is based on the landmarker results.
        ///     Note that one or more x and/or y coordinates may not be generated in the `BoundingPoly`
        ///     (the polygon will be unbounded) if only a partial face appears in the image to
        ///     be annotated.
        /// </summary>
        public BoundingPolyDto BoundingPoly { get; set; }

        /// <summary>
        /// The `fd_bounding_poly` bounding polygon is tighter than the `boundingPoly`, and
        ///     encloses only the skin part of the face. Typically, it is used to eliminate the
        ///     face from any image analysis that detects the "amount of skin" visible in an
        ///     image. It is not based on the landmarker results, only on the initial face detection,
        ///     hence the <code>fd</code> (face detection) prefix.
        /// </summary>  
        public BoundingPolyDto FdBoundingPoly { get; set; }

        /// <summary>
        /// Detected face landmarks.
        /// </summary>
        public IEnumerable<LandmarkDto> Landmarks { get; set; }

        /// <summary>
        /// Roll angle, which indicates the amount of clockwise/anti-clockwise rotation of the face relative to the image vertical about the axis perpendicular to the face. Range [-180,180].
        /// </summary>
        public float RollAngle { get; set; }

        /// <summary>
        /// Yaw angle, which indicates the leftward/rightward angle that the face is pointing relative to the vertical plane perpendicular to the image. Range [-180,180].
        /// </summary>
        public float PanAngle { get; set; }

        /// <summary>
        /// Pitch angle, which indicates the upwards/downwards angle that the face is pointing relative to the image's horizontal plane. Range [-180,180].
        /// </summary>
        public float TiltAngle { get; set; }

        /// <summary>
        /// Detection confidence. Range [0, 1].
        /// </summary>
        public float DetectionConfidence { get; set; }

        /// <summary>
        /// Face landmarking confidence. Range [0, 1].
        /// </summary>
        public float LandmarkingConfidence { get; set; }

        /// <summary>
        /// Joy likelihood.
        /// </summary>
        public FaceAnnotationLikelihood JoyLikelihood { get; set; }

        /// <summary>
        /// Sorrow likelihood.
        /// </summary>
        public FaceAnnotationLikelihood SorrowLikelihood { get; set; }

        /// <summary>
        /// Anger likelihood.
        /// </summary>
        public FaceAnnotationLikelihood AngerLikelihood { get; set; }

        /// <summary>
        /// Surprise likelihood.
        /// </summary>
        public FaceAnnotationLikelihood SurpriseLikelihood { get; set; }

        /// <summary>
        /// Under-exposed likelihood.
        /// </summary>
        public FaceAnnotationLikelihood UnderExposedLikelihood { get; set; }

        /// <summary>
        /// Blurred likelihood.
        /// </summary>
        public FaceAnnotationLikelihood BlurredLikelihood { get; set; }

        /// <summary>
        /// Headwear likelihood.
        /// </summary>
        public FaceAnnotationLikelihood HeadwearLikelihood { get; set; }

        public string DetectedFaceImageBase64 { get; set; }
    }
}
