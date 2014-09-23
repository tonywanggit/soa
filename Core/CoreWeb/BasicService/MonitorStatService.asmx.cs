using ESB.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ESB.CallCenter.BasicService
{
    /// <summary>
    /// MonitorService 监控统计服务
    /// </summary>
    [WebService(Namespace = "http://esb.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class MonitorStatService : System.Web.Services.WebService
    {

        [WebMethod(Description = "获取通讯日志的统计信息")]
        public List<ServiceMonitor> GetMonitorServiceStatic()
        {
            return ServiceMonitor.GetMonitorServiceStatic();
        }
    }
}
