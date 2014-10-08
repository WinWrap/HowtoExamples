using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace ww_ws_objmod
{
    [Scriptable]
    public class ClientImage
    {
        private Bitmap Bitmap_;
        public string Base64String_;

        public ClientImage(Bitmap bitmap)
        {
            Bitmap_ = bitmap;
            Base64String_ = "";
        }

        [Scriptable]
        public event Action Started;

        public void Start(string firedby)
        {
            if (Started != null)
                Started();
        }

        /*[Scriptable]
        public string DrawLine()
        {
            return "asdf";
        }*/

        [Scriptable]
        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            Pen redPen = new Pen(Color.Red, 3);
            using (var graphics = Graphics.FromImage(Bitmap_))
            {
                graphics.DrawLine(redPen, x1, y1, x2, y2);
            }
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Bitmap_.Save(memoryStream, ImageFormat.Png);
                Byte[] bytes = new Byte[memoryStream.Length];
                memoryStream.Position = 0;
                memoryStream.Read(bytes, 0, (int)bytes.Length);
                Base64String_ = Convert.ToBase64String(bytes, 0, bytes.Length);
            }
            Base64String_ = "data:image/png;base64," + Base64String_;
        }
    }
}