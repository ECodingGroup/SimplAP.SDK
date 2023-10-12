using ECoding.SimpleApi.Core.SDK.Attributes;

namespace ECoding.SimpleApi.Core.SDK.Enums
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
