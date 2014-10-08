using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

/*
 * http://ww-ws-objmod.azurewebsites.net/
 */

namespace ww_ws_objmod
{
    public partial class Default : System.Web.UI.Page, IAppModel
    {
        public ClientImage ClientImage { get; private set; }
        public Bitmap Bitmap_;
        string Base64String_ = "";

        private bool timedout_;
        private DateTime timelimit_;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Button1.Text = DateTime.Now.ToString();
            CreateImage();
            //Image2.ImageUrl = Base64String_;
            Image2.ImageUrl = ClientImage.Base64String_;
        }

        private void CreateImage()
        {
            int t = Convert.ToInt32(Top1.Value);
            t = 80; // real button height
            int w = Convert.ToInt32(Width1.Value) - 30;
            int h = (Convert.ToInt32(Height1.Value) - t - 30) * 93 / 100;
            Bitmap_ = new Bitmap(w, h);
            ClientImage = new ClientImage(Bitmap_);
            ScriptingLanguage.SetAppModel(this);
            WinWrapRunFile();
            //DrawLine(0, 0, w - 1, h - 1);
        }

        private void DrawLine(int x1, int y1, int x2, int y2)
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

        private void WinWrapRunFile()
        {
            using (var basicNoUIObj = new WinWrap.Basic.BasicNoUIObj())
            {
                basicNoUIObj.Begin += basicNoUIObj_Begin;
                basicNoUIObj.DoEvents += basicNoUIObj_DoEvents;
                basicNoUIObj.ErrorAlert += basicNoUIObj_ErrorAlert;
                basicNoUIObj.Pause_ += basicNoUIObj_Pause_;
                basicNoUIObj.Secret = new Guid(Utils.GetPatternString("ww-ws-objmod", "Guid[(]\"(.*)\"[)]"));
                basicNoUIObj.Initialize();
                basicNoUIObj.AddScriptableObjectModel(typeof(ScriptingLanguage));
                //Button1.Text = basicNoUIObj.Evaluate("2+3");
                string path = Utils.MacroPath("Macro1.bas");
                basicNoUIObj.RunFile(string.Format(@"""{0}""", path));
            }
        }

        void basicNoUIObj_Begin(object sender, EventArgs e)
        {
            timedout_ = false;
            timelimit_ = DateTime.Now + new TimeSpan(0, 0, 1);
        }

        void basicNoUIObj_DoEvents(object sender, EventArgs e)
        {
            if (DateTime.Now >= timelimit_)
            {
                timedout_ = true;
                WinWrap.Basic.BasicNoUIObj basicNoUIObj = sender as WinWrap.Basic.BasicNoUIObj;
                // time timelimit has been reached, terminate the script
                basicNoUIObj.Run = false;
            }
        }

        void basicNoUIObj_ErrorAlert(object sender, EventArgs e)
        {
            WinWrap.Basic.BasicNoUIObj basicNoUIObj = sender as WinWrap.Basic.BasicNoUIObj;
            TextBox1.Text = Utils.FormatError(basicNoUIObj.Error);
            TextBox1.Visible = true;
            /*Session["Error"] = Utils.FormatError(basicNoUIObj.Error);
            Response.Redirect("/LogPage.aspx");
            //Server.Transfer("LogPage.aspx", true);*/
        }

        void basicNoUIObj_Pause_(object sender, EventArgs e)
        {
            WinWrap.Basic.BasicNoUIObj basicNoUIObj = sender as WinWrap.Basic.BasicNoUIObj;
            if (basicNoUIObj.Error == null)
            {
                Session["Error"] = Utils.FormatTimeoutError(basicNoUIObj, timedout_);
                Response.Redirect("/LogPage.aspx");
            }
            // Script execution has paused, terminate the script
            basicNoUIObj.Run = false;
        }

        #region IAppModel

        public int PictureWidth { get { return 200; } }

        public void EraseLines(string s)
        {
            //string s = "EraseLines asdf";
            Button1.Text = s;
        }

        /*public void DrawLine(string s)
        {
            Button1.Text = s;
        }*/
        /*public void ErrorAppend(string serror)
        {
            Session["Error"] = Session["Error"].ToString() + Environment.NewLine + serror;
        }*/

        #endregion
    }
}