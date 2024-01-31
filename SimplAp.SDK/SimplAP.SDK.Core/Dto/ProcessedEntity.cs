using Newtonsoft.Json;
using SimplAP.SDK.Core.Dto.Shared.Face;
using SimplAP.SDK.Core.Dto.Shared;
using SimplAP.SDK.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimplAP.SDK.Core.Dto
{
    /// <summary>
    /// The entity that was processed during detection.
    /// All the properties relate to each other.
    /// For example if we have a detected object and detected faces, the detected faces will be from the detected object
    /// </summary>
    public class ProcessedEntity
    {
        /// <summary>
        /// If detection (or any other process related to detection) was enabled and the AI was able to detect an object, this property will contain the detected object.
        /// </summary>
        public PerformedProcessing<DetectedObject> DetectedObject { get; set; } = new PerformedProcessing<DetectedObject>(null, ImageAIProcessingType.Detection);
        /// <summary>
        /// If image blur detection was enabled this property will contain a boolean information whether the related entity was blurred.
        /// If the related entity is a detected object, it will check for the detected object bluriness. 
        /// </summary>
        public PerformedProcessing<bool?> IsImageBlurred { get; set; } = new PerformedProcessing<bool?>(null, ImageAIProcessingType.ImageBlurDetection);
        /// <summary>
        /// If scanner option was enabled this property will contain the scanned data for the related entity.
        /// If the related entity is a detected object it will contain the scanned data for the given object
        /// </summary>
        public PerformedProcessing<ScannerResult> ScannedData { get; set; } = new PerformedProcessing<ScannerResult>(null, ImageAIProcessingType.Scanner);
        /// <summary>
        /// If the face detection option, or face extraction option was enabled this property will contain the detected faces and metadata related to it.
        /// If Face extraction was enabled, on top of face detection, it will contain the extracted face image as base64 data.
        /// </summary>
        public PerformedProcessing<IEnumerable<FaceAnnotationDto>, FaceAnnotationDto> DetectedFaces { get; set; } = new PerformedProcessing<IEnumerable<FaceAnnotationDto>, FaceAnnotationDto>(null, ImageAIProcessingType.FaceDetection);

        /// <summary>
        /// If the roll angle detection option was enabled, this property will contain the detected roll angle for the object.
        /// </summary>
        public PerformedProcessing<double?> RollAngle { get; set; } = new PerformedProcessing<double?>(null, ImageAIProcessingType.ObjectRotationAngle);

        /// <summary>
        /// If the card lost or stolen detection option was enabled, for Slovakian ID cards and Slovakian Passports we are able to detect if the card was stolen or not.
        /// The property will contain the information if the we were able to perform the detection and if the result was positive or negative.
        /// </summary>
        public PerformedProcessing<bool?> WasCardLostOrStolen { get; set; } = new PerformedProcessing<bool?>(null, ImageAIProcessingType.CardLostOrStolenDetection);

        /// <summary>
        /// If the barcode reader option was enabled, this property will contain the contents of the 2D barcode and the format of the barcode. (Supported formats: UPC, EAN, CODE 39, Codabar Databar MS1 Plessey, QR Code, Data Matrix, PDF417, Aztec).
        /// The location of the barcode will be in the detected object
        /// </summary>
        public PerformedProcessing<BarcodeDto> DetectedBarcode { get; set; } = new PerformedProcessing<BarcodeDto>(null, ImageAIProcessingType.BarcodeReader);


        public bool IsPerformed()
        {
            return DetectedObject.WasProcessingPerformed ||
                   IsImageBlurred.WasProcessingPerformed ||
                   ScannedData.WasProcessingPerformed ||
                   DetectedFaces.WasProcessingPerformed ||
                   RollAngle.WasProcessingPerformed ||
                   WasCardLostOrStolen.WasProcessingPerformed ||
                   DetectedBarcode.WasProcessingPerformed;
        }
    }
}
