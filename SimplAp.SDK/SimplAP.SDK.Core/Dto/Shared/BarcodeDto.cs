using System;
using System.Collections.Generic;
using System.Text;

namespace SimplAP.SDK.Core.Dto.Shared
{
    public class BarcodeDto
    {
        public string BarcodeFormat { get; set; }
        public int BarcodeFormatType { get; set; }
        public string BarcodeContent { get; set; }
    }
}
