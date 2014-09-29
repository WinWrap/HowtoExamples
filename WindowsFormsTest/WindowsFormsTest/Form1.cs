using System;
using System.Net;
using System.Windows.Forms;

using System.Threading;

namespace WindowsFormsTest
{
    public partial class Form1 : Form
    {
        delegate void UpdateLabelDelegate(bool success);

        public Form1()
        {
            InitializeComponent();
            label1.Text = "0";
            label2.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread[] threads = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(WorkerThreadFunction);
            }

            for (int i = 0; i < 10; i++)
            {
                threads[i].Start();
            }
        }

        private void UpdateLabel(bool success)
        {
            if (success)
            {
                label1.Text = (Convert.ToInt32(label1.Text) + 1).ToString();
            }
            else
            {
                label2.Text = (Convert.ToInt32(label2.Text) + 1).ToString();
            }
            this.Refresh();
        }

        public void WorkerThreadFunction()
        {
            WebClient client = new WebClient();
            string url = "http://ww-ws-test.azurewebsites.net?x=" + label1.Text;
            string downloadString = client.DownloadString(url);
            bool success = downloadString.Contains("DateTime.Now");
            BeginInvoke(new UpdateLabelDelegate(UpdateLabel), success);
        }
    }
}
