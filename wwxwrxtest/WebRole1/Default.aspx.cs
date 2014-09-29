using System;

/*
 * Publish cloud service (wwxwrxtest):
 *   1a) fully deleted service, webrole, deployment, and storage
 *   1b) new publish profile (manage and remove existing profile)
 *   1c) use "North Central US", Locally Redundant
 *   or
 *   2a) running service, et.
 *   2b) existing publish profile
 */

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
                basicNoUIObj.Secret = new Guid(Utils.GetPatternString("wwxwrxtest", "Guid[(]\"(.*)\"[)]"));
                basicNoUIObj.Initialize();
                //Button1.Text = basicNoUIObj.Evaluate("4+3");
                Button1.Text = basicNoUIObj.Evaluate("System.DateTime.Now.ToString()");
            }
        }
    }
}