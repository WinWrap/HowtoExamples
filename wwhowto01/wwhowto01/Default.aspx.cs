using AppModel;
using System;
using System.Diagnostics;
using VBdotNet;
using System.IO;

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
 * http://stackoverflow.com/questions/2784878/continuously-reading-from-a-stream
 * http://ww-classobjs.azurewebsites.net/
*/

namespace ww_classobjs
{
    public partial class Default : System.Web.UI.Page, IAppModel
    {
        private bool timedout_;
        private DateTime timelimit_;
        //MemoryStream ms_;
        //StreamWriter sw_;

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptingLanguage.SetAppModel(this);
            //(new Triangle()).Test(); // test stackoverflow on ?x=y
            //Debug.Print((new Triangle()).Test()); // Debug is suppressed in released code
            /*Triangle t = new Triangle(10, 10, 10, 0, 0, 0);
            t.Solve();
            Debug.Print(t.ToString());*/
            //MyListener3();
            if (!IsPostBack)
            {
                CreateMyListener();
                Debug.Print("Page_Load: " + DateTime.Now.ToString());
                PrintMyListener();
                //Triangle t = new Triangle(10, 10, 10, 0, 0, 0);
            }
            else
            {

            }
            //Debug.Print("Page_Load: " + DateTime.Now.ToString());
            //PrintMyListener();
            //PrintMyListener();
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            //PrintMyListener();
            //ms_.Close();
        }

        private void CreateMyListener()
        {
            //ms_ = new MemoryStream();
            var ms = new MemoryStream();
            Session["ms_"] = ms;
            //sw_ = new StreamWriter(ms_);
            var sw = new StreamWriter(ms);
            Session["sw_"] = sw;
            //TextWriterTraceListener objTraceListener = new TextWriterTraceListener(sw_);
            TextWriterTraceListener objTraceListener = new TextWriterTraceListener(sw);
            System.Diagnostics.Trace.Listeners.Add(objTraceListener); // xxx System.Diagnostics ?
        }

        private void PrintMyListener()
        {
            //sw_.Flush();
            StreamWriter sw = (StreamWriter)Session["sw_"];
            sw.Flush();
            //ms_.Position = 0;
            MemoryStream ms = (MemoryStream)Session["ms_"];
            ms.Position = 0;
            /*var sr = new StreamReader(ms_);
            var myStr = sr.ReadToEnd();
            AppTrace(myStr);*/
            //using (var sr = new StreamReader(ms_))
            using (var sr = new StreamReader(ms))
            {
                var myStr = sr.ReadToEnd();
                AppTrace(myStr);
            }
        }

        private void MyListener3()
        {
            using (var ms = new MemoryStream())
            {
                var sw = new StreamWriter(ms);
                TextWriterTraceListener objTraceListener = new TextWriterTraceListener(sw);
                System.Diagnostics.Trace.Listeners.Add(objTraceListener);
                Debug.Print(DateTime.Now.ToString());
                sw.Flush();
                ms.Position = 0;
                var sr = new StreamReader(ms);
                var myStr = sr.ReadToEnd();
            }
        }

        private void MyListener2()
        {
            using (var ms = new MemoryStream())
            {
                var sw = new StreamWriter(ms);
                sw.WriteLine("Hello World");
                sw.Flush();
                ms.Position = 0;
                var sr = new StreamReader(ms);
                var myStr = sr.ReadToEnd();
                //Console.WriteLine(myStr);
            }
        }

        private void MyListener()
        {
            //string afile = "C:\\AppLog.txt";
            string afile = @"C:\Users\Public\Documents\AppLog.txt";
            FileStream objStream = new FileStream(afile, FileMode.OpenOrCreate);
            TextWriterTraceListener objTraceListener = new TextWriterTraceListener(objStream);
            //Trace.Listeners.Add(objTraceListener);
            System.Diagnostics.Trace.Listeners.Add(objTraceListener); // xxx
            System.Diagnostics.Trace.WriteLine("Hello 15Seconds Reader -- This is first trace message");
            System.Diagnostics.Trace.WriteLine("Hello again -- This is second trace message");
            Debug.WriteLine("Hello again -- This is first debug message");
            Debug.Print(DateTime.Now.ToString());
            System.Diagnostics.Trace.Flush();
            objStream.Close();
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
                    basicNoUIObj.DebugPrint += basicNoUIObj_DebugPrint;
                    basicNoUIObj.Secret = new Guid(Utils.GetPatternString("ww-classobjs", "Guid[(]\"(.*)\"[)]"));
                    basicNoUIObj.Initialize();
                    basicNoUIObj.AddScriptableObjectModel(typeof(ScriptingLanguage));
                    //Button1.Text = basicNoUIObj.Evaluate("2+3");
                    basicNoUIObj.VirtualFileSystem = new VirtualFileSystem();
                    basicNoUIObj.RunFile("Macro2.bas");
                }
            }
            catch (Exception e)
            {
                string s = e.Message;
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
            AppendToTextBox1(Utils.FormatError(basicNoUIObj.Error));
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
            if (TextBox1.Text.Length != 0)
                msg = Environment.NewLine + msg;
            AppendToTextBox1(msg);
        }

        #endregion
    }
}