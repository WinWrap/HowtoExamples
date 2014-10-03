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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Button1.Text = DateTime.Now.ToString();
        }
    }
}