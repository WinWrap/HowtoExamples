using System;

namespace WebRole1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Button1.Text = DateTime.Now.ToString();
            using (var basicNoUIObj = new WinWrap.Basic.BasicNoUIObj())
            {
                basicNoUIObj.Initialize();
                Button1.Text = basicNoUIObj.Evaluate("4+3");
            }
        }
    }
}