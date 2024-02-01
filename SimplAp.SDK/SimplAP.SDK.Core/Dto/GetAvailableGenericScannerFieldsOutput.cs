using System;
using System.Collections.Generic;
using System.Text;

namespace SimplAP.SDK.Core.Dto
{
    public class GetAvailableGenericScannerFieldsOutput
    {
        public IEnumerable<AvailableGenericFieldEntity> GenericScannerFields { get; set; }
    }
}
