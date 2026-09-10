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
        BlankPage = 3,
        Barcode = 4,
        NumOfPage = 5
    }
    public class ScanPack
    {
        public ScanPack()
        {
            ScanFiles = new List<ScanFile>();
        }
        public List<ScanFile> ScanFiles { get; set; }

        public ScanPack Clone()
        {
            ScanPack newPack = new ScanPack();
            foreach (var file in ScanFiles)
            {
                ScanFile newFile = new ScanFile();
                newFile.FileName = file.FileName;
                newFile.FileId = file.FileId;
                foreach (var page in file.Pages)
                {
                    ScanPage newPage = new ScanPage();
                    newPage.PageImage = page.PageImage;
                    newPage.ImageId = page.ImageId;
                    newPage.IsBlank = page.IsBlank;
                    newPage.PageIndex = page.PageIndex;
                    newPage.StdDevVal = page.StdDevVal;
                    newFile.Pages.Add(newPage);
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
            Pages = new List<ScanPage>();
        }
        public string FileName { get; set; }
        public string FileId { get; set; }
        public List<ScanPage> Pages { get; set; }
        public bool IsBlankSheet { get {
                //bool isBlank = false;
                return Pages[0].IsBlank && Pages[1].IsBlank;
            } }
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
        public double StdDevVal { get; set; }
        public string ImageId { get; set; }
    }

}
