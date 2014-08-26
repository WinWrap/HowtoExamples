using System;
using System.IO;
using System.Text;

namespace ww2014b
{
    public partial class WebForm1 : System.Web.UI.Page, IAppModel
    {
        public AClass TheAClass { get; private set; }

        private bool timedout_;
        private DateTime timelimit_;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Error"] = "";
            }
            TheAClass = new AClass();
            ScriptingLanguage.SetAppModel(this);
            using (var basicNoUIObj = new WinWrap.Basic.BasicNoUIObj())
            {
                basicNoUIObj.Begin += basicNoUIObj_Begin;
                basicNoUIObj.DoEvents += basicNoUIObj_DoEvents;
                basicNoUIObj.ErrorAlert += basicNoUIObj_ErrorAlert;
                basicNoUIObj.Pause_ += basicNoUIObj_Pause_;
                basicNoUIObj.Secret = new Guid(Utils.GetPatternString("ww2014b", "Guid[(]\"(.*)\"[)]"));
                basicNoUIObj.Initialize();
                basicNoUIObj.AddScriptableObjectModel(typeof(ScriptingLanguage));
                //Button1.Text = basicNoUIObj.Evaluate("2+3");
                string path = Utils.MacroPath("Macro1.bas");
                basicNoUIObj.RunFile(string.Format(@"""{0}""", path));
                if (!basicNoUIObj.LoadModule(Utils.MacroPath("Globals.bas")))
                {
                    Session["Error"] = Utils.FormatError(basicNoUIObj.Error);
                    Response.Redirect("/LogPage.aspx");
                }
                else
                {
                    using (var module = basicNoUIObj.ModuleInstance(Utils.MacroPath("AClass.bas"), false))
                    {
                        if (module == null)
                        {
                            Session["Error"] = Utils.FormatError(basicNoUIObj.Error);
                            Response.Redirect("/LogPage.aspx");
                        }
                        else
                        {
                            // Execute script code via an event
                            ScriptingLanguage.TheAClass.Start("Default.aspx");
                        }
                    }
                }
            }
            TheAClass = null;
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
            Session["Error"] = Utils.FormatError(basicNoUIObj.Error);
            Response.Redirect("/LogPage.aspx");
            //Server.Transfer("LogPage.aspx", true);
        }

        void basicNoUIObj_Pause_(object sender, EventArgs e) // xxx
        {
            WinWrap.Basic.BasicNoUIObj basicNoUIObj = sender as WinWrap.Basic.BasicNoUIObj;
            if (basicNoUIObj.Error == null)
            {
                Session["Error"] = Utils.FormatTimeoutError(basicNoUIObj, timedout_);
                Response.Redirect("/LogPage.aspx");
            }
            // Script execution has paused, terminate the script
            basicNoUIObj.Run = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button1.Text = DateTime.Now.ToString();
        }

        #region IAppModel

        public int PictureWidth { get { return 200; } }

        public void EraseLines()
        {
            string s = "EraseLines asdf";
            Button1.Text = s;
        }

        public void ErrorAppend(string serror)
        {
            Session["Error"] = Session["Error"].ToString() + Environment.NewLine + serror;
        }

        #endregion
    }
}