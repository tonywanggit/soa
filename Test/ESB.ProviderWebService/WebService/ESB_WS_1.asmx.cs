using ESB.Core.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ESB.ProviderWebService.WebService
{
    /// <summary>
    /// ESB_WS_1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://mb.com")]
    public class ESB_WS_1 : WebServiceAdapter
    {
        protected override string DoEsbAction(string esbAction, string request)
        {
            return String.Format("收到参数：{0}={1}。", esbAction, request);
        }
    }
}
