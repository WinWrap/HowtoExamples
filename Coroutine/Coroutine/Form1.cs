using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WinWrap.Basic;

namespace Coroutine
{
    public partial class Form1 : Form
    {
        string root;
        BasicNoUIObj basicNoUI;
        int nextengineid;
        bool initialized;

        public Form1()
        {
            InitializeComponent();
            // find the ScriptLoctTest.bas file
            root = typeof(Form1).Assembly.Location;
            while (!File.Exists(root + @"\ScriptLockTest.bas"))
            {
                root = Path.GetDirectoryName(root);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // switch to each tab containing a BasicIdeCtl to create the control.
            tabControl1.SelectedIndex = 1;
            tabControl1.SelectedIndex = 0;
            // initialize the script support for ScriptLock
            InitializeBasicIDE(basicIdeCtl1);
            InitializeBasicIDE(basicIdeCtl2);
            // load ScriptLockTest.bas into both IDEs
            basicIdeCtl1.FileName = root + @"\ScriptLockTest.bas";
            basicIdeCtl2.FileName = root + @"\ScriptLockTest.bas";
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (!initialized)
            {
                initialized = true;
                // application executes from inside this function
                EnableCoroutining(this);
                // application is terminating
            }
        }

        private void EnableCoroutining(Form host)
        {
            try
            {
                using (basicNoUI = new BasicNoUIObj())
                {
                    // replace with your application's secret
                    basicNoUI.Secret = new Guid("00000000-0000-0000-0000-000000000000");
                    basicNoUI.Initialize();
                    basicNoUI.AttachToForm(host, ManageConstants.OnDoEvents);
                    // this application's UI executes from this script
                    // Note: the application will not return from this call
                    // until the application is closing down.
                    basicNoUI.RunThis("Sub Main\r\nDo\r\nWait .01\r\nLoop\r\nEnd Sub");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + ": " + ex.Message);
            }
            finally
            {
                if (basicNoUI != null)
                {
                    // When we get here, the application is exiting from the infnite loop
                    basicNoUI = null;
                    host.Close();
                }
                else
                {
                    MessageBox.Show("Not able to enable parallel processing of scripts!");
                }
            }
        }

        private void InitializeBasicIDE(BasicIdeCtl basic)
        {
            // each IDE has a unique identifier
            int engineid = ++nextengineid;
            // consolidate debug output
            basic.DebugPrint += (sender, e) =>
            {
                textBox1.AppendText(string.Format("IDE#{0}@{1:HH:mm:ss}: {2}", engineid, DateTime.Now, e.Text));
            };
            // when the script ends release all locks for this IDE
            basic.End += (sender, e) =>
            {
                Extensions.HiddenCodeAux.UnlockAccessTokens(engineid);
            };
            // select the appropriate tab when an error occurs
            basic.HandleError += (sender, e) =>
            {
                TabPage tabPage = (TabPage)basic.Parent;
                tabControl1.SelectedTab = tabPage;
            };
            // select the appropriate tab when the form needs to be shown
            basic.ShowForm += (sender, e) =>
            {
                TabPage tabPage = (TabPage)basic.Parent;
                tabControl1.SelectedTab = tabPage;
            };
            // don't use automatic form closing
            basic.AttachToForm(null, ManageConstants.Disconnecting);
            // add .NET support code extensions to the HiddenCode
            basic.AddExtension("*xHiddenCode*|#", typeof(Extensions.HiddenCodeAux).Assembly);
            // define the ScriptLock function used for exclusive script code access
            basic.HiddenCode = "'#Language \"WWB.NET\"\r\n" + Extensions.HiddenCodeAux.GetHiddenCodeSnippet(engineid);
#if DEBUG
            if (engineid == 1)
            {
                // show error if HiddenCode fails
                basic.ErrorLog += HiddenCodeErrorLog;
            }
#endif
            // force HiddenCode to be loaded
            string s = basic.Evaluate("Now");

#if DEBUG
            if (engineid == 1)
            {
                // show error if HiddenCode fails
                basic.ErrorLog -= HiddenCodeErrorLog;
            }
#endif
        }

#if DEBUG
        private void HiddenCodeErrorLog(object sender, EventArgs e)
        {
            IBasicNoUI basic = (IBasicNoUI)sender;
            string line = "???";
            if (basic.Error.File == "*xHiddenCode*")
            {
                line = basic.HiddenCode.Split(new string[] { "\r\n" }, StringSplitOptions.None)[basic.Error.Line - 1];
                line = line.Insert(basic.Error.Offset, "<here>");
            }

            MessageBox.Show(basic.Error.ToString() + "\n\n" + line);
        }
#endif

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // prevent form closing until WinWrap® Basic has exited all nested execution
            // prevent form closing if user cancels the request
            switch (basicIdeCtl1.Shutdown())
            {
                case -1: // Basic engine execution is nested.
                    timer1.Enabled = true;
                    e.Cancel = true;
                    break;
                case 0:
                    break;
                case 1: // cancelled by user
                    e.Cancel = true;
                    break;
            }

            // prevent form closing until WinWrap® Basic has exited all nested execution
            // prevent form closing if user cancels the request
            switch (basicIdeCtl2.Shutdown())
            {
                case -1: // Basic engine execution is nested.
                    timer1.Enabled = true;
                    e.Cancel = true;
                    break;
                case 0:
                    break;
                case 1: // cancelled by user
                    e.Cancel = true;
                    break;
            }

            if (!e.Cancel)
            {
                // all IDEs are idle, stop BasicNoUI script so the application will exit normally
                basicNoUI.Run = false;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // disconnect and release all COM objects used by WinWrap® Basic
            bool okay1 = basicIdeCtl1.Disconnect();
            bool okay2 = basicIdeCtl2.Disconnect();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!basicIdeCtl1.InEvent && !basicIdeCtl2.InEvent)
            {
                // all IDEs are idle, close application
                timer1.Enabled = false;
                Close();
            }
        }
    }
}
