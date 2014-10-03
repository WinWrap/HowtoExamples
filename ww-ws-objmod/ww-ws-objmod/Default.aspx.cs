using System;

/*
 * http://ww-ws-objmod.azurewebsites.net/
 */

namespace ww_ws_objmod
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var basicNoUIObj = new WinWrap.Basic.BasicNoUIObj())
            {
                basicNoUIObj.Secret = new Guid(Utils.GetPatternString("ww-ws-objmod", "Guid[(]\"(.*)\"[)]"));
                basicNoUIObj.Initialize();
                Button1.Text = basicNoUIObj.Evaluate("2+3");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button1.Text = DateTime.Now.ToString();
        }
    }
}