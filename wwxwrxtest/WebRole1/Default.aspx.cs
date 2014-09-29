using System;

/*
 * Publish cloud service (wwxwrxtest):
 *   1a) fully deleted service, webrole, deployment, and storage
 *   1b) new publish profile (manage and remove existing profile)
 *   1c) use "North Central US", Locally Redundant
 *   or
 *   2a) running service, et.
 *   2b) existing publish profile
 */

namespace WebRole1
{
    public partial class Default : System.Web.UI.Page
    {
        string winwrapTraceFileName = AppDomain.CurrentDomain.GetData("DataDirectory") + @"\winwrap.txt";
        static object lock_ = new object();

        [System.Diagnostics.ConditionalAttribute("DEBUG")]
        void AppendToLog(string text)
        {
            lock (lock_)
            {
                using (System.IO.StreamWriter sw = System.IO.File.AppendText(winwrapTraceFileName))
                {
                    sw.WriteLine(DateTime.UtcNow.ToString() + " [" + System.Threading.Thread.CurrentThread.ManagedThreadId + "] " + text);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(winwrapTraceFileName) && Request["ResetTrace"] == "yes")
                System.IO.File.Delete(winwrapTraceFileName);

            AppendToLog("Creating BasicNoUIObj");
            using (var basicNoUIObj = new WinWrap.Basic.BasicNoUIObj())
            {
#if DEBUG
                basicNoUIObj.DebugTrace += (o2, e2) => { AppendToLog(e2.Text); };
#endif
                basicNoUIObj.Secret = new Guid(Utils.GetPatternString("wwxwrxtest", "Guid[(]\"(.*)\"[)]"));
                //basicNoUIObj.Secret = Guid.NewGuid();
                try
                {
                    basicNoUIObj.Initialize();
                    AppendToLog("Turn on BasicNoUIObj tracing");
#if DEBUG
                    basicNoUIObj.Trace(WinWrap.Basic.TraceConstants.All);
#endif
                }
                catch (Exception ex)
                {
                    AppendToLog("Exception: " + ex.Message + "\r\n" + ex.StackTrace.ToString());
                    throw ex;
                }

                Button1.Text = basicNoUIObj.Evaluate("4+3");
            }
            TextBox1.Text = System.IO.File.ReadAllText(winwrapTraceFileName);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Button1.Text = DateTime.Now.ToString();
            using (var basicNoUIObj = new WinWrap.Basic.BasicNoUIObj())
            {
                basicNoUIObj.Secret = new Guid(Utils.GetPatternString("wwxwrxtest", "Guid[(]\"(.*)\"[)]"));
                basicNoUIObj.Initialize();
                //Button1.Text = basicNoUIObj.Evaluate("4+3");
                Button1.Text = basicNoUIObj.Evaluate("System.DateTime.Now.ToString()");
            }
        }
    }
}