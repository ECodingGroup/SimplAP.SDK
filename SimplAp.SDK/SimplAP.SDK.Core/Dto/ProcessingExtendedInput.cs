using SimplAP.SDK.Core.Enums;
using System.Collections.Generic;

namespace SimplAP.SDK.Core.Dto
{
    public class ProcessingExtendedInput : ProcessingInput
    {
        public ProcessingExtendedInput(AIModelType modelType)
        {
            ModelType = modelType;
        }

        /// <summary>
        /// By default object segmentation is turned on. That means that all the processing is happening individually on the detected objects.
        /// Example:
        /// If segmentation is turned on and I request Detection, Scanner and ObjectRotationDetection firt an object detection happens and on the detected objects we try to do the scanner and rotation detection processes. If no object will be detected no further operations will be performed.
        /// If segmentation is turned off all the processing will happen, but only on the whole picture.
        /// There is also a caveat on the scanner feature is segmentation is turned off. Only certain (more generic) fields can be scanned. Look up GetAvailableGenericScannerFields endpoint to see what fields can be scanned genericly.
        /// </summary>
        public bool DisableObjectSegmentation { get; set; }
        /// <summary>
        /// This can only be used in combination with DisableObjectSegmentation = true, and ImageAIProcessingType.Scanner feature enabled.
        /// This is going to be the list of generic scanner fields that you want to scan. You can look up all the available generic scanner fields via GetAvailableGenericScannerFields endpoint
        /// </summary>
        public IEnumerable<string> GenericScannerFieldsToUse { get; set; }

        public AIModelType ModelType { get; set; }
    }

}
