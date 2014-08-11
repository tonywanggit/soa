using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JN.ESB.UDDI.Core.DataAccess
{
    public class ServiceDirectoryDBAccess
    {
        private static ServiceDirectoryDCDataContext serviceDirectoryDC = new ServiceDirectoryDCDataContext();
        public ServiceDirectoryDBAccess()
        {
            //serviceDirectoryDC = new ServiceDirectoryDCDataContext();
        }

        #region 服务提供者
        public static List<服务视图> GetAllUddiView()
        {
            var views = from uddiview in serviceDirectoryDC.服务视图
                        where uddiview.业务系统 != null
                        select uddiview;
            return views.ToList();
        }
        public static List<业务实体> GetAllBuinessEntity()
        {
            var enities = from BusinessEntity in serviceDirectoryDC.业务实体
                          where BusinessEntity.业务编码 != null
                          select BusinessEntity;
            return enities.ToList();
        }

        public static Guid AddNewBuinessEnitiy(业务实体 Entity)
        {
            Guid newID = Guid.NewGuid();
            try
            {
                Entity.业务编码 = Guid.NewGuid();
                serviceDirectoryDC.业务实体.InsertOnSubmit(Entity);
                serviceDirectoryDC.SubmitChanges();
            }
            catch
            {
                newID = Guid.Empty;
            }
            return newID;
        }

        public static bool UpdateBuinessEntity(业务实体 Entity)
        {
            bool isSubmitOk = true;
            try
            {
                var Entities = serviceDirectoryDC.业务实体.Where(p => p.业务编码 == Entity.业务编码);
                foreach (var i in Entities)
                {
                    i.描述 = Entity.描述;
                    i.业务名称 = Entity.业务名称;
                }
                serviceDirectoryDC.SubmitChanges();
            }
            catch
            {
                isSubmitOk = false;
            }
            return isSubmitOk;
        }

        public static bool RemoveBuinessEntity(业务实体 Entity)
        {
            bool isSubmitOk = true;
            try
            {
                //serviceDirectoryDC.业务实体.Attach(Entity);

                serviceDirectoryDC.业务实体.DeleteOnSubmit(GetBuinessEntityByBID(Entity.业务编码));
                serviceDirectoryDC.SubmitChanges();
            }
            catch 
            {
                isSubmitOk = false;
            }
            return isSubmitOk;
        }

        public static 业务实体 GetBuinessEntityByBID(Guid BID)
        {
            var BizEntity = serviceDirectoryDC.业务实体.Single(p => p.业务编码 == BID);
            if (BizEntity != null)
            {
               return BizEntity;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 服务对象

        public static List<服务> GetAllBusinessService()
        {
            var entities = from service in serviceDirectoryDC.服务
                         where service.服务编码 !=null
                         orderby service.业务编码
                         select service;
            return entities.ToList();
        }
        public static 服务 GetServiceBySID(Guid SID)
        {
            var entity = from service in serviceDirectoryDC.服务
                         where service.服务编码 == SID
                         select service;
            if (entity.Count() > 0)
                return entity.ToArray()[0];
            else
                return null;
        }

        public static 服务 GetServiceByName(string Name)
        {
            var entity = from service in serviceDirectoryDC.服务
                         where service.服务名称 == Name
                         select service;
            if (entity.Count() > 0)
                return entity.ToArray()[0];
            else
                return null;
        }
        
        /// <summary>
        /// 根据服务提供者编码获得所有服务
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public static List<服务> GetServiceByBID(Guid BID)
        {
            var entities = from service in serviceDirectoryDC.服务
                           where service.业务编码 == BID
                           select service;
            return entities.ToList();
        }



        public static bool UpdateBusinessService(服务 entity)
        {
            bool isUpdateOk = true;
            try
            {
                var entities = serviceDirectoryDC.服务.Where(p => p.服务编码 == entity.服务编码);
                entities.ToArray()[0].服务名称 = entity.服务名称;
                entities.ToArray()[0].服务种类 = entity.服务种类;
                entities.ToArray()[0].个人编码 = entity.个人编码;
                entities.ToArray()[0].业务编码 = entity.业务编码;
                entities.ToArray()[0].描述 = entity.描述;
                serviceDirectoryDC.SubmitChanges();
            }
            catch 
            {
                isUpdateOk = false;
            }
            return isUpdateOk;
        }

        public static Guid AddBusinessService(服务 entity)
        {
            Guid SID = Guid.NewGuid();
            try
            {
                entity.服务编码 = SID;
                serviceDirectoryDC.服务.InsertOnSubmit(entity);
                serviceDirectoryDC.SubmitChanges();
            }
            catch 
            {
                SID = Guid.Empty;
            }
            return SID;
        }

        public static bool RemoveBusinessService(服务 entity)
        {
            bool isdeleteOk = true;
            try
            {
                serviceDirectoryDC.服务.DeleteOnSubmit(GetServiceBySID(entity.服务编码));
                serviceDirectoryDC.SubmitChanges();

            }
            catch 
            {
                isdeleteOk = false;
            }
            return isdeleteOk;
        }
        #endregion 

        #region 服务管理员

        public static Guid AddNewPersonal(个人 personal)
        {
            Guid newId = Guid.NewGuid();
            try
            {
                personal.个人编码 = newId;
                serviceDirectoryDC.个人.InsertOnSubmit(personal);
                serviceDirectoryDC.SubmitChanges();
            }
            catch
            {
                newId = Guid.Empty;
            }
            return newId;
        }

        public static List<个人> GetAllPersonal()
        {
            var pers = from per in serviceDirectoryDC.个人
                          where per.个人编码 != Guid.Empty
                          select per;
            return pers.ToList();
        }

        public static bool RemovePersonal(个人 personal)
        {
            bool issucceed = true;
            try
            {
                serviceDirectoryDC.个人.DeleteOnSubmit(GetPersonalByPID(personal.个人编码));
                serviceDirectoryDC.SubmitChanges();
            }
            catch
            {
                issucceed = false;
            }
            return issucceed;
        }

        public static bool UpdatePersonal(个人 personal)
        {
             bool issucceed = true;
             try
             {
                 var pers = serviceDirectoryDC.个人.Where(p => p.个人编码 == personal.个人编码);
                 foreach (var per in pers)
                 {
                     per.电话 = personal.电话;
                     per.姓名 = personal.姓名;
                     per.邮件地址 = personal.邮件地址;
                     per.权限 = personal.权限;
                     per.帐号 = personal.帐号;
                     //per.个人编码 = personal.个人编码;
                 }
                 serviceDirectoryDC.SubmitChanges();
             }
             catch
             {
                 issucceed = false;
             }
             return issucceed;
        }

        public static 个人 GetPersonalByPID(Guid PID)
        {
            var per = serviceDirectoryDC.个人.Single(p => p.个人编码 == PID);
            return per;
        }

        public static 个人 GetPersonalByAccount(string PAccount)
        {
            var per = serviceDirectoryDC.个人.Single(p => p.帐号 == PAccount);
            return per;
        }

        #endregion 

        #region 服务地址
        public static List<服务地址> GetAllBindingTemplate()
        {
            var entities = from bindingTemplate in serviceDirectoryDC.服务地址
                         where bindingTemplate.服务地址编码 !=null
                         select bindingTemplate;
            return entities.ToList();
        }

        public static 服务地址 GetTemplateByTID(Guid TID)
        {
            var entity = from bindingTemplate in serviceDirectoryDC.服务地址
                                   where bindingTemplate.服务地址编码 ==TID
                                   select bindingTemplate;
            if (entity.Count() > 0)
                return entity.ToArray()[0];
            else
                return null;
        }

        /// <summary>
        /// 根据服务编码获得地址
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public static List<服务地址> GetTemplatBySID(Guid SID)
        {
            var entities = from bindingTemplate in serviceDirectoryDC.服务地址
                                   where bindingTemplate.服务编码 ==SID
                                   select bindingTemplate;
            return entities.ToList();
        }



        public static Guid AddBindingTemplate(服务地址 entity)
        {
            Guid BID = Guid.NewGuid();
            try
            {
                entity.服务地址编码 = BID;
                serviceDirectoryDC.服务地址.InsertOnSubmit(entity);
                serviceDirectoryDC.SubmitChanges();
            }
            catch 
            {
                BID = Guid.Empty;
            }
            return BID;
        }

        public static bool RemoveBindingTemplate(服务地址 entity)
        {
            bool isdeleteOk = true;
            try
            {
                serviceDirectoryDC.服务地址.DeleteOnSubmit(GetTemplateByTID(entity.服务地址编码));
                serviceDirectoryDC.SubmitChanges();

            }catch
            {
                isdeleteOk = false;
            }
            return isdeleteOk;
        }

        public static bool UpdateBindingTemplate(服务地址 entity)
        {
            bool isUpdateOk = true;
            try
            {
                var entities = serviceDirectoryDC.服务地址.Where(p => p.服务地址编码 == entity.服务地址编码);
                entities.ToArray()[0].服务编码 = entity.服务编码;
                entities.ToArray()[0].访问地址 = entity.访问地址;
                entities.ToArray()[0].描述 = entity.描述;
                entities.ToArray()[0].状态 = entity.状态;
                entities.ToArray()[0].绑定类型 = entity.绑定类型;
                entities.ToArray()[0].方法名称 = entity.方法名称;
                serviceDirectoryDC.SubmitChanges();
            }catch 
            {
                isUpdateOk = false;
            }
            return isUpdateOk;
        }

        #endregion

        #region 服务约束
        public static List<服务约束> GetAllTModel()
        {
            var entities = from tModel in serviceDirectoryDC.服务约束
                         where tModel.约束编码 !=null
                         select tModel;
            return entities.ToList();
        }

        public static 服务约束 GetTModelByMID(Guid MID)
        {
            var entity = from tModel in serviceDirectoryDC.服务约束
                         where tModel.约束编码 == MID
                         select tModel;
            return entity.ToArray()[0];
        }

        /// <summary>
        /// 根据服务地址编码获得服务约束
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public static List<服务约束> GetModelByBID(Guid BID)
        {
            var entities = from tModel in serviceDirectoryDC.服务约束
                           where tModel.服务地址编码 == BID
                           select tModel;
            return entities.ToList();
        }



        public static bool UpdateTModel(服务约束 entity)
        {
            bool isUpdateOk = true;
            try
            {
                var entities = serviceDirectoryDC.服务约束.Where(p => p.约束编码 == entity.约束编码);
                entities.ToArray()[0].服务地址编码 = entity.服务地址编码;
                entities.ToArray()[0].示例 = entity.示例;
                entities.ToArray()[0].描述 = entity.描述;
                serviceDirectoryDC.SubmitChanges();
            }
            catch 
            {
                isUpdateOk = false;
            }
            return isUpdateOk;
        }

        public static Guid AddTModel(服务约束 entity)
        {
            Guid MID = Guid.NewGuid();
            try
            {
                entity.约束编码 = MID;
                serviceDirectoryDC.服务约束.InsertOnSubmit(entity);
                serviceDirectoryDC.SubmitChanges();
            }
            catch 
            {
                MID = Guid.Empty;
            }
            return MID;
        }

        public static bool RemoveTModel(服务约束 entity)
        {
            bool isdeleteOk = true;
            try
            {
                serviceDirectoryDC.服务约束.DeleteOnSubmit(GetTModelByMID(entity.约束编码));
                serviceDirectoryDC.SubmitChanges();

            }
            catch 
            {
                isdeleteOk = false;
            }
            return isdeleteOk;
        }
    #endregion

        }
}
