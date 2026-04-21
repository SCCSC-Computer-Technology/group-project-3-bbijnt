using System.Drawing;
using System.Drawing.Text;

namespace CapstoneProject.Services
{
    public class BarcodeHelper
    {
        public static string FormatUUIDForBarcode(string uuid) //formats UUID for code 39 barcode
        {
            return "*" + uuid + "*";
        }

        public static Bitmap CreateBarcodeImage(string barcodeText, int fontSize = 48)
        {
            string fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "font", "fre3of9x.ttf");

            PrivateFontCollection fontCollection = new PrivateFontCollection(); //custom font collection for barcode font
            fontCollection.AddFontFile(fontPath);

            Font barcodeFont = new Font(fontCollection.Families[0], fontSize);
            Font uuidFont = new Font("Arial", 16);
            
            Bitmap barcodeBitmap = new Bitmap(1600, 200);
            Graphics g = Graphics.FromImage(barcodeBitmap);

            g.Clear(Color.White); //white background for barcode
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            Brush textBrush = Brushes.Black;
            g.DrawString(barcodeText, barcodeFont, textBrush, new PointF(110, 30));
            g.DrawString(barcodeText.Trim('*'), uuidFont, textBrush, new PointF(500, 125));
            g.Dispose();

            return barcodeBitmap;
        }

    }
}
