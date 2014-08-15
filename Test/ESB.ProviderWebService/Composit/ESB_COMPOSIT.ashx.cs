using ESB.Core;
using ESB.Core.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESB.ProviderWebService.Composit
{
    /// <summary>
    /// ESB_COMPOSIT 的摘要说明
    /// </summary>
    public class ESB_COMPOSIT : AshxAdapter
    {
        ESBProxy esbProxy = ESBProxy.GetInstance();

        protected override string DoEsbAction(string esbAction, string request)
        {
            String message1 = esbProxy.Invoke("ESB_WCF", "WCF_ACTION", "HelloWCF");

            String message2 = esbProxy.Invoke("ESB_ASHX", "ASHX_ACTION", "HelloWCF");


            return String.Format("收到参数：{0}={1}。", esbAction, request);
        }
    }
}