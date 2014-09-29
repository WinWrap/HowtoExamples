using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole1
{
    public partial class _Default : Page
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
                basicNoUIObj.Secret = new Guid(Utils.GetPatternString("wwwr140916b", "Guid[(]\"(.*)\"[)]"));
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

        }
    }
}