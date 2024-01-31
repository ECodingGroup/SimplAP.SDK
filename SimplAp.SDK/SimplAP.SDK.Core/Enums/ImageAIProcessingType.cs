using SimplAP.SDK.Core.Attributes;

namespace SimplAP.SDK.Core.Enums
{
    public enum ImageAIProcessingType
    {
        /// <summary>
        /// Standard object detection for the supported objects
        /// </summary>
        Detection = 1,
        /// <summary>
        /// Reading information from detected objects
        /// </summary>
        Scanner = 2,
        /// <summary>
        /// Detect faces within detected objects
        /// </summary>
        FaceDetection = 3,
        /// <summary>
        /// Get the rotation angle of the detected object
        /// </summary>
        ObjectRotationAngle = 4,
        /// <summary>
        /// Detect if the detected object image is blurred
        /// </summary>
        ImageBlurDetection = 5,
        /// <summary>
        /// Detect the detected object is stolen. Currently works only on slovakian ID Cards and passports.
        /// Enabling this automatically enables object detection (Detection) and Scanner
        /// </summary>
        CardLostOrStolenDetection = 6,
        /// <summary>
        /// Extract the face image from the detected face. Enabling this feature automatically enables Face Detection as well.
        /// </summary>
        FaceExtraction = 7,
        /// <summary>
        /// Extracts the value of a 2D Or 1D barcode (Supported formats: UPC, EAN, CODE 39, CODE 93, CODE 128, Codabar, MSI, Plessey, QR Code, Data Matrix, PDF417, Aztec)
        /// </summary>
        BarcodeReader = 8
    }
}
