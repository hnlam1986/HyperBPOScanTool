using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperBPOScanTool
{
    public class PDFUtils
    {
        public bool CreatePDF(string filename,string filePath, List<Image> imgs)
        {
            try
            {
                // Create a new PDF document
                PdfDocument document = new PdfDocument();
                document.Info.Title = filename;
                // Create an empty page
                foreach (var img in imgs)
                {
                    PdfPage page = document.AddPage();
                    
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, ImageFormat.Png);
                        ms.Position = 0;
                        using (XImage xImage = XImage.FromStream(ms))
                        {
                            page.Width = xImage.PointWidth;
                            page.Height = xImage.PointHeight;
                            using (XGraphics gfx = XGraphics.FromPdfPage(page))
                            {
                                // 5. Draw the image to fit the exact page boundaries
                                gfx.DrawImage(xImage, 0, 0, page.Width, page.Height);
                            }
                        }
                    }
                }
                document.Save(filePath+"//"+filename);
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
