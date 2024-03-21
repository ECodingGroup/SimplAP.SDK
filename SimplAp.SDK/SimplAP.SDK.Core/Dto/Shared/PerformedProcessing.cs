using SimplAP.SDK.Core.Enums;
using System;
using System.Collections.Generic;

namespace SimplAP.SDK.Core.Dto.Shared
{
    public class PerformedProcessing<TProcessedItem>
    {
        public PerformedProcessing() { }

        public PerformedProcessing(TProcessedItem result, ImageAIProcessingType processingType, bool wasProcessingPerformed = false)
        {
            ProcessingType = processingType;
            Result = result;
            WasProcessingPerformed = wasProcessingPerformed;

            if (result != null)
            { WasProcessingSuccessful = true; }
        }

        public bool WasProcessingPerformed { get; set; }
        public bool WasProcessingSuccessful { get; set; }
        public TProcessedItem Result { get; set; }
        public ImageAIProcessingType ProcessingType { get; set; }
        public string ProcessingTypeString { get => Enum.GetName(typeof(ImageAIProcessingType), ProcessingType); }

    }

    public class PerformedProcessing<TProcessedItem, T>
        where T : class
        where TProcessedItem : IEnumerable<T>
    {
        public PerformedProcessing() { }
        public PerformedProcessing(TProcessedItem result, ImageAIProcessingType processingType, bool wasProcessingPerformed = false)
        {
            ProcessingType = processingType;
            Result = result;
            WasProcessingPerformed = wasProcessingPerformed;

            if (result != null)
            { WasProcessingSuccessful = true; }
        }

        public bool WasProcessingPerformed { get; set; }
        public bool WasProcessingSuccessful { get; set; }
        public TProcessedItem Result { get; set; }
        public ImageAIProcessingType ProcessingType { get; set; }

    }

    public class PerformedProcessing<TProcessedItem, T, TKey>
    where T : class
    where TProcessedItem : IReadOnlyDictionary<TKey, T>
    {
        public PerformedProcessing(TProcessedItem result, ImageAIProcessingType processingType, bool wasProcessingPerformed = false)
        {
            ProcessingType = processingType;
            Result = result;
            WasProcessingPerformed = wasProcessingPerformed;

            if (result != null)
            { WasProcessingSuccessful = true; }
        }

        public bool WasProcessingPerformed { get; set; }
        public bool WasProcessingSuccessful { get; set; }
        public TProcessedItem Result { get; set; }
        public ImageAIProcessingType ProcessingType { get; set; }

    }
}
