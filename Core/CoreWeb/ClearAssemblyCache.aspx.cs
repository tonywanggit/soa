using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Threading;
using System.Threading;
using ESB.Core.Entity;
using ESB.Core.Rpc;

namespace CoreWeb
{
    public partial class ClearAssemblyCache : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Action"] == "RemoveCache")
            {
                String result = "恭喜，缓存清除成功！";
                try
                {
                    RemoveAssemblyCache(Request["Url"]);
                    PreCompilerAssembly(Request["Url"], Request["ServiceName"]);
                }
                catch (Exception ex)
                {
                    //result = ex.Message;
                    result = "预编译程序集出现错误，请检查日志！";
                }

                Response.Write(String.Format("{0}({{result: '{1}'}})", Request["callback"], result.Replace(":", "：")));
                Response.End();
            }

            if(!Page.IsPostBack)
                ShowCache();
        }

        protected void ShowCache()
        {
            lblConsole.Text = "当前程序集缓存数：" + SoapClientCache.GetSoapClientNum() + "<br>";
            List<String> urls = SoapClientCache.GetSoapClientCacheList();

            foreach (var item in urls)
            {
                lblConsole.Text += item + "<br>";
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "tony")
            {
                RemoveAssemblyCache(txtUrl.Text);
                //SoapClientCache.ClearCache("http://10.30.100.18/MockWeb/Demo.asmx");
                ShowCache();
            }
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="url"></param>
        protected void RemoveAssemblyCache(String url)
        {
            SoapClientCache.ClearCache(url);
        }

        /// <summary>
        /// 预编译程序集
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        protected void PreCompilerAssembly(String url, String serviceName)
        {
            SoapClientCache.GetItem(url, serviceName);
        }

        protected void btnCompiler_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Tony")
            {
                ManualResetEvent mre = new ManualResetEvent(false);
                for (int i = 0; i < 10; i++)
                {
                    Thread t = new Thread(() =>
                    {
                        mre.WaitOne();
                        SoapClientItem item = SoapClientCache.GetItem("http://10.30.1.6/CKMService/WebService/ESBEmailService.asmx", "OA_Email");
                    });
                    t.Start();
                }
                mre.Set();
            }
        }

        protected void btnShowCache_Click(object sender, EventArgs e)
        {
            ShowCache();
        }

        protected void btnShowAssemblyList_Click(object sender, EventArgs e)
        {
            Dictionary<String, AssemblyType> dictAssembly = AssemblyType.GetAssemblyTypeList();
            lblAssemblyList.Text = "当前程序集端口缓存数：" + dictAssembly.Count + "<br>";

            foreach (var item in dictAssembly)
            {
                lblAssemblyList.Text += item.Value.PortType + "【" + item.Value.AssemblyPath + "】" + "<br>";
            }
        }
    }
}
