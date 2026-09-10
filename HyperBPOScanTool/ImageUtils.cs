using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperBPOScanTool
{
    public static class ImageUtils
    {
        /// <summary>
        /// Rotates an image by a given angle in degrees.
        /// </summary>
        /// <param name="image">The original Bitmap image.</param>
        /// <param name="angle">Rotation angle in degrees (e.g., 45, 90, -30).</param>
        /// <returns>A new rotated Bitmap image.</returns>
        public static Bitmap RotateImage(Bitmap image, float angle)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            // Convert degrees to radians for trigonometric calculations
            double radians = angle * Math.PI / 180.0;
            double cos = Math.Abs(Math.Cos(radians));
            double sin = Math.Abs(Math.Sin(radians));

            // Calculate the bounding box of the rotated image to avoid clipping
            //int newWidth = (int)(image.Width * cos + image.Height * sin);
            //int newHeight = (int)(image.Width * sin + image.Height * cos);

            int newWidth = (int)(image.Width );
            int newHeight = (int)(image.Height );

            Bitmap rotatedBitmap = new Bitmap(newWidth, newHeight, image.PixelFormat);
            rotatedBitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotatedBitmap))
            {
                // Set high quality rendering options
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Move the rotation point to the center of the new image
                g.TranslateTransform((float)newWidth / 2, (float)newHeight / 2);
                //g.TranslateTransform(0, 0);

                // Rotate graphics context
                g.RotateTransform(angle);

                // Move origin back so image draws centered
                g.TranslateTransform(-(float)image.Width / 2, -(float)image.Height / 2);

                // Draw original image onto rotated graphics surface
                g.DrawImage(image, new Point(0, 0));
            }

            return rotatedBitmap;
        }
        public static double GetAngleDegrees(double x1, double y1, double x2, double y2)
        {
            double deltaX = x2 - x1;
            double deltaY = y2 - y1;

            double radians = Math.Atan2(deltaY, deltaX);
            double degrees = radians * (180.0 / Math.PI);

            return degrees;
        }
        public static Bitmap AddOuterWhiteBorder(Bitmap original, int top, int right, int bottom, int left)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));

            // 1. Calculate new dimensions
            int newWidth = original.Width ;
            int newHeight = original.Height ;

            // 2. Create canvas with matching PixelFormat
            Bitmap borderedImage = new Bitmap(newWidth, newHeight, original.PixelFormat);

            // Preserve resolution metadata (DPI)
            borderedImage.SetResolution(original.HorizontalResolution, original.VerticalResolution);

            using (Graphics g = Graphics.FromImage(borderedImage))
            {
                // High quality settings
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(original, new Point(0, 0));

                // Inset forces the pen width to grow INWARD so the border isn't clipped outside the image
                

                // Draw from (0,0) across full width and height
                Point[] points = new Point[5];
                points[0] = new Point(0, 0);
                points[1] = new Point(newWidth - 1, 0);
                points[2] = new Point(newWidth - 1, newHeight - 1);
                points[3] = new Point(0, newHeight - 1);
                points[4] = new Point(0, 0);

                Pen pen = new Pen(Color.White, top);
                pen.Alignment = PenAlignment.Inset;
                g.DrawLine(pen, points[0], points[1]);
                pen.Width = right;
                g.DrawLine(pen, points[1], points[2]);
                pen.Width = bottom;
                g.DrawLine(pen, points[2], points[3]);
                pen.Width = left;
                g.DrawLine(pen, points[3], points[4]);
                
            }

            return borderedImage;
        }
    }
}
