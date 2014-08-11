using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JN.ESB.Core.Service.Common
{
    public static class EsbConfig
    {
        static string configPath = @"c:\Esb\EsbApp.config";

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="setting">配置文件节点</param>
        /// <returns>节点值</returns>
        public static string getConfigValue(string nodeName)
        {
            string settingValue = null;
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = configPath;

            if (ConfigurationManager.OpenMappedExeConfiguration(map, 0).HasFile)
            {
                settingValue = ConfigurationManager.OpenMappedExeConfiguration(map, 0).AppSettings.Settings[nodeName].Value;
            }

            return settingValue;
        }

        /// <summary>
        /// 根据数据库名获取到链接
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public static string getConnStringByDBName(string dbName)
        {
            string conn = null;
            string dbIp = EsbConfig.getConfigValue("DBServer");
            if (dbIp != null)
            {
                conn = String.Format("Data Source={0};Initial Catalog={1};Integrated Security=False;Connect Timeout=120;UserID=soa;Password=123456", dbIp, dbName);
            }

            return conn;
        }
    }
}
