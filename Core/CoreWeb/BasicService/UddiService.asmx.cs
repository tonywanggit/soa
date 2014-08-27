using ESB.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using XCode;

namespace ESB.CallCenter.BasicService
{
    /// <summary>
    /// UddiService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://esb.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class UddiService : System.Web.Services.WebService
    {
        #region 服务提供者
        [WebMethod(Description = "获得所有服务提供者")]
        public List<BusinessEntity> GetAllBusinessEntity()
        {
            return BusinessEntity.FindAll();
        }

        [WebMethod(Description = "删除服务提供者")]
        public void DeleteBusinessEntity(BusinessEntity be)
        {
            be.Delete();
        }

        [WebMethod(Description = "修改服务提供者")]
        public void UpdateBusinessEntity(BusinessEntity be)
        {
            be.Update();
        }

        [WebMethod(Description = "插入服务提供者")]
        public void InsertBusinessEntity(BusinessEntity be)
        {
            be.Insert();
        }
        #endregion

        #region 服务
        [WebMethod(Description="获取到所有的服务列表")]
        public List<BusinessService> GetServiceList()
        {
            return BusinessService.FindAll();
        }

        [WebMethod(Description = "根据ServiceID查找服务")]
        public BusinessService GetServiceByID(String serviceID)
        {
            return BusinessService.FindByServiceID(serviceID);
        }

        [WebMethod(Description = "获取到业务实体下所有的服务列表")]
        public List<BusinessService> GetBusinessServiceByBussinessID(String businessID)
        {
            return BusinessService.FindAllByBusinessID(businessID);
        }

        [WebMethod(Description = "根据绑定地址找到服务")]
        public BusinessService GetBusinessServiceByTemplateID(String templateID)
        {
            BindingTemplate binding = BindingTemplate.FindByTemplateID(templateID);
            if (binding == null)
                return null;
            else
                return BusinessService.FindByServiceID(binding.ServiceID);
        }

        [WebMethod(Description = "新增服务")]
        public void InsertBusinessService(BusinessService service)
        {
            if (String.IsNullOrEmpty(service.ServiceID))
                service.ServiceID = Guid.NewGuid().ToString();

            service.Insert();
        }

        [WebMethod(Description = "修改服务")]
        public void UpdateBusinessService(BusinessService service)
        {
            service.Update();
        }

        [WebMethod(Description = "删除服务")]
        public void DeleteBusinessService(BusinessService service)
        {
            service.Delete();
        }

        #endregion

        #region 地址绑定
        [WebMethod(Description = "根据ServiceID查找绑定地址")]
        public List<BindingTemplate> GetBindingByServiceID(String serviceID)
        {
            return BindingTemplate.FindAllByServiceID(serviceID);
        }

        [WebMethod(Description = "新增绑定地址")]
        public void InsertBindingTemplate(BindingTemplate entity)
        {
            if (String.IsNullOrEmpty(entity.TemplateID))
                entity.TemplateID = Guid.NewGuid().ToString();

            entity.Insert();
        }

        [WebMethod(Description = "修改绑定地址")]
        public void UpdateBindingTemplate(BindingTemplate entity)
        {
            entity.Update();
        }

        [WebMethod(Description = "删除绑定地址")]
        public void DeleteBindingTemplate(BindingTemplate entity)
        {
            entity.Delete();
        }

        #endregion

        #region 用户
        [WebMethod(Description = "根据用户账户获取到用户信息")]
        public Personal GetPersonByLoginName(String loginName)
        {
            return Personal.FindAllByAccount(loginName);
        }

        [WebMethod(Description = "获取到所有用户信息")]
        public List<Personal> GetAllPerson()
        {
            return Personal.FindAll();
        }

        [WebMethod(Description = "新增用户")]
        public void InsertPerson(Personal entity)
        {
            if (String.IsNullOrEmpty(entity.PersonalID))
                entity.PersonalID = Guid.NewGuid().ToString();

            entity.Insert();
        }

        [WebMethod(Description = "修改用户")]
        public void UpdatePerson(Personal entity)
        {
            entity.Update();
        }

        [WebMethod(Description = "删除用户")]
        public void DeletePerson(Personal entity)
        {
            entity.Delete();
        }
        #endregion

    }
}
