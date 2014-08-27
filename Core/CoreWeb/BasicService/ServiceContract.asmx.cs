using ESB.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ESB.CallCenter.BasicService
{
    /// <summary>
    /// 服务契约、版本等高级特性
    /// </summary>
    [WebService(Namespace = "http://esb.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceContract : System.Web.Services.WebService
    {
        [WebMethod(Description = "获取到服务下的所有版本")]
        public List<BusinessServiceVersion> GetServiceVersionByServiceID(String serviceID)
        {
            return BusinessServiceVersion.FindAllByServiceID(serviceID);
        }

        [WebMethod(Description = "新增服务版本")]
        public void InsertServiceVersion(BusinessServiceVersion entity)
        {
            if (String.IsNullOrEmpty(entity.OID))
                entity.OID = Guid.NewGuid().ToString();

            entity.Insert();
        }

        [WebMethod(Description = "修改服务版本")]
        public void UpdateServiceVersion(BusinessServiceVersion entity)
        {
            entity.Update();
        }

        [WebMethod(Description = "删除服务版本")]
        public void DeleteServiceVersion(BusinessServiceVersion entity)
        {
            entity.Delete();
        }
    }
}
