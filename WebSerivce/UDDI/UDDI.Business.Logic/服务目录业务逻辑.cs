using System;
using System.Collections.Generic;
using System.Text;
using JN.ESB.Core.Service.Common;
using JN.ESB.UDDI.Core.DataAccess;

namespace JN.ESB.UDDI.Business.Logic
{
    public class 服务目录业务逻辑
    {

        /// <summary>
        /// //Zhen
        /// </summary>
        public 服务目录业务逻辑()
        {
            
        }

        public void 添加服务提供者(业务实体 服务提供者)
        {
            try
            {
                Guid bId = ServiceDirectoryDBAccess.AddNewBuinessEnitiy(服务提供者);
                //var service = 服务提供者.服务;
                //List<服务> list = service;
                //foreach (服务 service in list)
                //{
                //    service.业务编码 = bId;
                //    个人 person = new 个人();
                //    person.个人编码 = service.个人编码.Value;
                //    this.添加提供具体服务(person,service);
                //}
                //throw new System.NotImplementedException();
            }
            catch { }
        }

        public void 添加服务管理员(个人 服务管理员)
        {
            ServiceDirectoryDBAccess.AddNewPersonal(服务管理员);
        }
        public void 添加提供具体服务(个人 服务管理员,服务 具体服务)
        {

            Guid pId = Guid.Empty;
            String name = 服务管理员.姓名;
            if ((服务管理员.个人编码 == null) || (服务管理员.个人编码 == Guid.Empty))
            {
                //if (this.查找服务管理员_关键字(name).Count == 0)
                //{
                //    pId = ServiceDirectoryDBAccess.AddNewPersonal(服务管理员);

                //}
                //else
                //{
                //    pId = this.查找服务管理员_关键字(name)[0].个人编码;
                    
                //}
                pId = ServiceDirectoryDBAccess.AddNewPersonal(服务管理员);
            }
            else
            {
                pId = 服务管理员.个人编码;
            }
            具体服务.个人编码 = pId;
            //List<服务地址> templates = (务地址)具体服务.服务地址;
            //Guid 管理员编码 = 具体服务.个人编码.Value;
            Guid sId = ServiceDirectoryDBAccess.AddBusinessService(具体服务);
            
            //foreach(服务地址 template in templates)
            //{
            //    template.服务编码 = sId;
            //    this.添加服务绑定(template);
            //}
            //throw new System.NotImplementedException();
        }

        ////
        public List<服务约束> 获得服务约束＿服务地址(服务地址 绑定服务)
        {
            //List<服务约束> 服务约束 = new List<服务约束>();

            return ServiceDirectoryDBAccess.GetModelByBID(绑定服务.服务地址编码);
            //return 服务约束;
        }

        public 服务约束 获得服务约束＿约束编码(Guid 约束编码)
        {
            return ServiceDirectoryDBAccess.GetTModelByMID(约束编码);
        }
        public void 添加服务绑定(服务地址 服务地址)
        {
            Guid tId = ServiceDirectoryDBAccess.AddBindingTemplate(服务地址);
            //List<服务约束> tModels = (服务约束)服务地址.服务约束;
            //foreach (服务约束 tModel in tModels)
            //{
            //    tModel.服务地址编码 = tId;
            //    ServiceDirectoryDBAccess.AddTModel(tModel);
            //}

            //throw new System.NotImplementedException();
        }

        public void 添加服务约束(服务约束 服务约束)
        {
            ServiceDirectoryDBAccess.AddTModel(服务约束);
            //throw new System.NotImplementedException();
        }

        
        public List<个人> 获得所有服务管理员()
        {
            List<个人> 查询结果 = new List<个人>();
            var personals = ServiceDirectoryDBAccess.GetAllPersonal();
            foreach (var personal in personals)
            {
                查询结果.Add(personal);
            }
            return 查询结果;
        }


