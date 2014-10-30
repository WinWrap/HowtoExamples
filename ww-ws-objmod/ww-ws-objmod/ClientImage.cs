using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace ww_ws_objmod
{
    [Scriptable]
    public class ClientImage
    {
        private Bitmap Bitmap { get; set; }

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

        [Scriptable]
        public void DrawRectangle(int x, int y, int w, int h)
        {
            Pen bluePen = new Pen(Color.Blue, 2);
            using (var graphics = Graphics.FromImage(Bitmap))
            {
                graphics.DrawRectangle(bluePen, x, y, w, h);
            }
        }

        [Scriptable]
        public void FillRectangle(int x, int y, int width, int height)
        {
            SolidBrush brush = new SolidBrush(Color.Gold);
            using (var graphics = Graphics.FromImage(Bitmap))
            {
                graphics.FillRectangle(brush, x, y, width, height);
            }
        }

        [Scriptable]
        public void Gradient()
        {
            LinearGradientBrush linGrBrush = new LinearGradientBrush(
   new Point(20, 10),
   new Point(20, 100),
   Color.FromArgb(255, 255, 0, 0),   // Opaque red
   Color.FromArgb(255, 0, 0, 255));  // Opaque blue

            Pen pen = new Pen(linGrBrush);

            using (var graphics = Graphics.FromImage(Bitmap))
            {
                graphics.DrawLine(pen, 0, 10, 200, 10);
                graphics.FillEllipse(linGrBrush, 0, 30, 200, 100);
                graphics.FillRectangle(linGrBrush, 0, 155, 500, 30);
            }
        }

        [Scriptable]
        public void FillPolygon()
        {
            // Create solid brush.
            SolidBrush blueBrush = new SolidBrush(Color.Blue);

            // Create points that define polygon.
            PointF point1 = new PointF(50.0F, 50.0F);
            PointF point2 = new PointF(100.0F, 25.0F);
            PointF point3 = new PointF(200.0F, 5.0F);
            PointF point4 = new PointF(250.0F, 50.0F);
            PointF point5 = new PointF(300.0F, 100.0F);
            PointF point6 = new PointF(350.0F, 200.0F);
            PointF point7 = new PointF(250.0F, 250.0F);
            PointF[] curvePoints = { point1, point2, point3, point4, point5, point6, point7 };

            // Define fill mode.
            FillMode newFillMode = FillMode.Winding;

            // Fill polygon to screen.
            using (var graphics = Graphics.FromImage(Bitmap))
            {
                graphics.FillPolygon(blueBrush, curvePoints, newFillMode);
            }
        }

        [Scriptable]
        public void FillTriangle()
        {
            int x = 50;
            int y = 25;
            int h = 100;
            int w = 200;
            Brush brush = new LinearGradientBrush(
                  new Point(x, y),
                  new Point(0, h),
                  Color.FromArgb(255, 255, 0, 0),
                  Color.FromArgb(255, 0, 0, 255));
                  /*MakeColor(255, c), //fully opaque color
                  MakeColor(0, c)); //fully transparent color*/

            using (var graphics = Graphics.FromImage(Bitmap))
            {
            graphics.FillPolygon(brush, new PointF[] {
        new PointF(0, 0),
        new PointF(0, h),
        new PointF(w, h)
    });
            }
        }
    }
}