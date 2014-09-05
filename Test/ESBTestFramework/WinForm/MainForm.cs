using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ESB.TestFramework.WinForm
{
    public partial class MainForm : Form
    {
        Boolean isStop = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            HttpWebRequest.DefaultWebProxy = null;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            //CallService();

            while (!isStop)
            {
                CallService();
            }

            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            isStop = true;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }


        private void CallService()
        {
            String uri = String.Format("{0}?ServiceName={1}&Version={2}&MethodName={3}&Message={4}",
                txtCallCenterUrl.Text, txtServiceName.Text, txtVersion.Text, txtMethodName.Text, txtMessage.Text);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Method = "GET";
            webRequest.ContentType = "text/xml; charset=utf-8";


            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            using (Stream newstream = webResponse.GetResponseStream())
            {
                using (StreamReader srRead = new StreamReader(newstream, System.Text.Encoding.UTF8))
                {
                    String outString = srRead.ReadToEnd();

                    //txtMessage.Text = outString;
                }
            }
        }
    }
}
