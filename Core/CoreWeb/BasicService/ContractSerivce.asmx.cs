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
    public class ContractSerivce : System.Web.Services.WebService
    {
        #region 服务版本
        [WebMethod(Description = "获取到服务的特定版本")]
        public BusinessServiceVersion GetServiceVersionByID(String versionID)
        {
            return BusinessServiceVersion.FindByOID(versionID);
        }

        [WebMethod(Description = "获取到服务下的所有版本")]
        public List<BusinessServiceVersion> GetServiceVersionByServiceID(String serviceID)
        {
            return BusinessServiceVersion.FindAllByServiceID(serviceID);
        }

        [WebMethod(Description = "修改服务版本的状态")]
        public void UpdateServiceVersionStatus(String versionID, Int32 status)
        {
            BusinessServiceVersion.UpdateServiceVersionStatus(versionID, status);
        }

        [WebMethod(Description = "修改服务版本的信息")]
        public void UpdateServiceVersionInfo(String versionID, String confirmPersonID, String desc)
        {
            BusinessServiceVersion.UpdateServiceVersionInfo(versionID, confirmPersonID, desc);
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
        #endregion


        #region 服务契约
        /// <summary>
        /// 获取到服务版本下的所有契约
        /// </summary>
        /// <param name="serviceID"></param>
        /// <param name="versionID"></param>
        /// <param name="status">0：当前，1：提交评审，2：评审通过</param>
        /// <returns></returns>
        [WebMethod(Description = "获取到服务版本下的所有契约")]
        public List<ServiceContract> SelectServiceContract(String serviceID, String versionID, Int32 status)
        {
            return ServiceContract.FindByVersion(serviceID, versionID, status);
        }

        [WebMethod(Description = "新增服务契约")]
        public void InsertServiceContract(ServiceContract entity)
        {
            if (String.IsNullOrEmpty(entity.OID))
                entity.OID = Guid.NewGuid().ToString();

            entity.Insert();
        }

        [WebMethod(Description = "修改服务契约")]
        public void UpdateServiceContract(ServiceContract entity)
        {
            entity.Update();
        }

        [WebMethod(Description = "删除服务契约")]
        public void DeleteServiceContract(ServiceContract entity)
        {
            entity.Delete();
        }
        #endregion
    }
}
