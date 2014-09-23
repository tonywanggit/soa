using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ESB.TestFramework.Test
{
    class RedisTest
    {
        public static void DoTest()
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            RedisClient Client = new RedisClient("192.168.56.2", 6379);
            stopWatch.Stop();

            Console.WriteLine("RedisClient Construct 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);

            Boolean connected = Client.IsSocketConnected();

            stopWatch.Restart();
            string str = Client.Get<string>("city");
            stopWatch.Stop();
            Console.WriteLine("RedisClient Get 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);

            stopWatch.Restart();
            Client.Set<String>("city", "China");
            stopWatch.Stop();

            Console.WriteLine("RedisClient Set 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);



            stopWatch.Restart();
            str = Client.Get<string>("city");
            stopWatch.Stop();
            Console.WriteLine("RedisClient Get 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);

            stopWatch.Restart();
            Boolean req = Client.Add<String>("country", "China", new TimeSpan(0, 0, 1, 0));
            stopWatch.Stop();
            Console.WriteLine("RedisClient Add 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);


            List<String> keys = Client.SearchKeys("MBSOA:*");


            Console.WriteLine("之前通过客户端进行设置的city键值对:{0}", str);
            Console.ReadLine();
        }
    }
}
