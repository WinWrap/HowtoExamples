using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ww2014b
{
    public partial class LogPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //LabelLogPage.Text = (string)Session["Error"];
            //TextBox1.Text = Session["Error"].ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Default.aspx");
        }
    }
}