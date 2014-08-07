using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Net;
using System.IO;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml.Serialization;
using System.Text;
using NewLife.Log;
using NewLife.Security;
using System.Text.RegularExpressions;

namespace ESB.Core.Rpc
{
    /// <summary>
    /// 目标服务调用代理缓存类
    /// </summary>
    public static class SoapClientCache
    {
        /// <summary>
        /// 存放代理程序集缓存对象
        /// </summary>
        private static Dictionary<String, SoapClientItem> s_soapClientDict = new Dictionary<string, SoapClientItem>();
        /// <summary>
        /// 存放每个代理程序集的编译锁
        /// </summary>
        private static Dictionary<String, Object> s_locks = new Dictionary<String, Object>();

        /// <summary>
        /// 获取到指定URL的代理程序集
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static SoapClientItem GetItem(String url, String serviceName)
        {
            if (!s_soapClientDict.ContainsKey(url))
            {
                lock (GetLock(url))
                {
                    if (!s_soapClientDict.ContainsKey(url))
                    {
                        try
                        {
                            SoapClientItem item = LoadAssembly(url, serviceName);
                            s_soapClientDict.Add(url, item);
                        }
                        catch(Exception ex)
                        {
                            XTrace.WriteException(ex);
                            throw ex;
                        }
                    }
                }
            }
            return s_soapClientDict[url];
        }

        /// <summary>
        /// 获取到代理程序集的编译锁
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static Object GetLock(String url)
        {
            if (!s_locks.ContainsKey(url))
            {
                lock (s_locks)
                {
                    if (!s_locks.ContainsKey(url))
                        s_locks.Add(url, new Object());
                }
            }

            return s_locks[url];
        }

        /// <summary>
        /// 清除指定URL的调用代理程序集缓存,包括缓存的DLL文件
        /// </summary>
        public static void ClearCache(String url)
        {
            if (s_soapClientDict.ContainsKey(url))
            {
                s_soapClientDict.Remove(url);
            }

            String assemblyPath = GetAssemblyPath(url);
            if (File.Exists(assemblyPath))
            {
                File.Delete(assemblyPath);
            }
        }

        /// <summary>
        /// 获取代理程序集缓存数
        /// </summary>
        /// <returns></returns>
        public static int GetSoapClientNum()
        {
            return s_soapClientDict.Count;
        }

        /// <summary>
        /// 获取代理程序集缓存列表
        /// </summary>
        /// <returns></returns>
        public static List<String> GetSoapClientCacheList()
        {
            List<String> urls = new List<string>();
            foreach (var item in s_soapClientDict)
            {
                urls.Add(item.Key);
            }

            return urls;
        }

        /// <summary>
        /// 获取到程序集的路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static String GetAssemblyPath(String url)
        {
            String assemblyName = String.Format("Assembly_{0}.dll", DataHelper.Hash(url));
            String assemblyPath = MapPath("~/App_Data") + "\\" + assemblyName;

            return assemblyPath;
        }

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

        /// <summary>
        /// 加载代理程序集
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private static SoapClientItem LoadAssembly(String url, String serviceName)
        {
            String assemblyPath = GetAssemblyPath(url);
            String portTypeFullName = String.Empty; // 注意此处要用全名：命名空间.类名

            // 如果代理程序集不存在则编译生成代理程序集，否则直接加载
            if (!File.Exists(assemblyPath))
            {
                portTypeFullName = CompilerAssembly(url, serviceName, assemblyPath);
            }
            else
            {
                portTypeFullName = AssemblyType.GetInstance(assemblyPath).PortType;
            }

            // 如果没有获取到端口类型则清空程序集缓存并抛出异常
            if (portTypeFullName == String.Empty)
            {
                ClearCache(url);
                throw new Exception(String.Format("获取服务：{0}『{1}』的端口类型失败！", serviceName, url));
            }

            Assembly asm = null;
            using (FileStream fs = File.OpenRead(assemblyPath))
            {
                asm = Assembly.Load(fs.ReadBytes(fs.Length)); // 此处采用文件流进行加载，使得加载后的程序集仍然可以被重新编译
            }

            if (asm == null)
                throw new Exception(String.Format("加载代理程序集{0}失败！", assemblyPath));

            //Assembly asm = result.CompiledAssembly;
            //Type portType = asm.GetType(String.Format("{0}.{1}", serviceName, portTypeName)); 
            //Type reqType = asm.GetType(String.Format("{0}.request", serviceName));

            // 如果在前面为代理类添加了命名空间，此处需要将命名空间添加到类型前面。
            Type portType = GetTypeFromAssembly(asm, portTypeFullName, assemblyPath);
            Type reqType = GetTypeFromAssembly(asm, String.Format("{0}.request", portTypeFullName.Split('.')[0]), assemblyPath); 

            // 创建端口类对象
            Object port = Activator.CreateInstance(portType);
            if(port == null)
                throw new Exception(String.Format("创建：{0}『{1}』的端口类型对象失败！", serviceName, url));

            // 将超时设置为10分钟
            portType.InvokeMember("Timeout", BindingFlags.SetProperty, null, port, new Object[] { 10 * 60 * 1000 });

            SoapClientItem item = new SoapClientItem()
            {
                ProxyAssembly = asm,
                PortObject = port,
                PortType = portType,
                RequestType = reqType
            };

            return item;
        }

