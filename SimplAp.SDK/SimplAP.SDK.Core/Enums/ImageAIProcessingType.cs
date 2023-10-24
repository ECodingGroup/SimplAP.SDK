using SimplAP.SDK.Core.Attributes;

namespace SimplAP.SDK.Core.Enums
{
    public enum ImageAIProcessingType
    {
        Detection = 1,
        Scanner = 2,
        [NotSupported]
        FaceDetection = 3,
        ObjectRotationAngle = 4,
        [NotSupported]
        ImageBlurDetection = 5
    }
}
