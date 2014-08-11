using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using JN.ESB.UDDI.Core.DataAccess;
using JN.ESB.UDDI.Business.Logic;
using JN.ESB.Core.Service.Common;

namespace JN.ESB.UDDI.Service.Registration
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.jn.com/ESB/UDDI")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class 注册服务目录服务 : System.Web.Services.WebService
    {

        [WebMethod]
        public void 新增服务(个人 服务管理员,服务 具体服务)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
           
            服务逻辑.添加提供具体服务(服务管理员,具体服务);                       
        }

        [WebMethod]
        public void 新增服务提供者(业务实体 服务提供者)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            
            服务逻辑.添加服务提供者(服务提供者);
        }

        [WebMethod]
        public void 新增服务地址(服务地址 服务地址)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            服务逻辑.添加服务绑定(服务地址);
        }

        [WebMethod]
        public void 新增服务管理员(个人 服务管理员)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            服务逻辑.添加服务管理员(服务管理员);
        }

        [WebMethod]
        public void 新增服务约束(服务约束 服务约束)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            服务逻辑.添加服务约束(服务约束);
        }
        [WebMethod]
        public List<服务地址> 查找绑定地址_关键字(string 关键字)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.查找绑定地址_关键字(关键字);

        }

        [WebMethod]
        public List<服务地址> 查找绑定服务地址_服务状态(服务状态 状态)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.查找绑定服务地址_服务状态(状态);
        }

        [WebMethod]
        public List<服务地址> 查找绑定服务地址_服务类型(服务类型 类型)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.查找绑定服务地址_服务类型(类型);
        }

        [WebMethod]
        public List<个人> 查找服务管理员_关键字(string 关键字)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.查找服务管理员_关键字(关键字);
        }

        [WebMethod]
        public List<业务实体> 查找服务提供者_关键字(String 关键字)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.查找服务提供者_关键字(关键字);
        }

        [WebMethod]
        public List<服务> 查找具体服务_关键字(string 关键字)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.查找具体服务_关键字(关键字);
        }

        [WebMethod]
        public 服务地址 获得绑定信息_服务地址编码(Guid 服务地址编码)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得绑定信息_服务地址编码(服务地址编码);
        }

        [WebMethod]
        public List<服务地址> 获得绑定信息_具体服务(服务 具体服务单元)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得绑定信息_具体服务(具体服务单元);
        }

        [WebMethod]
        public 服务地址 获得绑定信息_服务名称(string 服务名称)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得绑定信息_服务名称(服务名称);
        }

        [WebMethod]
        public List<业务实体> 获得服务提供者_具体服务(服务 具体服务)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得服务提供者_具体服务(具体服务);
        }

        [WebMethod]
        public 个人 获得管理员_具体绑定服务(服务地址 绑定服务)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得管理员_具体绑定服务(绑定服务);
        }

        [WebMethod]
        public 个人 获得管理员_管理员编码(Guid 管理员编码)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得管理员_管理员编码(管理员编码);
        }

        [WebMethod]
        public 个人 获得管理员_管理员姓名(string 管理员姓名)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得管理员_管理员帐号(管理员姓名);
        }

        [WebMethod]
        public List<个人> 获得系统管理员()
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得系统管理员() ;
        }

        [WebMethod]
        public 服务 获得具体服务_绑定信息(服务地址 服务绑定信息)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得具体服务_绑定信息(服务绑定信息);
        }

        [WebMethod]
        public List<服务> 获得具体服务_管理员(个人 服务管理员)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得具体服务_管理员(服务管理员);
        }

        [WebMethod]
        public List<服务> 获得具体服务_服务提供者(业务实体 服务提供者)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得具体服务_服务提供者(服务提供者);
        }

        [WebMethod]
        public List<服务约束> 获得服务约束＿服务地址(服务地址 绑定服务)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得服务约束＿服务地址(绑定服务);
        }

        [WebMethod]
        public List<个人> 获得所有服务管理员()
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得所有服务管理员();
        }

        [WebMethod]
        public List<业务实体> 获得所有服务提供者()
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得所有服务提供者();
        }

        [WebMethod]
        public 业务实体 获得服务提供者(Guid 服务提供者编码)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();

            return 服务逻辑.获得服务提供者_服务提供者编码(服务提供者编码);
        }

        [WebMethod]
        public 服务 获得服务(Guid 服务编码)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();

            return 服务逻辑.获得具体服务_服务编码(服务编码);
        }

        [WebMethod]
        public 服务约束 获得服务约束(Guid 约束编码)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();

            return 服务逻辑.获得服务约束＿约束编码(约束编码);
        }

        [WebMethod]
        public List<服务视图> 获得所有服务视图()
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            return 服务逻辑.获得所有服务视图();
        }

        [WebMethod]
        public void 删除绑定方法(服务地址 绑定信息)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            服务逻辑.删除绑定方法(绑定信息);
        }

        [WebMethod]
        public void 删除服务管理员(个人 管理员)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            服务逻辑.删除服务管理员(管理员);
        }

        [WebMethod]
        public void 删除服务提供者(业务实体 服务提供者)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            服务逻辑.删除服务提供者(服务提供者);
        }

        [WebMethod]
        public void 删除具体服务(服务 具体服务)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            服务逻辑.删除具体服务(具体服务);
        }

        [WebMethod]
        public void 删除服务约束(服务约束 具体服务约束)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            服务逻辑.删除服务约束(具体服务约束);
        }

        

        [WebMethod]
        public void 修改服务管理员(个人 管理员)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            服务逻辑.修改服务管理员(管理员);
        }


        [WebMethod]
        public void 修改服务提供者(业务实体 服务提供者)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            服务逻辑.修改服务提供者(服务提供者);
        }

        [WebMethod]
        public void 修改具体服务(服务 具体服务)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            服务逻辑.修改具体服务(具体服务);
        }
        [WebMethod]
        public void 修改绑定方法(服务地址 绑定信息)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            服务逻辑.修改绑定方法(绑定信息);
        }
        [WebMethod]
        public void 修改服务约束(服务约束 具体服务约束)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            服务逻辑.修改服务约束(具体服务约束);
        }

    }
}
