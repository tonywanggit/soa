using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using NewLife.Log;
using System.Threading;

namespace ESB.Core.Rpc
{
    /// <summary>
    /// 记录并提供查询代理程序集和端口类的方法
    /// </summary>
    public sealed class AssemblyType
    {
        #region 成员及构造
        /// <summary>
        /// 代理程序集路径
        /// </summary>
        public String AssemblyPath { get; set; }

        /// <summary>
        /// 端口类
        /// </summary>
        public String PortType { get; set; }

        /// <summary>
        /// 构造器私有化，避免被外部程序实例化
        /// </summary>
        private AssemblyType(String assemblyPath, String portType) 
        {
            AssemblyPath = assemblyPath;
            PortType = portType;
        }
        #endregion

        #region 静态成员及方法
        /// <summary>
        /// 默认的序列化地址
        /// </summary>
        private static String s_xmlFilePath = "AssemblyType.xml";

        /// <summary>
        /// 代理程序集和端口类列表
        /// </summary>
        //private static List<AssemblyType> s_lstAssemblyType = new List<AssemblyType>();

        /// <summary>
        /// 代理程序集和端口类字典
        /// </summary>
        private static Dictionary<String, AssemblyType> s_dictAssemblyType = new Dictionary<string, AssemblyType>();

        /// <summary>
        /// 是否需要将代理程序集和端口类列表序列化到磁盘上
        /// </summary>
        private static Boolean s_needWriteToDisk = false;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static AssemblyType()
        {
            s_xmlFilePath = MapPath("~/App_Data") + "\\" + s_xmlFilePath;

            StartXmlWriterThread();

            if (File.Exists(s_xmlFilePath))
            {
                NewLife.Xml.XmlReaderX xml = new NewLife.Xml.XmlReaderX();
                using (XmlReader xr = XmlReader.Create(s_xmlFilePath))
                {
                    try
                    {
                        Object obj = null;
                        xml.Reader = xr;

                        if (xml.ReadObject(typeof(Dictionary<String, AssemblyType>), ref obj, null) && obj != null)
                        {
                            s_dictAssemblyType = obj as Dictionary<String, AssemblyType>;
                        }
                    }
                    catch (Exception ex) { XTrace.WriteException(ex); }
                }
            }
        }

        /// <summary>
        /// 启动Xml序列化到磁盘的进程
        /// </summary>
        private static void StartXmlWriterThread()
        {
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(3000);

                    if (s_needWriteToDisk) // 如果需要序列化
                    {
                        try
                        {
                            if (File.Exists(s_xmlFilePath)) File.Delete(s_xmlFilePath);

                            NewLife.Xml.XmlWriterX xml = new NewLife.Xml.XmlWriterX();
                            using (XmlWriter writer = XmlWriter.Create(s_xmlFilePath))
                            {
                                xml.Writer = writer;
                                xml.WriteObject(s_dictAssemblyType, typeof(Dictionary<String, AssemblyType>), null);
                            }
                            s_needWriteToDisk = false;

                            XTrace.WriteLine("已将代理程序集和端口类列表序列化到磁盘上AssemblyType.xml。");
                        }
                        catch (Exception ex)
                        {
                            XTrace.WriteException(ex);
                        }
                    }
                }
            });
            thread.Start();
        }

        /// <summary>
        /// 增加程序集路径和端口类，并序列化到XML文件
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <param name="portType"></param>
        public static void Add(String assemblyPath, String portType)
        {
            AssemblyType assType = GetInstance(assemblyPath);
            if (assType == null)
            {
                //s_lstAssemblyType.Add(new AssemblyType(assemblyPath, portType));
                s_dictAssemblyType[assemblyPath] = new AssemblyType(assemblyPath, portType);
            }
            else
            {
                assType.PortType = portType;
            }

            s_needWriteToDisk = true; // 表示需要序列化到磁盘上
        }

        /// <summary>
        /// 获取到程序集端口类的一个实例
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <returns></returns>
        public static AssemblyType GetInstance(String assemblyPath)
        {
            if (s_dictAssemblyType.ContainsKey(assemblyPath))
                return s_dictAssemblyType[assemblyPath];
            else
                return null;

            //return s_lstAssemblyType.SingleOrDefault(x => x.AssemblyPath == assemblyPath);
        }

        /// <summary>
        /// 获取到程序集端口类列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<String, AssemblyType> GetAssemblyTypeList()
        {
            return s_dictAssemblyType;
        }
        #endregion

        #region 工具方法
        /// <summary>
        /// 计算Web 服务器上的指定虚拟路径相对应的物理文件路径，针对多线程下HttpContext.Current为NULL的情况进行了处理
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static String MapPath(String path)
        {
            if (HttpContext.Current != null)
                return HttpContext.Current.Server.MapPath(path);
            else
            {
                path = path.Replace("/", "").Replace("~", "");
                if (path.StartsWith("//"))
                    path = path.TrimStart('/');

                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }
        }
        #endregion
    }
}
