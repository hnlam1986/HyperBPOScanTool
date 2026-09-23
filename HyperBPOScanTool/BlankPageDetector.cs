using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace HyperBPOScanTool
{
    public static class BlankPageDetector
    {
        /// <summary>
        /// Kiểm tra xem Bitmap thu được từ máy quét có phải là trang trắng hay không.
        /// </summary>
        /// <param name="image">Ảnh Bitmap truyền vào.</param>
        /// <param name="marginPercent">Tỷ lệ % lề cần bỏ qua (mặc định 5%).</param>
        /// <param name="luminanceThreshold">Ngưỡng độ sáng để coi 1 pixel là màu tối (0 - 255, mặc định 200).</param>
        /// <param name="blackPixelRatioThreshold">Tỷ lệ % pixel tối tối đa để coi là trang trắng (mặc định 0.002 = 0.2%).</param>
        /// <returns>True nếu là trang trắng, False nếu có nội dung.</returns>
        public static bool IsBlankPage(Bitmap image,out decimal stdDevVal, double marginPercent = 0.05, byte luminanceThreshold = 200, double blackPixelRatioThreshold = 0.002 )
        {
            stdDevVal = 0;
            if (image == null) throw new ArgumentNullException(nameof(image));

            // 1. Tính toán vùng ảnh cần kiểm tra (Bỏ qua lề xung quanh)
            int marginX = (int)(image.Width * marginPercent);
            int marginY = (int)(image.Height * marginPercent);

            Rectangle scanArea = new Rectangle(
                marginX,
                marginY,
                image.Width - (2 * marginX),
                image.Height - (2 * marginY)
            );

            if (scanArea.Width <= 0 || scanArea.Height <= 0)
                return true;

            long totalPixelsScanned = 0;
            long darkPixelCount = 0;

            // 2. Lock bộ đệm pixel vào bộ nhớ để duyệt cực nhanh
            BitmapData data = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb // Chuyển đổi đọc theo chuỗi 32-bit ARGB
            );

            try
            {
                unsafe
                {
                    byte* ptr = (byte*)data.Scan0;
                    int stride = data.Stride;

                    for (int y = scanArea.Top; y < scanArea.Bottom; y++)
                    {
                        byte* row = ptr + (y * stride);

                        for (int x = scanArea.Left; x < scanArea.Right; x++)
                        {
                            int pixelIndex = x * 4; // Format 32bpp = 4 bytes per pixel (B, G, R, A)

                            byte b = row[pixelIndex];
                            byte g = row[pixelIndex + 1];
                            byte r = row[pixelIndex + 2];

                            // Tính độ sáng theo công thức chuẩn YIQ/Luma (Grayscale conversion)
                            double luminance = (0.299 * r) + (0.587 * g) + (0.114 * b);

                            // Nếu độ sáng thấp hơn ngưỡng (pixel tối/có mực)
                            if (luminance < luminanceThreshold)
                            {
                                darkPixelCount++;
                            }

                            totalPixelsScanned++;
                        }
                    }
                }
            }
            finally
            {
                // Mở khóa bộ nhớ
                image.UnlockBits(data);
            }

            // 3. Tính tỷ lệ pixel tối
            double actualRatio = (double)darkPixelCount / totalPixelsScanned;
            stdDevVal = (decimal)actualRatio;
            // Nếu tỷ lệ pixel tối nhỏ hơn ngưỡng quy định -> Trang trắng
            return actualRatio < blackPixelRatioThreshold;
        }
    }
}
