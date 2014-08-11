using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using JN.ESB.Exception.DataAccess;
using JN.ESB.Exception.Logic;
using System.Net.Mail;
using JN.ESB.UDDI.Business.Logic;
using JN.ESB.UDDI.Core.DataAccess;
using System.Configuration;
using System.Xml;
using Exception.Service.Audit;

namespace JN.ESB.Exception.Service
{
    

    /// <summary>
    /// 错误消息服务 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.JN.com/esb/Exception/service/20100329")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class 错误消息服务 : System.Web.Services.WebService
    {
        static string configPath = @"c:\Esb\EsbApp.config";
        /// <summary>
        /// 将异常信息写入数据库
        /// </summary>
        /// <param name="异常描述"></param>
        /// <param name="异常代码"></param>
        /// <param name="异常信息"></param>
        /// <param name="异常级别"></param>
        /// <param name="异常类型"></param>
        /// <param name="主机名称"></param>
        /// <param name="方法名称"></param>
        /// <param name="消息编码"></param>
        /// <param name="绑定地址编码"></param>
        /// <param name="异常信息状态"></param>
        /// <param name="请求消息体"></param>
        [WebMethod]
        public void 添加错误消息(String 异常描述, String 异常代码, String 异常信息, int 异常级别, int 异常类型, String 主机名称, String 方法名称, String 消息编码, String 绑定地址编码, int 异常信息状态, String 请求消息体, int 请求类型, String 请求密码, int 绑定类型)
        {
            //bool isAddOk = true;
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            异常信息对象 异常对象 = new 异常信息对象();
            异常对象.异常时间 = System.DateTime.Now;
            异常对象.异常描述 = 异常描述.Trim();
            异常对象.异常代码 = 异常代码.Trim();
            异常对象.异常信息 = 异常信息.Trim();
            异常对象.异常级别 = 异常级别;
            异常对象.异常类型 = 异常类型;
            异常对象.主机名称 = 主机名称.Trim();
            异常对象.方法名称 = 方法名称.Trim();
            异常对象.请求密码 = 请求密码.Trim();
            异常对象.绑定类型 = 绑定类型;
            异常对象.请求类型 = 请求类型;
            异常对象.消息编码 = new Guid(消息编码);
            异常对象.绑定地址编码 = new Guid(绑定地址编码);
            异常对象.异常信息状态 = 异常信息状态;
            异常对象.请求消息体 = 请求消息体;
            XmlDocument document = new XmlDocument();
            document.LoadXml(请求消息体);

            string serviceName = document.DocumentElement.SelectSingleNode("服务名称").InnerText.Trim();
            string reqBeginTime = document.DocumentElement.SelectSingleNode("请求时间").InnerText.Trim();

            //Audit.AuditServcie auditServcie = new JN.ESB.Exception.Service.Audit.AuditServcie();
            //auditServcie.AddAuditBusiness(主机名称, 请求消息体, 消息编码, 方法名称, reqBeginTime, serviceName, 0);
            服务目录业务逻辑 UDDI = new 服务目录业务逻辑();
            List<个人> 系统管理员 = UDDI.获得系统管理员();
            if ((String.IsNullOrEmpty(绑定地址编码.Trim()) || 绑定地址编码.Trim() == "00000000-0000-0000-0000-000000000000"))
            {
                
                if(UDDI.获得绑定信息_服务名称(serviceName)!=null){
                    异常对象.绑定地址编码 = UDDI.获得绑定信息_服务名称(serviceName).服务地址编码;
                }
            }

            错误逻辑.创建错误消息(异常对象);

            try
            {
                if (!(异常对象.绑定地址编码.Value == Guid.Empty))
                {
                    服务地址 地址 = UDDI.获得绑定信息_服务地址编码(异常对象.绑定地址编码.Value);
                    个人 服务管理员 = UDDI.获得管理员_具体绑定服务(地址);
                    if (!(系统管理员.Contains(服务管理员)))
                        系统管理员.Add(服务管理员);
                    

                }
                this.发送OA邮件(异常对象, 系统管理员);
            }
            catch { }
            
            
        }
       


        /// <summary>
        /// 发生异常时，发送电子邮件通知服务管理员
        /// </summary>
        /// <param name="异常对象">待发送的异常信息对象</param>
        /// <param name="邮件地址">收件人邮件地址，多个收件人用分号隔开</param>
        /// <returns></returns>
        public bool 发送电子邮件(异常信息对象 异常对象, string 邮件地址) 
        {
            
            SmtpClient _smtpClient = new SmtpClient();
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            _smtpClient.Host = "127.0.0.1"; //指定SMTP服务器
            _smtpClient.Port = 25;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
            string strFrom = "EsbAdmin@JN.com";
            
            string portalUrl = 获取门户地址();
            string detailLink = portalUrl + "/Exception/ResendRequestMsg.aspx?msgId=" + 异常对象.异常编码;
            MailAddress from = new MailAddress(strFrom,"Esb管理员");
            
            char[] sp = new char[]{';'};
            string[] 管理员邮件地址 = 邮件地址.Trim().Split(sp);
            MailMessage _mailMessage = new MailMessage();
            _mailMessage.From = from;
            for (int i=0;i<管理员邮件地址.Length;i++)
            {
                MailAddress to = new MailAddress(管理员邮件地址[i]);
                _mailMessage.To.Add(to);
            }         

            _mailMessage.Subject = "服务调用异常通知";//主题          
            _mailMessage.IsBodyHtml = true;//设置为HTML格式
            _mailMessage.BodyEncoding = System.Text.Encoding.Default;//正文编码
            _mailMessage.Body = String.Format("ESB服务有未成功的调用请求，请处理! <a href={0}>点击此处</a>查看详细信息", detailLink);//内容
            
            
            _mailMessage.Priority = MailPriority.High;//优先级
            
            try
            {
                _smtpClient.Send(_mailMessage);
                return true;
            }
            catch(System.Exception e)
            {
                return false;
            }

        }

        public void 发送OA邮件(异常信息对象 异常对象, List<个人> 管理员)
        {
            foreach (个人 per in 管理员)
            {
                this.发送OA邮件(异常对象, per.邮件地址);
            }
        }

        public bool 发送OA邮件(异常信息对象 异常对象, string 邮件地址)
        {
            string portalUrl = 获取门户地址();
            //string portalUrl = "http://10.30.4.101/EsbPortal";
            string detailLink = portalUrl + "/Exception/ResendRequestMsg.aspx?msgId=" + 异常对象.异常编码;

            return true;
            //OAMail.EmailService OA邮件服务 = new JN.ESB.Exception.Service.OAMail.EmailService();
            //string message =  String.Format("ESB服务有未成功的调用请求，请处理! <a href={0}>点击此处</a>查看详细信息", detailLink);//内容

            //string result = OA邮件服务.SendEmaiToOA("administrator", 邮件地址, "ESB服务调用异常通知", message, "");
            //if(result.Equals("1"))
            //    return true;
            //else
            //    return false;
        }
        [WebMethod]
        public List<异常信息对象> 获得所有错误消息()
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            return 错误逻辑.获得所有错误信息();
        }
        
