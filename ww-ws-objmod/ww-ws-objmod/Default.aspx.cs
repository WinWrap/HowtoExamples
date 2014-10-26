using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
using TryVB;

/*
 * http://ww-ws-objmod.azurewebsites.net/
 * 
 * array with emptys
 * named arguments
 * no auto-implemented property
 * use of: Public SideB As Decimal => no such property or method.
 * 
 * remove apphost sorts
 * check if new dll downloaded
 */

namespace ww_ws_objmod
{
    public partial class Default : System.Web.UI.Page, IAppModel
    {
        public ClientImage ClientImage { get; private set; }

        private bool timedout_;
        private DateTime timelimit_;

        protected void Page_Load(object sender, EventArgs e)
        {
            /*List<double> l = new List<double>() { 10, 10, 10 };
            CosineRule cr = new CosineRule();
            string s = cr.xToString();
            double d = cr.Solve();*/
            /*var x = new TryVB.NaNTests();
            x.RunTests();*/
            ScriptingLanguage.SetAppModel(this);
            TryVB.Triangle triangle = new TryVB.Triangle();
            triangle.Parts.Add(new TryVB.TrianglePart(10, aangle: 1.0471975511966));
            triangle.Parts.Add(new TryVB.TrianglePart(0, 1.0471975511966));
            triangle.Parts.Add(new TryVB.TrianglePart(0, 0));
            triangle.Solve();
            string s = triangle.MakeString();
    /*triangle.Parts.Add(New TrianglePart(10, aangle:=1.0471975511966));
    triangle.Parts.Add(New TrianglePart(Nothing, 1.0471975511966));
    triangle.Parts.Add(New TrianglePart(Nothing, Nothing));
    triangle.Solve();*/
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int t = Convert.ToInt32(Top1.Value);
            t = 80; // button actual height
            int w = Convert.ToInt32(Width1.Value) - 30;
            int h = (Convert.ToInt32(Height1.Value) - t - 30) * 92 / 100;
            ClientImage = new ClientImage(w, h);
            WinWrapRunFile();
            if (TextBox1.Text.Length == 0) // xxx
            {
                Image2.ImageUrl = CreateImageUrl(ClientImage.Bitmap);
            }
        }
        // move xxx
        private string CreateImageUrl(Bitmap bitmap)
        {
            string base64String = "";
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                Byte[] bytes = new Byte[memoryStream.Length];
                memoryStream.Position = 0;
                memoryStream.Read(bytes, 0, (int)bytes.Length);
                base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            }
            return "data:image/png;base64," + base64String;
        }

        private void WinWrapRunFile()
        {
            try
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
            catch (Exception e)
            {
                string s = e.Message;
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
                TextBox1.Text = Utils.FormatTimeoutError(basicNoUIObj, timedout_);
                TextBox1.Visible = true;
                //Response.Redirect("/LogPage.aspx");
            }
            // Script execution has paused, terminate the script
            basicNoUIObj.Run = false;
        }

        #region IAppModel

        public List<object> AppSortSides(List<object> list)
        {
            list.Sort((i1, i2) =>
            {
                dynamic x = i1;
                dynamic y = i2;
                return x.Side.CompareTo(y.Side);
            });
            return list;
        }

        public List<object> AppSortAngles(List<object> list)
        {
            list.Sort((i1, i2) =>
            {
                dynamic x = i1;
                dynamic y = i2;
                return x.Angle.CompareTo(y.Angle);
            });
            return list;
        }

        public void AppTrace(string msg)
        {
            TextBox1.Text = msg + Environment.NewLine + TextBox1.Text;
            TextBox1.Visible = true;
        }

        #endregion
    }
}