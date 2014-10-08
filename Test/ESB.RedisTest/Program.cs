using NewLife.Log;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ESB.RedisTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            RedisClient m_RedisClient = new RedisClient("10.100.20.108", 8888);
            stopWatch.Stop();

            Int64 cnt = 1;
            while(true)
            {
                String messageIn = new String('T', rnd.Next(1024, 1024 * 1024));
                String key = "mbsoa:soa:" + rnd.Next(1, 1000);
                Int64 messageLen = GetStringByteLength(messageIn);

                try
                {
                    stopWatch.Restart();
                    m_RedisClient.Set<String>(key, messageIn);
                    stopWatch.Stop();
                    Console.WriteLine("第{0}次调用 Set, 长度：{1}, 耗时：{2}ms。", cnt, messageLen, stopWatch.ElapsedMilliseconds);

                    if (stopWatch.ElapsedMilliseconds > 1000)
                    {
                        XTrace.WriteLine("[信息]-[次数：{0}]-[动作：SET]-[长度：{1}]-[耗时：{2}]", cnt, messageLen, stopWatch.ElapsedMilliseconds);
                    }

                    stopWatch.Restart();
                    String messageOut = m_RedisClient.Get<String>(key);
                    stopWatch.Stop();
                    Console.WriteLine("第{0}次调用 Set, 长度：{1}, 耗时：{2}ms。", cnt, messageLen, stopWatch.ElapsedMilliseconds);

                    if (stopWatch.ElapsedMilliseconds > 500)
                    {
                        XTrace.WriteLine("[信息]-[次数：{0}]-[动作：GET]-[长度：{1}]-[耗时：{2}]", cnt, messageLen, stopWatch.ElapsedMilliseconds);
                    }

                    if (messageIn == messageOut)
                    {
                        Console.WriteLine("测试成功！");
                    }
                    else
                    {
                        Console.WriteLine("测试失败！--------------------------------------------------------------");
                        XTrace.WriteLine("[错误]-[次数：{0}]-[长度：{1}]-[耗时：{2}]", cnt, messageLen, stopWatch.ElapsedMilliseconds);
                    }

                    if (cnt % 1000 == 0)
                    {
                        XTrace.WriteLine("[统计]-[执行次数：{0}]", cnt);
                    }

                    cnt++;
                }
                catch (Exception ex)
                {
                    XTrace.WriteLine("[异常]-[次数：{0}]-[长度：{1}]-[异常信息：{2}]", cnt, messageLen, ex.ToString());
                }
            }
        }

        /// <summary>
        /// 获取到消息的字节数
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Int64 GetStringByteLength(String message)
        {
            if (String.IsNullOrEmpty(message))
                return 0;
            else
                return Encoding.Default.GetByteCount(message);
        }
    }
}
