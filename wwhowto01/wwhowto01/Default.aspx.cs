using ScriptingModel;
using System;
using System.Diagnostics;
using System.IO;
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
 * IsPostBack ?
 * Session in Azure ?
 * failed web silently with no cert
 * ScriptingLanguage.cs does not belong to the project being debugged
 * input radians, output degrees
 * http://stackoverflow.com/questions/2784878/continuously-reading-from-a-stream
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
            bool b = Test.RunAll();
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {

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
        }

        private void AppendToTextBox1(string msg)
        {
            TextBox1.Text += msg;
            TextBox1.Visible = true;
        }

        #region IAppModel

        public void AppTrace(string msg)
        {
            AppendToTextBox1(msg + Environment.NewLine);
        }

        #endregion
    }
}