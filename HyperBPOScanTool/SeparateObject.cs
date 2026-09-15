using System;
using System.Collections.Generic;
using System.Text;

namespace HyperBPOScanTool
{
    public class SeparateObject
    {
        public SeparateType SeparateMode { get; set; }
        public int TotalPage { get; set; }
        public decimal BlankValue { get; set; }
        public bool HideBlankPage { get; set; }
        public bool HideBlankSheet { get; set; }
    }
}
