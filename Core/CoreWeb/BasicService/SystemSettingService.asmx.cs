using ESB.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ESB.CallCenter.BasicService
{
    /// <summary>
    /// 系统设置服务
    /// </summary>
    [WebService(Namespace = "http://esb.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class SystemSettingService : System.Web.Services.WebService
    {
        #region 地址设置
        [WebMethod(Description = "获取到所有的地址设置")]
        public List<SettingUri> GetAllSettingUri()
        {
            return SettingUri.FindAll();
        }

        [WebMethod(Description = "新增地址设置")]
        public void InsertSettingUri(SettingUri entity)
        {
            if (String.IsNullOrEmpty(entity.OID))
                entity.OID = Guid.NewGuid().ToString();

            entity.Insert();
        }

        [WebMethod(Description = "修改地址设置")]
        public void UpdateSettingUri(SettingUri entity)
        {
            entity.Update();
        }

        [WebMethod(Description = "删除地址设置")]
        public void DeleteSettingUri(SettingUri entity)
        {
            entity.Delete();
        }
        #endregion
    }
}
