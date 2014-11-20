using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace ScriptingModel
{
    [Scriptable]
    public class ClientImage
    {
        private Bitmap Bitmap { get; set; }
        [Scriptable] public int Width { get { return Bitmap.Width; } }
        [Scriptable] public int Height { get { return Bitmap.Height; } }

        public ClientImage(int width, int height)
        {
            Bitmap = new Bitmap(width, height);
        }

        public string ImageUrl()
        {
            string base64String = "";
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Bitmap.Save(memoryStream, ImageFormat.Png);
                Byte[] bytes = new Byte[memoryStream.Length];
                memoryStream.Position = 0;
                memoryStream.Read(bytes, 0, (int)bytes.Length);
                base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            }
            return "data:image/png;base64," + base64String;
        }

        [Scriptable]
        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            Pen redPen = new Pen(Color.Red, 3);
            using (var graphics = Graphics.FromImage(Bitmap))
            {
                graphics.DrawLine(redPen, x1, y1, x2, y2);
            }
        }
    }
}