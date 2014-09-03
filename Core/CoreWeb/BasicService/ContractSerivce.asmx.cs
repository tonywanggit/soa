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
        [WebMethod(Description = "获取到服务版本视图")]
        public List<EsbView_ServiceVersion> GetServiceVersionViewByConfirmPerson(String personalID, Int32 status)
        {
            List<EsbView_ServiceVersion> lstVersionView = EsbView_ServiceVersion.FindAllByPersonalIDAndStatus(personalID, status);

            return lstVersionView.OrderByDescending(x => x.CommitDateTime).ToList();
        }

        [WebMethod(Description = "根据版本ID获取到服务版本视图")]
        public EsbView_ServiceVersion GetServiceVersionViewByID(String versionID)
        {
            return EsbView_ServiceVersion.FindAllByVersionID(versionID);
        }

        [WebMethod(Description = "获取到服务的特定版本")]
        public BusinessServiceVersion GetServiceVersionByID(String versionID)
        {
            return BusinessServiceVersion.FindByOID(versionID);
        }

        [WebMethod(Description = "获取到服务下的所有版本")]
        public List<BusinessServiceVersion> GetServiceVersionByServiceID(String serviceID)
        {
            List<BusinessServiceVersion> lstVersion = BusinessServiceVersion.FindAllByServiceID(serviceID);
            return lstVersion.OrderByDescending(x => x.CreateDateTime).ToList();
        }

        [WebMethod(Description = "获取到服务下的所有大版本")]
        public List<BusinessServiceVersion> GetServiceBigVersionByServiceID(String serviceID)
        {
            List<BusinessServiceVersion> lstVersion = BusinessServiceVersion.FindAllByServiceID(serviceID);
            List<BusinessServiceVersion> lstBigVerion = new List<BusinessServiceVersion>();
            foreach (Int32 bigVer in lstVersion.Select(x => x.BigVer).Distinct())
            {
                BusinessServiceVersion sv = lstVersion.Where(x => x.BigVer == bigVer).OrderByDescending(x => x.CreateDateTime).First();
                if (sv != null)
                    lstBigVerion.Add(sv);
            }

            return lstBigVerion.OrderBy(x=>x.BigVer).ToList();
        }

        [WebMethod(Description = "修改服务版本的状态")]
        public void UpdateServiceVersionStatus(String versionID, Int32 status, String opinion)
        {
            BusinessServiceVersion.UpdateServiceVersionStatus(versionID, status, opinion);
        }

        [WebMethod(Description = "修订版本")]
        public void ReviseServiceVersion(String versionID, String personalID)
        {
            BusinessServiceVersion.ReviseServiceVersion(versionID, personalID);
        }

        [WebMethod(Description = "升级版本")]
        public void UpgradeServiceVersion(String versionID, String personalID)
        {
            BusinessServiceVersion.UpgradeServiceVersion(versionID, personalID);
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

        [WebMethod(Description = "废弃服务版本")]
        public void ObsoleteServiceVersion(String versionID, String personalID)
        {
            BusinessServiceVersion.ObsoleteServiceVersion(versionID, personalID);
        }

        [WebMethod(Description = "删除服务版本和服务契约")]
        public void DeleteServiceVersionAndContract(String versionID)
        {
            BusinessServiceVersion.DeleteServiceVersionAndContract(versionID);
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
        public List<ServiceContract> SelectServiceContract(String versionID, Int32 status)
        {
            return ServiceContract.FindByVersion(versionID, status).OrderBy(x => x.CreateDateTime).ToList();
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
