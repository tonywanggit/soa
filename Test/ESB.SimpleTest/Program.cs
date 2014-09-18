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
            //ESBProxy esbProxy = ESBProxy.GetInstance();

            //Console.ReadKey();

            //String request = esbProxy.Invoke("BG_DUBBO", "histr", "['1=?*/&==\\/%中国2','2']");

            //request = esbProxy.Invoke("BG_DUBBO", "histr", "['1=?*/&==\\/%中国2','2']");


            ////RabbitMQClient mqClient = new RabbitMQClient("10.100.20.100", "admin", "osroot");


            //Console.ReadKey();

            TestInvokeQueue();
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

            esbProxy.InvokeQueue("ESB_QUEUE_10", "HelloWorld", "Queue");


            stopWatch.Stop();

            Console.WriteLine("第1次调用 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
