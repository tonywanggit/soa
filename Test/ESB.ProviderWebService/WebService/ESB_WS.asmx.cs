using ESB.Core.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ESB.ProviderWebService.WebService
{
    /// <summary>
    /// ESB_WS 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://mb.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ESB_WS : WebServiceAdapter
    {
        protected override string DoEsbAction(string esbAction, string request)
        {
            return String.Format("收到参数：{0}={1}。", esbAction, request);
        }
    }
}
