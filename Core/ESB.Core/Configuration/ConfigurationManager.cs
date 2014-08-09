using ESB.Core.Util;
using NewLife.Configuration;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace ESB.Core.Configuration
{
    /// <summary>
    /// 配置管理器：负责将配置文件实例化到本地磁盘，并从本地磁盘中加载配置文件
    /// </summary>
    public class ConfigurationManager
    {
        private static ConfigurationManager m_ConfigurationManager;
        /// <summary>
        /// 获取到配置管理器单例
        /// </summary>
        /// <returns></returns>
        public static ConfigurationManager GetInstance()
        {
            if (m_ConfigurationManager != null) return m_ConfigurationManager;

            ConfigurationManager cm = new ConfigurationManager();
            Interlocked.CompareExchange<ConfigurationManager>(ref m_ConfigurationManager, cm, null);

            return m_ConfigurationManager;
        }

        /// <summary>
        /// 配置管理器
        /// </summary>
        private ConfigurationManager()
        {
            FilePath = Config.GetConfig<String>("ESB.LocalDataPath");

            if (String.IsNullOrEmpty(FilePath))
                FilePath = "ESBData";


            if (!Directory.Exists(ESBDataPath))
                throw new Exception("没有找到ESBData配置目录，请建立相关目录并添加ConsumerConfig.xml文件！");


            if (!File.Exists(ConsumerConfigPath))
                throw new Exception("没有找到ConsumerConfig.xml文件！");
        }

        private String m_ESBDataPath;
        /// <summary>ESBData全路径</summary>
        public String ESBDataPath
        {
            get
            {
                if (!String.IsNullOrEmpty(m_ESBDataPath)) return m_ESBDataPath;

                String dir = FilePath;
                if (!Path.IsPathRooted(dir)) dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath);

                //保证\结尾
                if (!String.IsNullOrEmpty(dir) && dir.Substring(dir.Length - 1, 1) != @"\") dir += @"\";

                m_ESBDataPath = new DirectoryInfo(dir).FullName;
                return m_ESBDataPath;
            }
        }

        private String m_ConsumerConfigPath;
        /// <summary>
        /// 消费者配置文件路径
        /// </summary>
        public String ConsumerConfigPath
        {
            get
            {
                return Path.Combine(ESBDataPath, "ConsumerConfig.xml");
            }
        }

        private String m_ESBConfigPath;
        /// <summary>
        /// ESBConfig配置文件路径
        /// </summary>
        public String ESBConfigPath
        {
            get
            {
                return Path.Combine(ESBDataPath, "ESBConfig.xml");
            }
        }

        /// <summary>ESBData配置路径</summary>
        public String FilePath {get;set;}

        /// <summary>
        /// 加载消费者配置文件
        /// </summary>
        /// <returns></returns>
        public ConsumerConfig LoadConsumerConfig()
        {
            String consumerConfig = File.ReadAllText(ConsumerConfigPath);
            return XmlUtil.LoadObjFromXML<ConsumerConfig>(consumerConfig);
        }

        /// <summary>
        /// 将消费者配置文件写入磁盘
        /// </summary>
        /// <param name="consumerConfig"></param>
        public void SaveConsumerConfig(ConsumerConfig consumerConfig)
        {
            File.WriteAllText(ConsumerConfigPath, XmlUtil.SaveXmlFromObj<ConsumerConfig>(consumerConfig));
        }

        /// <summary>
        /// 获取本地的ESBConfig配置信息，如果没有则返回null
        /// </summary>
        /// <returns></returns>
        public ESBConfig LoadESBConfig()
        {
            if (!File.Exists(ESBConfigPath))
                return null;

            String esbConfig = File.ReadAllText(ESBConfigPath);
            return XmlUtil.LoadObjFromXML<ESBConfig>(esbConfig);
        }

        /// <summary>
        /// 将ESBConfig配置信息写入磁盘
        /// </summary>
        /// <param name="consumerConfig"></param>
        public void SaveESBConfig(ESBConfig esbConfig)
        {
            try
            {
                File.WriteAllText(ESBConfigPath, XmlUtil.SaveXmlFromObj<ESBConfig>(esbConfig));
            }
            catch (Exception ex)
            {
                XTrace.WriteLine("序列化ESBConfig.xml文件失败：{0}", ex.ToString());
            }
        }
    }
}
