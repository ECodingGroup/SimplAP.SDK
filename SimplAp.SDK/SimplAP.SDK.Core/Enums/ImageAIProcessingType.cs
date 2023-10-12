using SimplAP.SDK.Core.Attributes;

namespace SimplAP.SDK.Core.Enums
{
    public enum ImageAIProcessingType
    {
        ObjectDetection = 1,
        Scanner = 2,
        [NotSupported]
        FaceRecognition = 3,
        ObjectRotationAngle = 4,
        [NotSupported]
        ImageBlurDetection = 5
    }
}
