using System;
using System.Collections.Generic;
using System.Text;

namespace SimplAP.SDK.Core.Dto
{
    public class ProcessingOutput
    {
        public IEnumerable<ProcessedEntities> Result { get; set; } = Array.Empty<ProcessedEntities>();
    }

}
