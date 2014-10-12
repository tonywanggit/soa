using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using NewLife.Configuration;
using System.Xml;
using NewLife.Log;

namespace JN.ESB.Core.Service.Common
{
    /// <summary>
    /// 此类最终将被去除
    /// </summary>
    public static class EsbConfig
    {
        /// <summary>
        /// 保存连接字符串集合
        /// </summary>
        private static ConnectionStringSettingsCollection m_ConnStrings;

        /// <summary>
        /// Tony.2014.09.11
        /// 获取到连接字符串，如果配置了XCode.ConnConfigFile项则优先使用，否则使用ConnectionStrings节
        /// </summary>
        /// <returns></returns>
        private static ConnectionStringSettingsCollection GetConnectionStrings()
        {
            String connFile = Config.GetConfig<String>("XCode.ConnConfigFile");
            if (String.IsNullOrWhiteSpace(connFile))
            {
                return ConfigurationManager.ConnectionStrings;
            }
            else
            {
                ConnectionStringSettingsCollection cnsc = new ConnectionStringSettingsCollection();
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(connFile);

                    foreach (XmlNode conn in xmlDoc.SelectNodes("/connectionStrings/add"))
                    {
                        ConnectionStringSettings css = new ConnectionStringSettings();
                        css.Name = conn.Attributes["name"] == null ? String.Empty : conn.Attributes["name"].Value;
                        css.ConnectionString = conn.Attributes["connectionString"] == null ? String.Empty : conn.Attributes["connectionString"].Value;
                        css.ProviderName = conn.Attributes["providerName"] == null ? String.Empty : conn.Attributes["providerName"].Value;

                        cnsc.Add(css);
                    }
                }
                catch (Exception ex)
                {
                    XTrace.WriteLine("无法从XCode.ConnConfigFile配置节中获取到ConnectionStrings，异常详情：{0}", ex.ToString());
                }

                return cnsc;
            }
        }

        /// <summary>
        /// 根据连接字符串的名称查找连接字符串
        /// </summary>
        /// <param name="connName"></param>
        /// <returns></returns>
        public static String GetConnString(String connName)
        {
            if (m_ConnStrings == null)
            {
                m_ConnStrings = GetConnectionStrings();
            }

            foreach (ConnectionStringSettings item in m_ConnStrings)
            {
                if (item.Name == connName)
                    return item.ConnectionString;
            }

            return String.Empty;
        }

    }
}
