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
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var basicNoUIObj = new WinWrap.Basic.BasicNoUIObj())
            {
                basicNoUIObj.Secret = new Guid(Utils.GetPatternString("wwwr140916b", "Guid[(]\"(.*)\"[)]"));
                basicNoUIObj.Initialize();
                Button1.Text = basicNoUIObj.Evaluate("4+3");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}