        public List<业务实体> 获得所有服务提供者()
        {
            List<业务实体> 查询结果 = new List<业务实体>();
            var 服务提供者集 = ServiceDirectoryDBAccess.GetAllBuinessEntity();
            foreach (var 服务提供者 in 服务提供者集)
            {
                查询结果.Add(服务提供者);
            }
            return 查询结果;
        }

        /// <summary>
        /// //Zhen
        /// </summary>
        public List<个人> 查找服务管理员_关键字(string 关键字)
        {
            
            List<个人> 查询结果 = new List<个人>();
            var personals = ServiceDirectoryDBAccess.GetAllPersonal();
            foreach (var personal in personals)
            {
                if (personal.姓名.Contains(关键字))
                {
                    查询结果.Add(personal);
                    continue;
                }
                else if (personal.邮件地址.Contains(关键字))
                {
                    查询结果.Add(personal);
                    continue;
                }
                else if (personal.电话.Contains(关键字))
                {
                    查询结果.Add(personal);
                    continue;
                }
                else
                {
                    continue;
                }
            }
            return 查询结果;
        }

        public List<业务实体> 查找服务提供者_关键字(String 关键字) 
        {
            List<业务实体> 业务实体集 = new List<业务实体>();
            var 查询结果 = ServiceDirectoryDBAccess.GetAllBuinessEntity();
            foreach (var 结果 in 查询结果)
            {
                if (结果.业务名称.Contains(关键字))
                {
                    业务实体集.Add(结果);
                    continue;
                }
            }
            return 业务实体集;
        }


        public void 修改服务提供者(业务实体 服务提供者)
        {
            ServiceDirectoryDBAccess.UpdateBuinessEntity(服务提供者);
            //List<服务> services = (服务)服务提供者.服务;
            //foreach(服务 service in services)
            //{
            //    this.修改具体服务(service);
            //}
            //throw new System.NotImplementedException();
        }

