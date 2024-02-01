using System;
using System.Collections.Generic;
using System.Text;

namespace SimplAP.SDK.Core.Dto
{
    public class AvailableGenericFieldEntity
    {
        /// <summary>
        /// Use this value for the AI processing
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The description of this field
        /// </summary>
        public string Description { get; set; }

        public IEnumerable<AIScannerGenericField> IncludedFields { get; set; }
    }

    public class AIScannerGenericField
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }

}
