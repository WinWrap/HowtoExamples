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
            if (Request["ResetTrace"] == "yes")
                MessengerService.DeleteLog();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MessengerService wwbs = new MessengerService();
            Button1.Text = DateTime.UtcNow.ToString() + " -- " + wwbs.SendMessage("Direct Call");
        }
    }
}