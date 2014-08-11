using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JN.ESB.UDDI.Business.Logic;
using JN.ESB.UDDI.Core.DataAccess;
using JN.ESB.Exception.DataAccess;

namespace JN.ESB.Exception.Logic
{
    public class 错误消息处理逻辑
    {
        public void 创建错误消息(异常信息对象 异常对象)
        {
            ExceptionDataAccess.添加异常信息(异常对象);
            
            //throw new System.NotImplementedException();
        }

        public void 错误消息处理(异常信息对象 异常对象,异常结果 处理结果)
        {
            异常对象.异常信息状态 = (int)处理结果;
            ExceptionDataAccess.更新异常对象(异常对象);
            //throw new System.NotImplementedException();
        }

        public List<异常信息对象> 获得未处理的错误_服务编码(Guid 服务编码)
        {
            List<异常信息对象> 结果集 = new List<异常信息对象>();

            服务 具体服务 = new 服务();
            具体服务.服务编码 = 服务编码;
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            List<服务地址> 绑定信息 = 服务逻辑.获得绑定信息_具体服务(具体服务);
            List<异常信息对象> 异常对象集 = ExceptionDataAccess.获得所有的异常对象();
            
            foreach(服务地址 地址 in 绑定信息){
                List<异常信息对象> 对相集 = 异常对象集.FindAll(p=>p.绑定地址编码==地址.服务地址编码);
                foreach(异常信息对象 异常对象 in 对相集)
                {
                    if(异常对象.异常信息状态 == (int)异常结果.未处理)
                        结果集.Add(异常对象);
                }
            }
            return 结果集;
            //throw new System.NotImplementedException();
        }

        public List<异常信息对象> 获得所有错误信息()
        {
            return ExceptionDataAccess.获得所有的异常对象();
        }


        /// <summary>
        /// 获得没有消息体的异常对象
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maxRows"></param>
        /// <returns></returns>
        /// modified at 2010-12-2
        public List<异常信息对象> 获得错误信息(int startRowIndex,int maxRows)
        {
            return ExceptionDataAccess.获得异常对象(startRowIndex, maxRows);
        }

        public List<异常信息对象> 获得错误信息_用户编码(Guid userId)
        {
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            个人 管理员 = 服务逻辑.获得管理员_管理员编码(userId);
            //系统管理员
            if (管理员.权限 == 0)
                return ExceptionDataAccess.获得所有的异常对象();
            else
            {
                List<服务> services = 服务逻辑.获得具体服务_管理员(管理员);
                List<异常信息对象> 结果集 = new List<异常信息对象>();
                foreach (服务 服务 in services)
                {
                    foreach (异常信息对象 异常 in 获得所有错误信息_服务编码(服务.服务编码))
                        结果集.Add(异常);
                }
                return 结果集; ;
            }
        }

        public List<异常信息对象> 获得错误信息_用户编码(int startRowIndex, int maxRows, Guid userId)
        {
            return 获得错误信息_用户编码(userId).Skip(startRowIndex).Take(maxRows).ToList();
        }


        public List<异常信息对象> 获得所有错误信息_服务编码(int startRowIndex, int maxRows,Guid 服务编码)
        {
            List<异常信息对象> 结果集 = this.获得所有错误信息_服务编码(服务编码);
            return 结果集.Skip(startRowIndex).Take(maxRows).ToList();
        }

        public List<异常信息对象> 获得所有错误信息_服务编码(Guid 服务编码)
        {
            List<异常信息对象> 结果集 = new List<异常信息对象>();

            服务 具体服务 = new 服务();
            具体服务.服务编码 = 服务编码;
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            List<服务地址> 绑定信息 = 服务逻辑.获得绑定信息_具体服务(具体服务);
            List<异常信息对象> 异常对象集 = ExceptionDataAccess.获得所有的异常对象();

            foreach (服务地址 地址 in 绑定信息)
            {
                List<异常信息对象> 对相集 = 异常对象集.FindAll(p => p.绑定地址编码 == 地址.服务地址编码);
                foreach (异常信息对象 异常对象 in 对相集)
                {
                    结果集.Add(异常对象);
                }
            }
            return 结果集;
            //throw new System.NotImplementedException();
        }

