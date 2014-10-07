using ESB.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data;
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

        [WebMethod(Description = "根据服务名和方法名查找服务当天监控数据")]
        public List<ServiceMonitor> GetAllByServiceAndMethodToday(String serviceName, String methodName)
        {
            return ServiceMonitor.FindAllByServiceAndMethodToday(serviceName, methodName);
        }

        [WebMethod(Description="获取到看板的统计数据")]
        public DataSet GetDashboardOverview(String businessID)
        {
            return AuditBusiness.GetDashboardOverview(businessID);
        }
    }
}