        public void 删除服务提供者(业务实体 服务提供者)
        {
            Guid bId = 服务提供者.业务编码;
            List<服务> services = ServiceDirectoryDBAccess.GetServiceByBID(bId);
            foreach (服务 service in services)
            {
                this.删除具体服务(service);
            }
            ServiceDirectoryDBAccess.RemoveBuinessEntity(服务提供者);
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// //Zhen
        /// Tony 2011-03-22 如果服务提供者.业务编码为空则返回所有服务
        /// </summary>
        public List<服务> 获得具体服务_服务提供者(业务实体 服务提供者)
        {
            List<服务> 查询结果 = new List<服务>();
            var service = ServiceDirectoryDBAccess.GetAllBusinessService();

            if (服务提供者.业务编码 == Guid.Empty)
            {
                foreach (var ser in service)
                {
                    查询结果.Add(ser);
                }
            }
            else
            {
                foreach (var ser in service)
                {
                    if (ser.业务编码 == 服务提供者.业务编码)
                    {
                        查询结果.Add(ser);
                        continue;
                    }
                }
            }

            return 查询结果;
        }

        /// <summary>
        /// //Zhen
        /// </summary>
        public List<服务> 查找具体服务_关键字(string 关键字)
        {
            List<服务> 服务集 = new List<服务>();
            var 查询结果 = ServiceDirectoryDBAccess.GetAllBusinessService();
            foreach (var 结果 in 查询结果)
            {
                if (结果.服务名称.Contains(关键字))
                {
                    服务集.Add(结果);
                    continue;
                }
            }
            return 服务集;
        }

        public void 修改具体服务(服务 具体服务)
        {
            ServiceDirectoryDBAccess.UpdateBusinessService(具体服务);
            //List<服务地址> templates = (服务地址)具体服务.服务地址;
            //foreach (服务地址 template in templates)
            //{
            //    this.修改绑定方法(template);
            //}
            ////throw new System.NotImplementedException();
        }

        public void 修改服务约束(服务约束 具体服务约束)
        {
            ServiceDirectoryDBAccess.UpdateTModel(具体服务约束);
        }

        public void 删除具体服务(服务 具体服务)
        {
            
            //List<服务地址> templates = (服务地址)具体服务.服务地址;
            Guid sId = 具体服务.服务编码;
            List<服务地址> templates = ServiceDirectoryDBAccess.GetTemplatBySID(sId);
            foreach (服务地址 template in templates)
            {
                this.删除绑定方法(template);
            }
            ServiceDirectoryDBAccess.RemoveBusinessService(具体服务);
            //throw new System.NotImplementedException();
        }

        public void 删除服务约束(服务约束 具体服务约束) 
        {
            ServiceDirectoryDBAccess.RemoveTModel(具体服务约束);
        }

        /// <summary>
        /// //Zhen
        /// </summary>
        public List<服务地址> 获得绑定信息_具体服务(服务 具体服务单元)
        {
            List<服务地址> 服务地址查询结果 =  new List<服务地址>(); 
            var 查询结果 = ServiceDirectoryDBAccess.GetAllBindingTemplate();
            foreach (var 查询 in 查询结果)
            {
                if(查询.服务编码.Value == 具体服务单元.服务编码)
                {
                    服务地址查询结果.Add(查询);
                }
            }
            return 服务地址查询结果;
        }

        public 服务地址 获得绑定信息_服务名称(string 服务名称)
        {

            服务 具体服务 = ServiceDirectoryDBAccess.GetServiceByName(服务名称);
            if (具体服务 != null)
            {
                List<服务地址> 服务地址查询结果 = ServiceDirectoryDBAccess.GetTemplatBySID(具体服务.服务编码);

                if (服务地址查询结果.Count > 0)
                    return 服务地址查询结果[0];
                else
                    return null;
            }
            else
                return null;
        }

        public 服务地址 获得绑定信息_服务地址编码(Guid 服务地址编码)
        {
            return ServiceDirectoryDBAccess.GetTemplateByTID(服务地址编码);
        }

        public List<服务视图> 获得所有服务视图()
        {
            return ServiceDirectoryDBAccess.GetAllUddiView();
        }

        public List<业务实体> 获得服务提供者_具体服务(服务 具体服务)
        {
            List<业务实体> 服务提供者查询结果 = new List<业务实体>();
            var 查询结果 = ServiceDirectoryDBAccess.GetAllBuinessEntity();
            foreach (var 查询 in 查询结果)
            {
                if (查询.业务编码 == 具体服务.业务编码)
                {
                    服务提供者查询结果.Add(查询);
                }
            }
            return 服务提供者查询结果;
        }

        public 业务实体 获得服务提供者_服务提供者编码(Guid 服务提供者编码)
        {
            //List<业务实体> 服务提供者查询结果 = new List<业务实体>();
            var 查询结果 = ServiceDirectoryDBAccess.GetBuinessEntityByBID(服务提供者编码);
            return 查询结果;
        }

        public 服务 获得具体服务_服务编码(Guid 服务编码)
        {
            //服务 服务查询结果 = new 服务();
            var 查询结果 = ServiceDirectoryDBAccess.GetServiceBySID(服务编码);
            return 查询结果;
        }

        public 服务 获得具体服务_绑定信息(服务地址 服务绑定信息)
        {
            
            服务地址 地址 = ServiceDirectoryDBAccess.GetTemplateByTID(服务绑定信息.服务地址编码);

            服务 服务查询结果 = ServiceDirectoryDBAccess.GetServiceBySID(地址.服务编码.Value);
            return 服务查询结果;
        }

        

        /// <summary>
        /// //Zhen
        /// </summary>
        public List<服务地址> 查找绑定地址_关键字(string 关键字)
        {
            List<服务地址> 查询结果 = new List<服务地址>();
            var bindingAddress = ServiceDirectoryDBAccess.GetAllBindingTemplate();
            foreach (var address in bindingAddress)
            {
                if (address.描述.Contains(关键字))
                {
                    查询结果.Add(address);
                }
                else if (address.访问地址.Contains(关键字))
                {
                    查询结果.Add(address);
                }
                else if (address.方法名称.Contains(关键字))
                {
                    查询结果.Add(address);
                }
                else
                {
                    continue;
                }
            }
            return 查询结果;
        }

        public void 修改绑定方法(服务地址 绑定信息)
        {
            //List<服务约束> tModels = (服务约束)绑定信息.服务约束;
            //foreach (服务约束 tModel in tModels) 
            //{
            //    ServiceDirectoryDBAccess.UpdateTModel(tModel);
            //}
            ServiceDirectoryDBAccess.UpdateBindingTemplate(绑定信息);
            
        }

        /// <summary>
        /// </summary>
        public void 删除绑定方法(服务地址 绑定信息)
        {
            Guid tId = 绑定信息.服务地址编码;
            List<服务约束> tModels = ServiceDirectoryDBAccess.GetModelByBID(tId);
            //List<服务约束> tModels = ServiceDirectoryDBAccess.GetModelByBID(绑定信息.服务地址编码);
            foreach(服务约束 tModel in tModels)
            {
                ServiceDirectoryDBAccess.RemoveTModel(tModel);
            }
            ServiceDirectoryDBAccess.RemoveBindingTemplate(绑定信息);
        }

        /// <summary>
        /// //Zhen
        /// </summary>
        public 个人 获得管理员_具体绑定服务(服务地址 绑定服务)
        {
           
            var 服务 = ServiceDirectoryDBAccess.GetAllBusinessService().Find(p => p.服务编码 == 绑定服务.服务编码);
            return ServiceDirectoryDBAccess.GetPersonalByPID(服务.个人编码.Value);
        }

        public 个人 获得管理员_管理员编码(Guid 管理员编码)
        {
            return ServiceDirectoryDBAccess.GetPersonalByPID(管理员编码);
        }

        public 个人 获得管理员_管理员帐号(string 管理员帐号)
        {
            return ServiceDirectoryDBAccess.GetPersonalByAccount(管理员帐号);
        }

        public List<个人> 获得系统管理员()
        {
            return ServiceDirectoryDBAccess.GetAllPersonal().FindAll(p => p.权限 == 0);
        }

        public void 修改服务管理员(个人 管理员)
        {
            ServiceDirectoryDBAccess.UpdatePersonal(管理员);
            //throw new System.NotImplementedException();
        }

        public void 删除服务管理员(个人 管理员)
        {
            ServiceDirectoryDBAccess.RemovePersonal(管理员);
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// //Zhen
        /// </summary>
        public List<服务地址> 查找绑定服务地址_服务状态(服务状态 状态)
        {
            List<服务地址> 查找结果 = new List<服务地址>();
            var services = ServiceDirectoryDBAccess.GetAllBindingTemplate();
            foreach (var ser in services)
            {
                if (ser.状态.Value == (int)状态)
                {
                    查找结果.Add(ser);
                }
            }
            return 查找结果;

        }

        public List<服务地址> 查找绑定服务地址_服务类型(服务类型 类型)
        {
            List<服务地址> 查找结果 = new List<服务地址>();
            var services = ServiceDirectoryDBAccess.GetAllBindingTemplate();
            foreach (var ser in services)
            {
                if (ser.绑定类型.Value == (int)类型)
                {
                    查找结果.Add(ser);
                }
            }
            return 查找结果;

        }

        public List<服务> 获得具体服务_管理员(个人 管理员) 
        {
            List<服务> services = new List<服务>();
            foreach (业务实体 提供者 in 获得所有服务提供者()) {
                List<服务> ser = 获得具体服务_服务提供者(提供者);
                foreach (服务 具体服务 in ser){
                    if (具体服务.个人编码 == 管理员.个人编码)
                        services.Add(具体服务);
                }
            }
            return services;
        }

    }
}