        [WebMethod]
        public List<异常信息对象> 获得错误消息_服务编码(Guid 服务编码)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            return 错误逻辑.获得所有错误信息_服务编码(服务编码);
        }

        [WebMethod]
        public List<异常信息对象> 获得分页错误消息_服务提供者编码(int startRowIndex, int maxRows, Guid 服务提供者编码)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            return 错误逻辑.获得所有错误信息_服务提供者编码(startRowIndex, maxRows,服务提供者编码);
        }

        
        [WebMethod]
        public int 获得分页错误消息数量_服务提供者编码(int startRowIndex, int maxRows, Guid 服务提供者编码)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            return 错误逻辑.获得所有错误信息数量_服务提供者编码(服务提供者编码);
        }

        [WebMethod]
        public int 获得分页错误消息数量_服务提供者_用户编码(int startRowIndex, int maxRows, Guid 用户编码,Guid 服务提供者编码)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            return 错误逻辑.获得错误信息数量_服务提供者_用户编码(服务提供者编码, 用户编码);
        }

        [WebMethod]
        public List<异常信息对象> 获得分页错误消息_服务提供者_用户编码(int startRowIndex, int maxRows,Guid 用户编码, Guid 服务提供者编码)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            return 错误逻辑.获得错误信息_服务提供者编码_用户编码(startRowIndex, maxRows, 服务提供者编码,用户编码);
        }


        [WebMethod]
        public List<异常信息对象> 获得错误消息_服务提供者编码(Guid 服务提供者编码)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            return 错误逻辑.获得所有错误信息_服务提供者编码(服务提供者编码);
        }


        [WebMethod]
        public 异常信息对象 获得错误消息_异常编码(Guid 异常编码)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            return 错误逻辑.获得错误消息_异常编码(异常编码);
        }

        [WebMethod]
        public bool 删除错误消息_异常编码(Guid 异常编码)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            //异常信息对象 异常 = 错误逻辑.获得错误消息_异常编码(异常编码);
            
            //AuditServcie audit = new AuditServcie();
            bool result = 错误逻辑.删除错误消息(异常编码);
            //if (result)
            //    audit.ExceptionResend(异常.消息编码.Value);

            return result;
        }

        [WebMethod]
        public int 获取未处理异常数量_服务提供者编码(Guid 服务提供者编码)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            服务目录业务逻辑 UDDI = new 服务目录业务逻辑();
            业务实体 服务提供者 = new 业务实体();
            服务提供者.业务编码 = 服务提供者编码;
            int 异常数量 = 0;
            List<服务> 服务集 = UDDI.获得具体服务_服务提供者(服务提供者);
            foreach (服务 具体服务 in 服务集)
            {
                异常数量 = 异常数量 + 错误逻辑.获得未处理的错误_服务编码(具体服务.服务编码).Count;
            }
            return 异常数量;
        }

        /// <summary>
        /// 从配置文件中获取Esb管理门户的地址
        /// </summary>
        /// <returns>Esb管理门户网站地址</returns>
        private string 获取门户地址()
        {
            return this.getConfigSetting("PortalUrl");            
        }

        /// <summary>
        /// 判断是否需要发送电子邮件
        /// </summary>
        /// <returns></returns>
        private bool 是否发送邮件()
        {
            string sendMail = this.getConfigSetting("SendMail");
            if (sendMail.Trim().ToUpper().Equals("TRUE"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="setting">配置文件节点</param>
        /// <returns>节点值</returns>
        private string getConfigSetting(string setting)
        {
            string settingValue = null;
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = configPath;
            if (ConfigurationManager.OpenMappedExeConfiguration(map, 0).HasFile)
            {
                settingValue = ConfigurationManager.OpenMappedExeConfiguration(map, 0).AppSettings.Settings[setting].Value;
            }
            return settingValue;
        }

        [WebMethod]
        public List<异常信息对象> 获得错误消息(int startRowIndex, int maxRows)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            return 错误逻辑.获得错误信息(startRowIndex, maxRows);
        }

        [WebMethod]
        public List<异常信息对象> 获得错误消息_用户编码(int startRowIndex, int maxRows,Guid 用户编码)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            List<异常信息对象> 异常列表 = 错误逻辑.获得错误信息_用户编码(startRowIndex, maxRows, 用户编码);
            
            return 异常列表;
        }

        /// <summary>
        /// 2011.1 created
        /// </summary>
        /// <param name="msgId"></param>
        /// <returns></returns>
        [WebMethod]
        public string 获得错误消息内容(Guid exceptionId)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            return 错误逻辑.获得错误消息_异常编码(exceptionId).请求消息体;
        }

        [WebMethod]
        public int 获得错误消息数量_用户编码(int startRowIndex, int maxRows, Guid 用户编码)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            return 错误逻辑.获得错误信息_用户编码(用户编码).Count;
        }


        [WebMethod]
        public List<异常信息对象> 获得分页错误消息_服务编码(int startRowIndex, int maxRows,Guid 服务编码)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            return 错误逻辑.获得所有错误信息_服务编码(startRowIndex, maxRows,服务编码);
        }

        [WebMethod]
        public int 获得错误消息数量_服务编码( int startRowIndex, int maxRows,Guid 服务编码)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            return 错误逻辑.获得所有错误信息_服务编码(服务编码).Count;
        }

        [WebMethod]
        public int 获得所有错误消息数量(int startRowIndex, int maxRows)
        {
            错误消息处理逻辑 错误逻辑 = new 错误消息处理逻辑();
            return 错误逻辑.获得全部错误数量();
        }
    }
}