        /// <summary>
        /// 从代理程序集中获取到指定的类型，如果失败则抛出异常
        /// </summary>
        /// <param name="asm"></param>
        /// <param name="typeName"></param>
        /// <param name="assemblyPath"></param>
        /// <returns></returns>
        private static Type GetTypeFromAssembly(Assembly asm, String typeName, String assemblyPath)
        {
            Type type = asm.GetType(typeName);
            if (type == null)
            {
                throw new Exception(String.Format("从代理程序集：{0}中获取类型『{1}』失败！", assemblyPath, typeName));
            }
            else
            {
                return type;
            }
        }

        /// <summary>
        /// 编译程序集
        /// </summary>
        /// <param name="url"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private static String CompilerAssembly(String url, String serviceName, String assemblyPath)
        {
            // 1. 使用 WebClient 下载 WSDL 信息。
            WebClient web = new WebClient();
            using (Stream stream = web.OpenRead(url + "?WSDL"))
            {
                // 2. 创建和格式化 WSDL 文档。
                ServiceDescription description = ServiceDescription.Read(stream);

                // 3. 创建客户端代理代理类。
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();

                importer.ProtocolName = "Soap"; // 指定访问协议。
                importer.Style = ServiceDescriptionImportStyle.Client; // 生成客户端代理。
                importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;

                importer.AddServiceDescription(description, null, null); // 添加 WSDL 文档。

                // 4. 使用 CodeDom 编译客户端代理类。
                CodeNamespace nmspace = new CodeNamespace(serviceName); // 为代理类添加命名空间，缺省为全局空间。
                CodeCompileUnit unit = new CodeCompileUnit();
                unit.Namespaces.Add(nmspace);

                ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

                // 4.1 根据ESB的需要修正自动生成的代码, 只针对Java进行修正
                if(!url.EndsWith(".asmx"))
                    FixAutoGenCode(nmspace, description);

                CompilerParameters parameter = new CompilerParameters();
                parameter.GenerateExecutable = false;
                parameter.GenerateInMemory = false;
                parameter.OutputAssembly = assemblyPath; // 可以指定你所需的任何文件名。
                parameter.ReferencedAssemblies.Add("System.dll");
                parameter.ReferencedAssemblies.Add("System.XML.dll");
                parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
                parameter.ReferencedAssemblies.Add("System.Data.dll");

                CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);

                // 5. 如果编译出现异常，则直接抛出
                if (result.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new StringBuilder();
                    sb.AppendLine("编译代理程序集出现异常：");

                    foreach (CompilerError ce in result.Errors)
                    {
                        sb.Append(ce.ToString() + System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }

                // 6. 返回代理类的名词
                Regex regSoap = new Regex("Soap$");
                // 注意：此处要用全名，因为可能存在多个服务共用一个DLL的情况
                String portTypeFullName = serviceName + "." + regSoap.Replace(description.PortTypes[0].Name, "");

                //--记录代理程序集的端口类名，当不需要编译程序集的时候可以直接从AssemblyType.GetInstance(assemblyPath).PortType获取
                AssemblyType.Add(assemblyPath, portTypeFullName);

                return portTypeFullName;
            }
        }

        /// <summary>
        /// 针对WSDL自动生成的代码进行调整，避免在调用Java服务时返回NULL
        /// </summary>
        /// <param name="nmspace"></param>
        private static void FixAutoGenCode(CodeNamespace nmspace, ServiceDescription wsdl)
        {
            // 1 寻找代理类
            String portTypeName = wsdl.PortTypes[0].Name;

            CodeTypeDeclaration portTypeDeclaration = null;
            foreach (CodeTypeDeclaration ctd in nmspace.Types)
            {
                if (ctd.Name + "Soap" == portTypeName)
                {
                    portTypeDeclaration = ctd;
                    break;
                }
            }
            if (portTypeDeclaration == null) return;


            // 2 寻找代理类中可以调用的方法
            List<String> methods = new List<string>();
            foreach (Message messge in wsdl.Messages)
            {
                String methodName = messge.Name.Replace("Request", "").Replace("Response", "");
                if (!methods.Contains(methodName))
                {
                    methods.Add(methodName);
                }
            }
            if (methods.Count == 0) return;


            // 3 给所有暴露的方法增加
            foreach (CodeTypeMember item in portTypeDeclaration.Members)
            {
                if (methods.Contains(item.Name))
                {
                    CodeMemberMethod method = item as CodeMemberMethod;
                    if (method != null)
                    {
                        //--如果暴露方法中含有自定义返回特性则是ASMX,可以直接返回
                        if (method.ReturnTypeCustomAttributes.Count > 0) return;

                        method.ReturnTypeCustomAttributes.Add(new CodeAttributeDeclaration("System.Xml.Serialization.XmlElementAttribute"
                            , new CodeAttributeArgument("Namespace", new CodePrimitiveExpression("http://schemas.jn.com/esb/response/20100329"))
                        ));
                    }
                }
            }

            // 4 给所有方法的返回参数添加特性
            foreach (CodeTypeDeclaration ctd in nmspace.Types)
            {
                if (ctd.Name.EndsWith("Result") && methods.Contains(ctd.Name.Substring(0, ctd.Name.Length - 6)))
                {
                    foreach (CodeTypeMember item in ctd.Members)
                    {
                        CodeMemberProperty prop = item as CodeMemberProperty;

                        if (prop != null && prop.CustomAttributes.Count == 0)
                        {
                            CodeAttributeDeclaration ctdAttribute = new CodeAttributeDeclaration("System.Xml.Serialization.XmlElementAttribute"
                                , new CodeAttributeArgument("Form"
                                    , new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("System.Xml.Schema.XmlSchemaForm")
                                        , "Unqualified")));
                            prop.CustomAttributes.Add(ctdAttribute);
                        }
                    }
                }
            }

        }


