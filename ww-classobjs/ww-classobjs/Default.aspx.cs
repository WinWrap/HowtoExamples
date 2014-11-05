using AppModel;
using System;
using System.Diagnostics;
using VBdotNet;

/*
 * try catch on side a > sides b + c
 *   VB.Net local: no catch, stackoverflowexception
 *   VB.Net Azure: no catch, no failure indication
 *   WWB.Net time limit exceeded
 *     what is time limit
 *     try increasing
 * put triangle code in separate macro, sub main
 * non-solvable triangle
 *   limit recursion
 *   verify sequencing
 *   try-catch when non-solvable is detectable
 *   // test stackoverflow on ?x=
 * website monitoring
 *   Azure tools
 *   built
 * assert (on internal illegal conditions)
 * abstract tests
 *   MS testing support in VS ?
 * spurious dot(: ".) in VS for "AppTrace("Solve: " & ex.ToString())"
 * common code for VB.Net WWB.Net
 * pass timelimit to error msg
 * ScriptingExtensions
 * Debug.Print listener
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
            ScriptingLanguage.SetAppModel(this);
            //(new Triangle()).Test(); // test stackoverflow on ?x=y
            //Debug.Print((new Triangle()).Test()); // Debug is suppressed in released code
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
                    basicNoUIObj.Secret = new Guid(Utils.GetPatternString("ww-classobjs", "Guid[(]\"(.*)\"[)]"));
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

        void basicNoUIObj_Pause_(object sender, EventArgs e)
        {
            WinWrap.Basic.BasicNoUIObj basicNoUIObj = sender as WinWrap.Basic.BasicNoUIObj;
            if (basicNoUIObj.Error == null)
            {
                TextBox1.Text = Utils.FormatTimeoutError(basicNoUIObj, timedout_);
                TextBox1.Visible = true;
            }
            // Script execution has paused, terminate the script
            basicNoUIObj.Run = false;
        }

        void basicNoUIObj_ErrorAlert(object sender, EventArgs e)
        {
            WinWrap.Basic.BasicNoUIObj basicNoUIObj = sender as WinWrap.Basic.BasicNoUIObj;
            TextBox1.Text = Utils.FormatError(basicNoUIObj.Error);
            TextBox1.Visible = true;
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
        }

        #region IAppModel

        public void AppTrace(string msg)
        {
            if (TextBox1.Text.Length != 0)
                TextBox1.Text = TextBox1.Text + Environment.NewLine;
            TextBox1.Text = TextBox1.Text + msg;
            TextBox1.Visible = true;
        }

        #endregion
    }
}