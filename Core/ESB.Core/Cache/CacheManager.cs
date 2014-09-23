using NewLife.Log;
using NewLife.Threading;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ESB.Core.Cache
{
    /// <summary>
    /// 缓存管理器
    /// 此类维持和Redis的关联
    /// Redis中Key的格式为：MBSOA:服务名:方法名:消息MD5
    /// </summary>
    public class CacheManager
    {
        private RedisClient m_RedisClient;
        private ESBProxy m_ESBProxy;

        /// <summary>
        /// 定时器：用于检测调用中心是否可以使用
        /// </summary>
        private TimerX m_TimerX;
        private Object m_TimerXLock = new Object();

        /// <summary>
        /// 是否连接
        /// </summary>
        private Boolean m_IsConnected = false;

        /// <summary>
        /// 失败重连次数
        /// </summary>
        private Int32 m_ReConnectNum = 0;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="esbProxy"></param>
        internal CacheManager(ESBProxy esbProxy)
        {
            m_ESBProxy = esbProxy;
        }

        /// <summary>
        /// 获取到缓存管理器的实例
        /// </summary>
        /// <returns></returns>
        public static CacheManager GetInstance()
        {
            return ESBProxy.GetInstance().CacheManager;
        }

        /// <summary>
        /// 异步调用连接
        /// </summary>
        internal void ConnectAsync()
        {
            ThreadPool.QueueUserWorkItem(x =>
            {
                Connect();
            });
        }

        private void Connect()
        {
            if (!m_IsConnected && m_ESBProxy.ESBConfig != null && m_ESBProxy.ESBConfig.Cache != null && m_ESBProxy.ESBConfig.Cache.Count > 0)
            {
                try
                {
                    String[] paramUri= m_ESBProxy.ESBConfig.Cache[0].Uri.Split(':');
                    m_RedisClient = new RedisClient(paramUri[0], Int32.Parse(paramUri[1]));

                    m_RedisClient.Set<String>(String.Format("MBSOA_Consumer:{0}:{1}", m_ESBProxy.ConsumerConfig.ApplicationName, m_ESBProxy.ClientIP)
                        , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    m_IsConnected = true;

                    XTrace.WriteLine("成功连接到缓存中心。");

                    if (m_TimerX != null)   //--说明监控中心曾经不可用过
                    {
                        m_TimerX.Dispose();
                        m_TimerX = null;
                        m_ReConnectNum = 0;
                    }
                }
                catch (Exception ex)
                {
                    m_IsConnected = false;

                    if (m_TimerX == null)
                    {
                        lock (m_TimerXLock)
                        {
                            m_TimerX = new TimerX(x => Connect(), null, 20000, 20000);
                            XTrace.WriteLine("无法连接到缓存中心：" + ex.Message);
                        }
                    }
                    else
                    {
                        XTrace.WriteLine("第{0}次重连缓存中心失败, 抛出异常：{1}", m_ReConnectNum, ex.Message);
                        m_ReConnectNum++;
                    }
                }
            }
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="duration"></param>
        internal void SetCache(String key, String value, Int32 duration)
        {
            if (m_IsConnected)
            {
                try
                {
                    m_RedisClient.Set<String>(key, value);

                    if(duration > 0)
                        m_RedisClient.Expire(key, duration);
                }
                catch (Exception)
                {
                    if (m_IsConnected)//--此处表明该线程首先发现了缓存无法使用的情况，则重新连接
                    {
                        ConnectAsync();
                    }
                    m_IsConnected = false;
                }
            }
        }

        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key"></param>
        internal String GetCache(String key)
        {
            if (m_IsConnected)
            {
                try
                {
                    return m_RedisClient.Get<String>(key);
                }
                catch (Exception)
                {
                    if (m_IsConnected)//--此处表明该线程首先发现了缓存无法使用的情况，则重新连接
                    {
                        ConnectAsync();
                    }
                    m_IsConnected = false;

                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取到缓存的统计信息
        /// </summary>
        /// <returns></returns>
        public List<CacheInfo> GetCacheStatic()
        {
            if (m_IsConnected)
            {
                Dictionary<String, CacheInfo> dictCacheInfo = new Dictionary<string, CacheInfo>();

                List<String> keys = m_RedisClient.SearchKeys("MBSOA:*");
                foreach (String item in keys)
                {
                    String[] keyArray = item.Split(":");
                    String serviceName = keyArray[1];
                    String methodName = GetMethodName(keyArray);
                    String dictKey = serviceName + "-" + methodName;

                    CacheInfo ci;
                    if (dictCacheInfo.ContainsKey(dictKey))
                        ci = dictCacheInfo[dictKey];
                    else
                    {
                        ci = new CacheInfo()
                        {
                            ServiceName = serviceName,
                            MethodName = methodName,
                            CacheKeyNum = 0
                        };
                        dictCacheInfo.Add(dictKey, ci);
                    }
                    ci.CacheKeyNum++;
                }

                return dictCacheInfo.Select(x => x.Value).ToList();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="methodName"></param>
        public void RemoveCache(String serviceName, String methodName)
        {
            if (m_IsConnected)
            {
                List<String> keys = m_RedisClient.SearchKeys(String.Format("MBSOA:{0}*", serviceName));
                foreach (String item in keys)
                {
                    if (methodName == "*")
                        m_RedisClient.Remove(item);
                    else
                    {
                        String[] keyArray = item.Split(":");
                        if (GetMethodName(keyArray) == methodName)
                        {
                            m_RedisClient.Remove(item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 从Redis键值中获取到方法名称
        /// MBSOA:ServiceName:MethodName:Key
        /// MBSOA:ServiceName:GET:JSON:MethodName:Key
        /// MBSOA:ServiceName:GET:MethodName:Key
        /// </summary>
        /// <param name="keysArray"></param>
        /// <returns></returns>
        private String GetMethodName(String[] keysArray)
        {
            if (keysArray.Length == 4)
                return keysArray[2];
            else if (keysArray.Length == 5)
                return keysArray[3];
            else if (keysArray.Length == 6)
                return keysArray[4];
            else
                return String.Empty;
        }
    }

    public class CacheInfo
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public String ServiceName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public String MethodName { get; set; }

        /// <summary>
        /// 缓存Key的数量
        /// </summary>
        public Int32 CacheKeyNum { get; set; }
    }
}
