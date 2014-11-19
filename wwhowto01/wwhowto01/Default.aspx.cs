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
 * http://ww-classobjs.azurewebsites.net/
*/

namespace ww_classobjs
{
    public partial class Default : System.Web.UI.Page, IAppModel
    {
        private bool timedout_;
        private DateTime timelimit_;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetTextBoxes("SSS");
            }
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
                }
            }
            catch (Exception e)
            {
                AppendToTextBox1(e.Message);
            }
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

        #endregion

        private double texttolength(TextBox box)
        {
            string s = box.Enabled ? box.Text : "";
            double value = 0;
            double.TryParse(s, out value);
            return value;
        }

        private double texttodegrees(TextBox box)
        {
            string s = box.Enabled ? box.Text : "";
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
            TextBoxSideA.Text = "3";
            TextBoxSideB.Text = "4";
            TextBoxSideC.Text = "5";
            TextBoxAngleA.Text = "36.869897645844";
            TextBoxAngleB.Text = "53.130102354156";
            TextBoxAngleC.Text = "90";
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