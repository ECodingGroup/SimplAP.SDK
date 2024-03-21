using SimplAP.SDK.Core.Enums;
using System.Collections.Generic;

namespace SimplAP.SDK.Core.Dto
{
    public class ProcessingExtendedInput : ProcessingInput
    {
        public ProcessingExtendedInput(AIModelType modelType, ProcessedImageType imageType, byte[] fileToProcess, params ImageAIProcessingType[] processesToRun)
        {
            ModelType = modelType;
            ImageType = imageType;
            ImageData = fileToProcess;
            ProcessesToRun = processesToRun;
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

        /// <summary>
        /// If checkbox detection is inabled we will try to find all the checkboxes in the form but we will asign random Ids.
        /// To be more specific, you will need to provide us some identifiers and matching regexes to specify the checkboxes. 
        /// </summary>
        public IEnumerable<CheckboxDetectionInput> CheckboxesToDetect { get; set; }

        public AIModelType ModelType { get; internal set; }
    }

}
