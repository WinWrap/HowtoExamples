using System;
using System.Net;
using System.Windows.Forms;

namespace WindowsFormsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.Text = "0";
            label2.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 10; i++)
            {
                WorkerThreadFunction();
            }
        }

        public void WorkerThreadFunction()
        {
            WebClient client = new WebClient();
            string url = "http://ww-ws-test.azurewebsites.net?x=" + label1.Text;
            string downloadString = client.DownloadString(url);
            if (downloadString.Contains("DateTime.Now"))
            {
                label1.Text = (Convert.ToInt32(label1.Text) + 1).ToString();
            }
            else
            {
                label2.Text = (Convert.ToInt32(label2.Text) + 1).ToString();
            }
            this.Refresh();
        }
    }
}