        public List<异常信息对象> 获得所有错误信息_服务提供者编码(Guid 服务提供者编码)
        {
            List<异常信息对象> 结果集 = new List<异常信息对象>();

            业务实体 服务提供者 = new 业务实体();
            服务提供者.业务编码 = 服务提供者编码;
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            List<服务>  服务集= 服务逻辑.获得具体服务_服务提供者(服务提供者);
            
            foreach (服务 具体服务 in 服务集)
            {
                结果集.AddRange(this.获得所有错误信息_服务编码(具体服务.服务编码));
            }
            return 结果集;
        }


        public List<异常信息对象> 获得错误信息_服务提供者编码_用户编码(Guid 服务提供者编码,Guid 用户编码)
        {
            //List<异常信息对象> 结果集 = 获得所有错误信息_服务提供者编码(服务提供者编码);
            List<异常信息对象> 结果集 = new List<异常信息对象>();
            业务实体 服务提供者 = new 业务实体();
            服务提供者.业务编码 = 服务提供者编码;
            服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            List<服务> 服务集 = 服务逻辑.获得具体服务_服务提供者(服务提供者);
            个人 管理员 = 服务逻辑.获得管理员_管理员编码(用户编码);
            
            foreach (服务 具体服务 in 服务集)
            {
                if ((具体服务.个人编码 == 用户编码) || (管理员.权限==0))
                    结果集.AddRange(this.获得所有错误信息_服务编码(具体服务.服务编码));
            }
            return 结果集;
        }


        public List<异常信息对象> 获得所有错误信息_服务提供者编码(int startRowIndex,int maxRows,Guid 服务提供者编码)
        {
            //List<异常信息对象> 结果集 = new List<异常信息对象>();

            //业务实体 服务提供者 = new 业务实体();
            //服务提供者.业务编码 = 服务提供者编码;
            //服务目录业务逻辑 服务逻辑 = new 服务目录业务逻辑();
            //List<服务> 服务集 = 服务逻辑.获得具体服务_服务提供者(服务提供者);

            //foreach (服务 具体服务 in 服务集)
            //{
            //    结果集.AddRange(this.获得所有错误信息_服务编码(具体服务.服务编码));
            //}
            List<异常信息对象> 结果集 = this.获得所有错误信息_服务提供者编码(服务提供者编码);

            return 结果集.Skip(startRowIndex).Take(maxRows).ToList();
        }

        public List<异常信息对象> 获得错误信息_服务提供者编码_用户编码(int startRowIndex, int maxRows, Guid 服务提供者编码, Guid 用户编码)
        {
            
            List<异常信息对象> 结果集 = this.获得错误信息_服务提供者编码_用户编码(服务提供者编码,用户编码);

            return 结果集.Skip(startRowIndex).Take(maxRows).ToList();
        }

        public int 获得错误信息数量_服务提供者_用户编码(Guid 服务提供者编码, Guid 用户编码)
        {

            List<异常信息对象> 结果集 = this.获得错误信息_服务提供者编码_用户编码(服务提供者编码, 用户编码);

            return 结果集.Count;
        }

        public int 获得所有错误信息数量_服务提供者编码(Guid 服务提供者编码)
        {
            return this.获得所有错误信息_服务提供者编码(服务提供者编码).Count;
        }

        public List<异常信息对象> 获得错误消息_错误级别(异常级别 级别)
        {
            return ExceptionDataAccess.获得异常对象_异常级别(级别);
        }

        public 异常信息对象 获得错误消息_异常编码(Guid 异常编码)
        {
            return ExceptionDataAccess.获得异常处理对象_异常编码(异常编码);
        }

        public int 获得全部错误数量()
        {
            return ExceptionDataAccess.获得全部异常数量();
        }

        public void 修改错误消息(异常信息对象 异常对象)
        {
            ExceptionDataAccess.更新异常对象(异常对象);
        }
        public bool 删除错误消息(Guid 异常编码)
        {
            return ExceptionDataAccess.删除异常对象(异常编码);
        }
    }
}