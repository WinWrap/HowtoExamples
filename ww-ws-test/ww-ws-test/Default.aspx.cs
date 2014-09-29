using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ww_ws_test
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = System.DateTime.Now.ToString();
            using (var basicNoUIObj = new WinWrap.Basic.BasicNoUIObj())
            {
                basicNoUIObj.Secret = new Guid(Utils.GetPatternString("ww-ws-test", "Guid[(]\"(.*)\"[)]"));
                basicNoUIObj.Initialize();
                string sx = "x=" + (Request["x"] ?? "null");
                string s = "arg x=" + Request["x"].ToString();
                Label1.Text = basicNoUIObj.Evaluate(@"""System.DateTime.Now.ToString()""");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (var basicNoUIObj = new WinWrap.Basic.BasicNoUIObj())
            {
                basicNoUIObj.Secret = new Guid(Utils.GetPatternString("ww-ws-test", "Guid[(]\"(.*)\"[)]"));
                basicNoUIObj.Initialize();
                Label1.Text = basicNoUIObj.Evaluate("System.DateTime.Now.ToString()");
            }
        }
    }
}