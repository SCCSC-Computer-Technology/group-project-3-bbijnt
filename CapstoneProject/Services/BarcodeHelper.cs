using System.Drawing;

namespace CapstoneProject.Services
{
    public class BarcodeHelper
    {
        public static string FormatUUIDForBarcode(string uuid) //formats UUID for code 39 barcode
        {
            return "*" + uuid + "*";
        }

        public static Bitmap CreateBarcodeImage(string barcodeText, string fontFamily = "fre3of9x", int fontSize = 48)
        {
            Font font = new Font(fontFamily, fontSize);
            Bitmap barcodeBitmap = new Bitmap(600, 150);
            Graphics g = Graphics.FromImage(barcodeBitmap);

            g.Clear(Color.White); //white background for barcode
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            Brush textBrush = Brushes.Black;
            g.DrawString(barcodeText, font, textBrush, new PointF(10, 30));
            g.Dispose();

            return barcodeBitmap;
        }

    }
}
