using System;
using System.Collections.Generic;
using System.Text;

namespace HyperBPOScanTool.Models
{
    public class BatchSettingObject
    {
        public string BatchId { get; set; }
        public string BatchName { get; set; }
        public bool HasCreateSubFolder { get; set; }
        public string SeparateChar { get; set; }
        public string IndexFormat { get; set; }
        public int StartNum { get; set; }
        public string ExportFolder { get; set; }

        public SeparateType SeparateType { get; set; }
        public int SplitPageCount { get; set; }
        public decimal BlankSheetThreshold { get; set; }
        public bool IsDiscardBlankSheets { get; set; }

        public int WhiteBorderTop { get; set; }
        public int WhiteBorderRight { get; set; }
        public int WhiteBorderBottom { get; set; }
        public int WhiteBorderLeft { get; set; }

    }

}
