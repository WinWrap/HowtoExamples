using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MessengerService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MessengerService.svc or MessengerService.svc.cs at the Solution Explorer and start debugging.
    public class MessengerService : IMessengerService
    {
        public static string version = "12 (vmsize=large)";
        static string winwrapTraceFileName = AppDomain.CurrentDomain.GetData("DataDirectory") + @"\winwrap.txt";
        static object lock_ = new object();

        [System.Diagnostics.ConditionalAttribute("DEBUG")]
        static void AppendToLog(string text)
        {
            lock (lock_)
            {
                using (System.IO.StreamWriter sw = System.IO.File.AppendText(winwrapTraceFileName))
                {
                    sw.WriteLine(DateTime.UtcNow.ToString() + " [" + System.Threading.Thread.CurrentThread.ManagedThreadId + "] " + text);
                }
            }
        }

        static public void DeleteLog()
        {
            if (System.IO.File.Exists(winwrapTraceFileName))
                System.IO.File.Delete(winwrapTraceFileName);
        }

        static public string ReadLog()
        {
#if DEBUG
            string text = string.Format("Contents of {0}:\r\n", winwrapTraceFileName);
            if (!System.IO.File.Exists(winwrapTraceFileName))
                text += "(no file)";
            else
                text += System.IO.File.ReadAllText(winwrapTraceFileName);
            return text;
#else
            return "(production release)";
#endif
        }

        public string SendMessage(string name)
        {
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
                    basicNoUIObj.AddExtension("$Feature ResolveGlobalsFirst False", null);
                    basicNoUIObj.AddExtension("$Feature WWB-COM Off", null);
                    basicNoUIObj.AddExtension("$Feature WWB.NET On", null);
                    basicNoUIObj.HiddenCode = @"'#Langauge ""WWB.NET""
Public GlobalVariable As String
";
                    basicNoUIObj.RunThis(@"Sub Main
    GlobalVariable = ""From WinWrap Basic: Hello ""
    Wait 1
End Sub");
                    string s = basicNoUIObj.EvaluateVariant(@"GlobalVariable").ToString();
                    return "[" + version + "] " + s + name;
                }
                catch (Exception ex)
                {
                    AppendToLog("Exception: " + ex.Message + "\r\n" + ex.StackTrace.ToString());
                    return "EXCEPTION";
                }
            }
        }
    }
}
