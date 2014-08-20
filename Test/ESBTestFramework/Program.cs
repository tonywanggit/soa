using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESB.Core;
using ESB.Core.Registry;
using System.Diagnostics;
using System.Reflection;
using ESB.Core.Monitor;
using System.Threading;

namespace ESB.TestFramework
{
    class Program
    {
        static void Main(string[] args)
        {

            TestEsbProxy("ESB_ServiceStack", "Hello", @"{""Name""=""Tony""}");
            //TestEsbProxy("ESB_ServiceStack", "POST:XML:Hello", @"<Hello xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://schemas.datacontract.org/2004/07/ESB.ServiceStack.ServiceModel""><Name>Ronger</Name></Hello>");
            //TestEsbProxy("WXSC_WeiXinServiceForApp", "GET:XML:CollocationDetailFilter", "collocationId=11");

            //TestEsbProxy("ESB_COM_WS");

            //TestEsbProxy("ESB_WCF");

            //TestEsbProxy("ESB_ASHX");

            //TestEsbProxy("ESB_WS");

            //TestWXSC();
        }

        static void TestEsbProxy(String serviceName, String methodName = "HelloAction", String message = null)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ESBProxy esbProxy = ESBProxy.GetInstance();
            stopWatch.Stop();

            Console.WriteLine("ESBProxy Init 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);;
            //Console.ReadKey();

            stopWatch.Restart();
            String msgBody = (message == null) ? new String('A', 1024 * 10) : message;
            String response = esbProxy.Invoke(serviceName, methodName, msgBody);
            stopWatch.Stop();

            Console.WriteLine("第1次调用 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);
            Console.ReadKey();

            Int64 elapsedMS = 0;
            for (int i = 0; i < 10; i++)
            {
                stopWatch.Restart();
                String ret = esbProxy.Invoke(serviceName, methodName, msgBody);
                stopWatch.Stop();

                elapsedMS += stopWatch.ElapsedMilliseconds;

                Console.WriteLine("第{0}次调用 耗时：{1}ms。", i + 2, stopWatch.ElapsedMilliseconds);
            }

            Console.WriteLine("排除第一次后 10 平均耗时：{0}ms。", elapsedMS / 10);

            Console.ReadKey();
        }

        static void TestWcfService()
        {
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Restart();
            String msgBody = new String('A', 1024 * 10);
            WCF.EsbActionClient esbClient = new WCF.EsbActionClient();
            String message = esbClient.EsbAction("HelloAction", msgBody);
            stopWatch.Stop();

            //Console.WriteLine("第1次调用 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);

            for (int i = 0; i < 10; i++)
            {
                stopWatch.Restart();
                String ret = esbClient.EsbAction("HelloAction", msgBody);
                stopWatch.Stop();

                //Console.WriteLine("第{0}次调用 耗时：{1}ms。", i + 2, stopWatch.ElapsedMilliseconds);
            }

            Console.ReadKey();
        }

    }


    public class SeleniumUtil
    {
        public static void getFileName()
        {
            StackTrace trace = new StackTrace();
            StackFrame frame = trace.GetFrame(1);
            MethodBase method = frame.GetMethod();
            String className = method.ReflectedType.Name;
            Console.Write("ClassName:" + className + "\nMethodName:" + method.Name);



            //SeleniumUtil.getFileName();
            //WCF.EsbActionClient port = new WCF.EsbActionClient();
            //String hello = port.EsbAction("MyAction", "HelloWorld!");

            //ESBProxy esbProxy = ESBProxy.GetInstance();
            //String message = esbProxy.ReceiveRequest("ESB_WCF", "HelloAction", "HelloBody!");

            //TestWcfService();
        }
    }
}
