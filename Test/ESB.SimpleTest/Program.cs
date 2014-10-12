using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESB.Core;
using ESB.Core.Monitor;
using System.Diagnostics;

namespace ESB.SimpleTest
{
    /// <summary>
    /// 简单的测试调用
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            /*
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ESBProxy esbProxy = ESBProxy.GetInstance();
            stopWatch.Stop();

            Console.WriteLine("ESBProxy Init 耗时：{0}ms。", stopWatch.ElapsedMilliseconds); ;
            Console.ReadKey();

            stopWatch.Restart();
            String request = esbProxy.Invoke("ESB_ASHX", "MethodName", "你好，MBSOA！");
            stopWatch.Stop();

            Console.WriteLine("第1次调用 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);
            Console.ReadKey();


            Int64 elapsedMS = 0;
            for (int i = 0; i < 10; i++)
            {
                stopWatch.Restart();
                String ret = esbProxy.Invoke("ESB_ASHX", "MethodName", "你好，MBSOA！");
                stopWatch.Stop();

                elapsedMS += stopWatch.ElapsedMilliseconds;

                Console.WriteLine("第{0}次调用 耗时：{1}ms。", i + 2, stopWatch.ElapsedMilliseconds);
            }

            Console.WriteLine("排除第一次后 {0} 平均耗时：{1}ms。", 10, elapsedMS / 10);
            Console.ReadKey();
             * */
        }

        /// <summary>
        /// 测试队列化调用
        /// </summary>
        static void TestInvokeQueue()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ESBProxy esbProxy = ESBProxy.GetInstance();
            stopWatch.Stop();

            Console.WriteLine("ESBProxy Init 耗时：{0}ms。", stopWatch.ElapsedMilliseconds); ;
            Console.ReadKey();

            stopWatch.Restart();
            //esbProxy.InvokeQueue("ESB_Queue", "HelloWorld", "Tony", 0, new Core.Rpc.QueueParam()
            //{
            //    QueueName = "ERP.Order"
            //});

            //esbProxy.InvokeQueue("ESB_QUEUE_20", "HelloWorld", "Queue");

            esbProxy.Invoke("ESB_ASHX", "HelloAction", "Hello World");

            stopWatch.Stop();

            Console.WriteLine("第1次调用 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
