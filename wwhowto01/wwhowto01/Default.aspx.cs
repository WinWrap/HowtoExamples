﻿using ScriptingModel;
using System;
using System.Diagnostics;
using System.IO;
using VBdotNet;
using System.Web.UI.WebControls;

/*
 * website monitoring
 *   Azure tools
 *   built
 * pass timelimit to error msg
 * failed web silently with no cert
 * namespace names
 * Button1 -> ButtonSolve
 * user and developer macroes
 * http://ww-classobjs.azurewebsites.net/
*/

namespace ww_classobjs
{
    public partial class Default : System.Web.UI.Page, IAppModel
    {
        private bool timedout_;
        private DateTime timelimit_;
        private ClientImage clientImage_;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetTextBoxes("SSS");
            }
            clientImage_ = new ClientImage(500, 500);

            ScriptingLanguage.SetAppModel(this);
        }

        private void RunWinWrap()
        {
            try
            {
                using (var basicNoUIObj = new WinWrap.Basic.BasicNoUIObj())
                {
                    basicNoUIObj.Begin += basicNoUIObj_Begin;
                    basicNoUIObj.DoEvents += basicNoUIObj_DoEvents;
                    basicNoUIObj.ErrorAlert += basicNoUIObj_ErrorAlert;
                    basicNoUIObj.Pause_ += basicNoUIObj_Pause_;
                    //#if DEBUG
                    // Debug.Print in a script shown on webpage when running locally
                    basicNoUIObj.DebugPrint += basicNoUIObj_DebugPrint;
                    //#endif
                    basicNoUIObj.Secret = new Guid(Utils.GetPatternString("wwhowto01", "Guid[(]\"(.*)\"[)]"));
                    basicNoUIObj.Initialize();
                    basicNoUIObj.AddScriptableObjectModel(typeof(ScriptingLanguage));
                    //basicNoUIObj.AddReference(typeof(String).Assembly);
                    //Button1.Text = basicNoUIObj.Evaluate("2+3");
                    basicNoUIObj.VirtualFileSystem = new VirtualFileSystem();
                    basicNoUIObj.RunFile("Macro1.bas");
                    DrawTriangle();
                    ImageUser.ImageUrl = clientImage_.ImageUrl();
                }
            }
            catch (Exception e)
            {
                AppendToTextBox1(e.Message);
            }
        }

        private void DrawTriangle()
        {
            int sa = Convert.ToInt32(SideA);
            int sb = Convert.ToInt32(SideB);
            int sc = Convert.ToInt32(SideC);
            int aa = Convert.ToInt32(AngleA);
            int ab = Convert.ToInt32(AngleB);
            int ac = Convert.ToInt32(AngleC);
            int w = clientImage_.Width;
            int h = clientImage_.Height;
            clientImage_.DrawLine(0, 0, w - 1, h - 1);
            int x1, x2, y1, y2;
            x1 = w * 3 / 10;
            y1 = h * 9 / 10;
            int scale = w / sc / 2;
            x2 = x1 + sc * scale;
            y2 = y1;
            clientImage_.DrawLine(x1, y1, x2, y2);
            x1 = w * 3 / 10;
            y1 = h * 9 / 10;
            x2 = x1 + sb * scale * Convert.ToInt32(Math.Cos(AngleA));
            y2 = y1 - sb * scale * Convert.ToInt32(Math.Sin(AngleA));
            clientImage_.DrawLine(x1, y1, x2, y2);
            x2 = x1 + sc * scale;
            y2 = y1;
            x2 = x2 - sa * scale * Convert.ToInt32(Math.Cos(AngleB));
            y2 = y2 - sa * scale * Convert.ToInt32(Math.Sin(AngleB));
            //clientImage_.DrawLine(x1, y1, x2, y2);
        }

        void basicNoUIObj_DebugPrint(object sender, WinWrap.Basic.Classic.TextEventArgs e)
        {
            AppendToTextBox1(e.Text);
        }

        void basicNoUIObj_Pause_(object sender, EventArgs e)
        {
            WinWrap.Basic.BasicNoUIObj basicNoUIObj = sender as WinWrap.Basic.BasicNoUIObj;
            if (basicNoUIObj.Error == null)
            {
                AppendToTextBox1(Utils.FormatTimeoutError(basicNoUIObj, timedout_));
            }
            // Script execution has paused, terminate the script
            basicNoUIObj.Run = false;
        }

        void basicNoUIObj_ErrorAlert(object sender, EventArgs e)
        {
            WinWrap.Basic.BasicNoUIObj basicNoUIObj = sender as WinWrap.Basic.BasicNoUIObj;
            AppendToTextBox1(Utils.FormatError(basicNoUIObj.Error, basicNoUIObj.VirtualFileSystem));
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

        void basicNoUIObj_Begin(object sender, EventArgs e)
        {
            timedout_ = false;
            timelimit_ = DateTime.Now + new TimeSpan(0, 0, 1);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            RunWinWrap();
            Button1.Text = "Solved";
        }

        private void AppendToTextBox1(string msg)
        {
            TextBox1.Text += msg;
            TextBox1.Visible = true;
        }

        #region IAppModel

        public double SideA
        {
            get
            {
                return texttolength(TextBoxSideA);
            }
            set
            {
                TextBoxSideA.Text = value.ToString();
            }
        }

        public double SideB
        {
            get
            {
                return texttolength(TextBoxSideB);
            }
            set
            {
                TextBoxSideB.Text = value.ToString();
            }
        }

        public double SideC
        {
            get
            {
                return texttolength(TextBoxSideC);
            }
            set
            {
                TextBoxSideC.Text = value.ToString();
            }
        }

        public double AngleA
        {
            get
            {
                return texttodegrees(TextBoxAngleA);
            }
            set
            {
                TextBoxAngleA.Text = (value * 180 / Math.PI).ToString();
            }
        }

        public double AngleB
        {
            get
            {
                return texttodegrees(TextBoxAngleB);
            }
            set
            {
                TextBoxAngleB.Text = (value * 180 / Math.PI).ToString();
            }
        }

        public double AngleC
        {
            get
            {
                return texttodegrees(TextBoxAngleC);
            }
            set
            {
                TextBoxAngleC.Text = (value * 180 / Math.PI).ToString();
            }
        }

        public ClientImage TriangleImage
        {
            get
            {
                return clientImage_;
            }
            set
            {
                clientImage_ = value;
            }
        }

        #endregion

        private double texttolength(TextBox box)
        {
            //string s = box.Enabled ? box.Text : "";
            string s = box.Text;
            double value = 0;
            double.TryParse(s, out value);
            return value;
        }

        private double texttodegrees(TextBox box)
        {
            //string s = box.Enabled ? box.Text : "";
            string s = box.Text;
            double value = 0;
            double.TryParse(s, out value);
            return value * Math.PI / 180;
        }

        private void SetTextBoxes(string datatype)
        {
            Button1.Text = "Solve";
            TextBoxSideA.Enabled = false;
            TextBoxSideB.Enabled = false;
            TextBoxSideC.Enabled = false;
            TextBoxAngleA.Enabled = false;
            TextBoxAngleB.Enabled = false;
            TextBoxAngleC.Enabled = false;
            /*TextBoxSideA.Text = "3";
            TextBoxSideB.Text = "4";
            TextBoxSideC.Text = "5";
            TextBoxAngleA.Text = "36.869897645844";
            TextBoxAngleB.Text = "53.130102354156";
            TextBoxAngleC.Text = "90";*/
            switch (datatype)
            {
                case "SSS":
                    TextBoxSideA.Enabled = true;
                    TextBoxSideB.Enabled = true;
                    TextBoxSideC.Enabled = true;
                    ImageTriangle.ImageUrl = @"Images\triangle-sss.bmp";
                    break;
                case "SAS":
                    TextBoxSideB.Enabled = true;
                    TextBoxSideC.Enabled = true;
                    TextBoxAngleA.Enabled = true;
                    ImageTriangle.ImageUrl = @"Images\triangle-sas.bmp";
                    break;
                case "SSA":
                    TextBoxSideB.Enabled = true;
                    TextBoxSideC.Enabled = true;
                    TextBoxAngleB.Enabled = true;
                    ImageTriangle.ImageUrl = @"Images\triangle-ssa.bmp";
                    break;
                case "AAA":
                    ImageTriangle.ImageUrl = @"Images\triangle-aaa.bmp";
                    break;
                case "ASA":
                    TextBoxSideC.Enabled = true;
                    TextBoxAngleA.Enabled = true;
                    TextBoxAngleB.Enabled = true;
                    ImageTriangle.ImageUrl = @"Images\triangle-asa.bmp";
                    break;
                case "AAS":
                    TextBoxSideC.Enabled = true;
                    TextBoxAngleA.Enabled = true;
                    TextBoxAngleC.Enabled = true;
                    ImageTriangle.ImageUrl = @"Images\triangle-aas.bmp";
                    break;
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTextBoxes(RadioButtonList1.Text);
        }
    }
}