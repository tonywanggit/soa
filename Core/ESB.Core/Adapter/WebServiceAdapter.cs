using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;

namespace ESB.Core.Adapter
{
    /// <summary>
    /// WebService适配器
    /// </summary>
    [WebService(Namespace="http://mb.com")]
    public abstract class WebServiceAdapter : WebService
    {
        [WebMethod(Description="ESB路由接口")]
        public string EsbAction(String action, String request)
        {
            return DoEsbAction(action, request);
        }

        protected abstract String DoEsbAction(String esbAction, String request);
    }
}