        /// <summary>
        /// 得到URL中的WebService名称
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <returns>如http://wwww.baidu.com/service.asmx 则返回service</returns>
        private static string GetClassName(string url, string ns)
        {
            String[] parts = url.Split('/');
            String[] pps = parts[parts.Length - 1].Split('.');
            String className = pps[0];

            if (!url.Contains(".asmx") && className.EndsWith("Soap"))
            {
                className = className.Substring(0, className.Length - 4);
            }

            return String.Format("{0}.{1}", ns, className);
        }
    }

    /// <summary>
    /// 目标服务调用代理项
    /// </summary>
    public class SoapClientItem
    {
        /// <summary>
        /// 代理程序集
        /// </summary>
        public Assembly ProxyAssembly { get; set; }

        /// <summary>
        /// 发送端口对象
        /// </summary>
        public Object PortObject { get; set; }

        /// <summary>
        /// 发送端口类型
        /// </summary>
        public Type PortType { get; set; }

        /// <summary>
        /// 请求对象
        /// </summary>
        public Type RequestType { get; set; }

        /// <summary>
        /// 获取到调用目标方法的信息
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public MethodInfo GetMethodInfo(String methodName)
        {
            if (String.IsNullOrEmpty(methodName))
                throw new Exception("方法名称必须填写！");

            if (methodName.EndsWith("ByESB"))
                methodName = methodName.Substring(0, methodName.Length - 5);

            return PortType.GetMethod(methodName);
        }

        /// <summary>
        /// 创建请求对象
        /// </summary>
        /// <returns></returns>
        public Object CreateRequestObject()
        {
            return RequestType.InvokeMember(null, BindingFlags.CreateInstance, null, null, null);
        }

        /// <summary>
        /// 设置请求对象
        /// </summary>
        public void SetReqObjProperty(Object target, String propName, Object propValue)
        {
            RequestType.InvokeMember(propName, BindingFlags.SetProperty, null, target, new Object[] { propValue });
        }
    }
}
