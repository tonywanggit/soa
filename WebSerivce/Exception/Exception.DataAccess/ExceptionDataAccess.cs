using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JN.ESB.Exception.DataAccess
{
    public class ExceptionDataAccess
    {
        private static ExceptionDataClassesDataContext execptionDC = new ExceptionDataClassesDataContext();
        public ExceptionDataAccess()
        {

        }
        public static Guid 添加异常信息(异常信息对象 异常对象)
        {
            Guid newId = Guid.NewGuid();
            try
            {
                异常对象.异常编码 = newId;
                execptionDC.异常信息对象.InsertOnSubmit(异常对象);
                execptionDC.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
            }
            catch
            {
                newId = Guid.Empty;
            }
            return newId;
        }
        public static 异常信息对象 获得异常处理对象_异常编码(Guid 异常编码)
        {
            var exp = execptionDC.异常信息对象.Single(p => p.异常编码 == 异常编码);
            return exp;
        }
        public static List<异常信息对象> 获得所有的异常对象()
        {
            List<异常信息对象> 异常对象集合 = new List<异常信息对象>();
            var 查询结果 = execptionDC.异常信息对象.OrderByDescending(p=>p.异常时间).ToList();
            foreach (var 结果 in 查询结果)
            {
                异常对象集合.Add(结果);
            }
            return 异常对象集合;
        }

        public static List<异常信息对象> 获得异常对象(int startRowIndex,int maxRows)
        {
            List<异常信息对象> 异常对象集合 = new List<异常信息对象>();
            var 查询结果 = execptionDC.异常信息对象.OrderByDescending(p => p.异常时间).Skip(startRowIndex).Take(maxRows).ToList();
            foreach (var 结果 in 查询结果)
            {
                异常对象集合.Add(结果);
            }
            return 异常对象集合;
        }

        public static List<异常信息对象> 获得异常对象_无消息体(int startRowIndex, int maxRows)
        {
            List<异常信息对象> 异常对象集合 = new List<异常信息对象>();
            var 查询结果 = execptionDC.异常信息对象.OrderByDescending(p => p.异常时间).Skip(startRowIndex).Take(maxRows).ToList();
            foreach (var 结果 in 查询结果)
            {
                结果.请求消息体 = "";
                异常对象集合.Add(结果);
            }
            return 异常对象集合;
        }

        public static List<异常信息对象> 获得异常对象_异常状态(异常状态 状态)
        {
            List<异常信息对象> 异常对象集合 = new List<异常信息对象>();
            var 查询结果 = execptionDC.异常信息对象.Where(p => p.异常信息状态 == (int)状态).OrderByDescending(p => p.异常时间);
            foreach (var 结果 in 查询结果)
            {
                异常对象集合.Add(结果);
            }
            return 异常对象集合;
        }
        public static List<异常信息对象> 获得异常对象_异常级别(异常级别 级别)
        {
            List<异常信息对象> 异常对象集合 = new List<异常信息对象>();
            var 查询结果 = execptionDC.异常信息对象.Where(p => p.异常级别 == (int)级别).OrderByDescending(p => p.异常时间);
            foreach (var 结果 in 查询结果)
            {
                异常对象集合.Add(结果);
            }
            return 异常对象集合;
        }

        public static List<异常信息对象> 获得异常对象_服务地址编码(Guid 服务地址编码)
        {
            List<异常信息对象> 异常对象集合 = new List<异常信息对象>();
            var 查询结果 = execptionDC.异常信息对象.Where(p => p.绑定地址编码 == 服务地址编码).OrderByDescending(p => p.异常时间);
            foreach (var 结果 in 查询结果)
            {
                异常对象集合.Add(结果);
            }
            return 异常对象集合;
        }

        public static int 获得全部异常数量()
        {
            return execptionDC.异常信息对象.Count();

        }

        public static List<异常信息对象> 获得异常对象_服务地址编码(Guid 服务地址编码, int startRowIndex, int maxRows)
        {
            List<异常信息对象> 异常对象集合 = new List<异常信息对象>();
            var 查询结果 = execptionDC.异常信息对象.Where(p => p.绑定地址编码 == 服务地址编码)
                .OrderByDescending(p => p.异常时间).Skip(startRowIndex).Take(maxRows);
            foreach (var 结果 in 查询结果)
            {
                异常对象集合.Add(结果);
            }
            return 异常对象集合;

        }

        public static int 获得异常对象数量_服务地址编码(Guid 服务地址编码)
        {
            return execptionDC.异常信息对象.Where(p => p.绑定地址编码 == 服务地址编码).Count();

        }

        public static void 更新异常对象(异常信息对象 异常对象)
        {
            var 异常 = execptionDC.异常信息对象.Where(p => p.异常编码 == 异常对象.异常编码);
            foreach (var 异常单体 in 异常)
            {
                异常单体.绑定地址编码 = 异常对象.绑定地址编码;
                异常单体.消息编码 = 异常对象.消息编码;
                异常单体.异常编码 = 异常对象.异常编码;
                异常单体.异常代码 = 异常对象.异常代码;
                异常单体.异常级别 = 异常对象.异常级别;
                异常单体.异常类型 = 异常对象.异常类型;
                异常单体.异常描述 = 异常对象.异常描述;
                异常单体.异常时间 = 异常对象.异常时间;
                异常单体.异常信息状态 = 异常对象.异常信息状态;
                异常单体.主机名称 = 异常对象.主机名称;
                异常单体.请求消息体 = 异常对象.请求消息体;
                异常单体.方法名称 = 异常对象.方法名称;

            }
            execptionDC.SubmitChanges();
        }
        public static bool 删除异常对象(Guid 异常编码)
        {
            bool isDeletedOk = true;
            try
            {
                string _SSE = string.Format("Delete ExceptionCoreTb Where ExceptionID = '{0}'", 异常编码.ToString());
                execptionDC.ExecuteCommand(_SSE);
            }
            catch 
            {
                isDeletedOk = false;
            }
            return isDeletedOk;
        }


        public static int 添加异常类型(异常类型对象 异常类型) 
        {
            //Guid newId = Guid.NewGuid();
            int newId = -1;
            try
            {
                //异常类型.类型编码 = newId;
                execptionDC.异常类型对象.InsertOnSubmit(异常类型);
                execptionDC.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
                newId = 异常类型.类型编码;
            }
            catch
            {
                newId = -1;
            }
            return newId;
            
        }


        public static void 更新异常类型(异常类型对象 异常类型)
        {
            var 异常 = execptionDC.异常类型对象.Where(p => p.类型编码 == 异常类型.类型编码);
            foreach (var 异常单体 in 异常)
            {
                异常单体.描述 = 异常类型.描述;
                异常单体.异常级别 = 异常类型.异常级别;
                异常单体.级别描述 = 异常类型.级别描述;
            }
            execptionDC.SubmitChanges();
        }

        public static void 删除异常类型(异常类型对象 异常类型)
        {
            execptionDC.异常类型对象.DeleteOnSubmit(异常类型);
            execptionDC.SubmitChanges();
        }
    }
}
