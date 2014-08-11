using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace JN.ESB.Core.Service.Common
{
    public static class UDDI访问
    {

        static readonly string exceptMsg = "无法获得UDDI信息,或服务地址已停用";
        /// <summary>
        /// 根据Biztalk SQL适配器返回的消息内容，获取对应的UDDI数量
        /// </summary>
        /// <param name="UddiMsg">消息内容</param>
        /// <returns>UDDI数量</returns>
        public static int GetUddiCount(string UddiMsg,string methodName)
        {
            List<UDDI对象> _lstUddi = GetUddiByMsg(UddiMsg, methodName);
            if (_lstUddi.Count == 0) throw new Exception(exceptMsg);
            return _lstUddi.Count; 
        }

        /**
        /// <summary>
        /// 根据索引获取UDDI对象
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>UDDI对象</returns>
        public static UDDI对象 GetUddi(string UddiMsg,string methodName,int index)
        {
            List<UDDI对象> _lstUddi = GetUddiByMsg(UddiMsg, methodName);;
            return _lstUddi.ElementAt(index);
        }
        **/
        /// <summary>
        /// 根据Biztalk SQL适配器返回的消息内容以及索引，获取UDDI对象
        /// </summary>
        /// <param name="UddiMsg">消息内容</param>
        /// <param name="index">索引</param>
        /// <returns>UDDI对象</returns>
        public static UDDI对象 GetUddi(string UddiMsg,string methodName, int index)
        {
            try
            {
                List<UDDI对象> _lstUddi = GetUddiByMsg(UddiMsg, methodName);
                if (_lstUddi.Count == 0) throw new Exception(exceptMsg);
                return _lstUddi[index];
            }
            catch (Exception e)
            {
                throw new Exception(exceptMsg);
            }
        }

        /// <summary>
        /// 根据Biztalk SQL适配器返回的消息内容，获取UDDI对象列表
        /// </summary>
        /// <param name="UddiMsg">消息内容</param>
        /// <returns>UDDI对象列表</returns>
        public static List<UDDI对象> GetUddiByMsg(string UddiMsg,string methodName)
        {
            List<UDDI对象> lstUDDI = new List<UDDI对象>();
            try
            {
                
                XDocument doc = XDocument.Load(new StringReader(UddiMsg));
                //XDocument doc = XDocument.Load(@"C:\XMLFile.xml");
                //XDocument root = (XDocument)doc.Root.FirstNode;
                XNamespace ns = "http://www.jn.com/Esb";

                IEnumerable<XElement> query = doc.Descendants(ns + "BTable");
                foreach (var record in query){
                    UDDI对象 uddi = new UDDI对象();
                    uddi.TemplateID = new Guid(record.Attribute("TemplateID").Value);
                    uddi.ServiceID = new Guid(record.Attribute("ServiceID").Value);
                    uddi.Url = record.Attribute("Address").Value;
                    uddi.MethodName = methodName;
                    uddi.ServiceStatus = Int32.Parse(record.Attribute("BindingStatus").Value);
                    uddi.ServiceType = Int32.Parse(record.Attribute("BindingType").Value);
                    //uddi.UpdateStatus();
                    if(uddi.ServiceStatus!=(int)服务状态.停用)
                        lstUDDI.Add(uddi);
                }

            }
            catch (Exception e){
                throw new Exception(exceptMsg);
            }
            //
            return lstUDDI;
        }

        
    }
}
