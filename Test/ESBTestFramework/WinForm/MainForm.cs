using ESB.TestFramework.Test;
using NewLife.Threading;
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
        delegate void UpdateLabel(Int32 value);

        TestManager m_TestManager = new TestManager();
        TimerX m_TimerX; 

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

            m_TestManager.Start(new ESBInvokeParam()
            {
                CallCenterUrl = this.txtCallCenterUrl.Text,
                ServiceName = this.txtServiceName.Text,
                MethodName = this.txtMethodName.Text,
                Message = this.txtMessage.Text,
                Version = 1
            });

            m_TimerX = new TimerX(x =>
            {
                this.lblInvokeNum.Invoke(new UpdateLabel(UpdateInvokeNumLabel), m_TestManager.InvokeNum);
            }, null, 1000, 1000);

        }

        private void UpdateInvokeNumLabel(int invokeNum)
        {
            this.lblInvokeNum.Text = invokeNum.ToString();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            m_TestManager.Stop();
            m_TimerX.Dispose();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void tbThreadNum_Scroll(object sender, EventArgs e)
        {
            this.lblThreadNum.Text = tbThreadNum.Value.ToString();
            m_TestManager.SetThreadNum(tbThreadNum.Value);
        }
    }
}
