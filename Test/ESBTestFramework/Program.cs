using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESB.Core;
using ESB.Core.Registry;
using System.Diagnostics;
using System.Reflection;

namespace ESB.TestFramework
{
    class Program
    {
        public class SeleniumUtil
        {
            public static void getFileName()
            {
                StackTrace trace = new StackTrace();
                StackFrame frame = trace.GetFrame(1);
                MethodBase method = frame.GetMethod();
                String className = method.ReflectedType.Name;
                Console.Write("ClassName:" + className + "\nMethodName:" + method.Name);
            }
        }

        static void Main(string[] args)
        {
            //SeleniumUtil.getFileName();
            //WCF.EsbActionClient port = new WCF.EsbActionClient();
            //String hello = port.EsbAction("MyAction", "HelloWorld!");

            //ESBProxy esbProxy = ESBProxy.GetInstance();
            //String message = esbProxy.ReceiveRequest("ESB_WCF", "HelloAction", "HelloBody!");

            TestEsbProxy("ESB_WCF");

            //TestEsbProxy("ESB_ASHX");

            //TestEsbProxy("ESB_WS");

            //TestWcfService();
        }

        static void TestWcfService()
        {
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Restart();
            String msgBody = new String('A', 1024 * 1);
            WCF.EsbActionClient esbClient = new WCF.EsbActionClient();
            String message = esbClient.EsbAction("HelloAction", msgBody);
            stopWatch.Stop();

            Console.WriteLine("第1次调用 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);

            for (int i = 0; i < 10; i++)
            {
                stopWatch.Restart();
                String ret = esbClient.EsbAction("HelloAction", msgBody);
                stopWatch.Stop();

                Console.WriteLine("第{0}次调用 耗时：{1}ms。", i + 2, stopWatch.ElapsedMilliseconds);
            }

            Console.ReadKey();
        }

        static void TestEsbProxy(String serviceName)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ESBProxy esbProxy = ESBProxy.GetInstance();
            stopWatch.Stop();

            Console.WriteLine("ESBProxy Init 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);;
            //Console.ReadKey();

            stopWatch.Restart();
            String msgBody = new String('A', 1024*1);
            String message = esbProxy.ReceiveRequest(serviceName, "HelloAction", msgBody);
            stopWatch.Stop();

            Console.WriteLine("第1次调用 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);

            for (int i = 0; i < 10; i++)
            {
                stopWatch.Restart();
                String ret = esbProxy.ReceiveRequest(serviceName, "HelloAction", msgBody);
                stopWatch.Stop();

                Console.WriteLine("第{0}次调用 耗时：{1}ms。", i + 1, stopWatch.ElapsedMilliseconds);
            }

            Console.ReadKey();
        }
    }
}
