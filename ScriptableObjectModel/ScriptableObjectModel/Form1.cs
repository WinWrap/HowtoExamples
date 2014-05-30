using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ScriptableExample1
{
    public partial class Form1 : Form, IDrawing
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ScriptingLanguage.SetDrawing(this);
            basicIdeCtl1.AddScriptableObjectModel(typeof(ScriptingLanguage));
            basicIdeCtl1.FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\..\..\Macro1.bas";
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            EraseLines();
        }

        private Stack<int> _iter;
        private int _lim = 20;
        
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            _iter = new Stack<int>(Enumerable.Range(1, _lim).Reverse());
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int w = pictureBox1.Width;
            int h = pictureBox1.Height;
            int i = _iter.Pop();
            DrawEllipse(0, 0, w * i / _lim - 1, h * i / _lim - 1);
            if (_iter.Count == 0)
                timer1.Enabled = false;
        }

        // draw an ellipse into the drawing area
        public void DrawEllipse(int x1, int y1, int x2, int y2)
        {
            Rectangle rect = new Rectangle(x1, y1, x2, y2);
            using (Graphics g = Graphics.FromImage(_drawArea))
            {
                g.DrawEllipse(Pens.Red, rect);
            }
            pictureBox1.Image = _drawArea;
        }

        private Bitmap _drawArea;

        #region IDrawing

        // get the width of the drawing area
        public int PictureWidth { get { return pictureBox1.Width; } }

        // get the height of the drawing area
        public int PictureHeight { get { return pictureBox1.Height; } }

        // draw a line into the drawing area
        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            using (Graphics g = Graphics.FromImage(_drawArea))
            {
                g.DrawLine(
                    new Pen(Color.Blue, 2f),
                    new Point(x1, y1),
                    new Point(x2, y2));
            }
            pictureBox1.Image = _drawArea;
        }

        // erase all the lines from the drawing area
        public void EraseLines()
        {
            _drawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.Image = _drawArea;
        }

        #endregion
    }
}
