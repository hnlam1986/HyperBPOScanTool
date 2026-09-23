using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperBPOScanTool
{
    public enum SeparateType
    {
        None = 0,
        Persheet = 1,
        BlankSheet = 3,
        Barcode = 4,
        NumOfPage = 5
    }
    public class ScanBatch
    {
        public ScanBatch()
        {
            ScanFiles = new List<ScanFile>();
        }
        public List<ScanFile> ScanFiles { get; set; }
        public string BatchName { get; set; }
        public string ExportPath { get; set; }
        public string IndexFormat { get; set; }
        public ScanBatch Clone()
        {
            ScanBatch newPack = new ScanBatch();
            foreach (var file in ScanFiles)
            {
                ScanFile newFile = new ScanFile();
                newFile.FileName = file.FileName;
                newFile.FileId = file.FileId;
                foreach (ScanSheet sheet in file.Sheets)
                {
                    ScanSheet newSheet = new ScanSheet();
                    newSheet.SheetName = sheet.SheetName;
                    newSheet.SheetId = sheet.SheetId;
                    newSheet.SheetIndex = sheet.SheetIndex;
                    newSheet.IsBlankSheet = sheet.IsBlankSheet;
                    newSheet.Top = sheet.Top;
                    newSheet.Bottom = sheet.Bottom;
                }
                newPack.ScanFiles.Add(newFile);
            }
            return newPack;
        }

    }
                    

    public class ScanFile
    {
        public ScanFile()
        {
            Sheets = new List<ScanSheet>();
        }
        public string FileName { get; set; }
        public string FileId { get; set; }
        public List<ScanSheet> Sheets { get; set; }
        
    }

    public class ScanPage
    {
        public ScanPage()
        {
            
        }
        [JsonIgnore]
        public Image PageImage { get; set; }
        public bool IsBlank { get; set; }
        public int PageIndex { get; set; }
        public decimal StdDevVal { get; set; }
        public string ImageId { get; set; }
    }

    public class ScanSheet
    {
        public ScanSheet()
        {
                
        }
        public string SheetName { get; set; }
        public string SheetId { get; set; }
        public int SheetIndex { get; set; }
        public bool IsBlankSheet { get { return Top.IsBlank && Bottom.IsBlank; } set; }
        public ScanPage Top { get; set; }
        public ScanPage Bottom { get; set; }

    }

}
