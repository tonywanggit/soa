using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;

namespace JN.ESB.Core.Service.Common
{
    public static class 日志
    {

        static string configPath = @"c:\Esb\EsbApp.config";

        public enum 日志等级
        {
            无,
            信息,
            警告,
            错误
        }

        private static 日志等级 获取日志记录级别()
        {
            string 日志记录级别 = getConfigSetting("EventLog");

            switch (日志记录级别.Trim().ToUpper())
            {
                case "INFORMATION":
                    return 日志等级.信息;
                case "WARNING":
                    return 日志等级.警告;
                case "ERROR":
                    return 日志等级.错误;
                default:
                    return 日志等级.无;

            }
        }
        public static void 写日志(string 日志内容,日志等级 写入日志等级) 
        {
            try
            {
                日志等级 日志记录级别 = 获取日志记录级别();
                if (日志记录级别 == 日志等级.无)
                {
                    return;
                }
               
                switch (写入日志等级)
                {
                    case 日志等级.无:
                        break;

                    case 日志等级.信息:
                        if (日志记录级别 == 日志等级.信息)
                        {
                            写入日志(日志内容, EventLogEntryType.Information);                            
                        }
                        break;

                    case 日志等级.警告:
                        if ((日志记录级别 == 日志等级.信息)||(日志记录级别 == 日志等级.警告))
                        {
                            写入日志(日志内容, EventLogEntryType.Warning);                            
                        }
                        break;

                    case 日志等级.错误:
                        写入日志(日志内容, EventLogEntryType.Error);
                        break;
                }
            }
            catch { }
        }

        private static void 写入日志(string 内容, EventLogEntryType 等级)
        {
            if (!EventLog.SourceExists("ESB"))
            {
                EventLog.CreateEventSource("ESB", "ESB");
            }
            EventLog.WriteEntry("ESB", 内容, 等级);
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="setting">配置文件节点</param>
        /// <returns>节点值</returns>
        public static string getConfigSetting(string setting)
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
    }
}


