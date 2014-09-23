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
using ESB.TestFramework.WinForm;
using System.Windows.Forms;
using ESB.TestFramework.Test;

namespace ESB.TestFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestInvokeQueue();

            //RedisTest.DoTest();

            //MD5Test.DoTest();

            //ESBProxy esbProxy = ESBProxy.GetInstance();

            //Console.ReadKey();

            //ShowMainForm();

            //MBEmailTest.DoTest();

            //MonitorDataChartTest.DoTest();
            //MonitorCenterTest.DoTest();

            //TestEsbProxy("ESB_ServiceStack", "Hello", @"{""Name""=""Tony""}", 10, 2);

            //TestEsbProxy("ESB_ServiceStack", "POST:XML:Hello", @"<Hello xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://schemas.datacontract.org/2004/07/ESB.ServiceStack.ServiceModel""><Name>Ronger</Name></Hello>");
            //TestEsbProxy("WXSC_WeiXinServiceForApp", "GET:XML:CollocationDetailFilter", "collocationId=11", 10);

            //TestEsbProxy("ESB_COM_WS");

            //TestEsbProxy("BG_DUBBO", "GET:histr", "['中国1?=*&人','2']");
            //TestEsbProxy("BG_DUBBO", "histr", "['1=?*/&==\\/%中国2','2']");

            TestEsbProxy("ESB_ASHX", "HelloAction", "Hello World", 10);

            //TestEsbProxy("ESB_WS", "HelloAction", "Hello World", 1);

            //TestWXSC();
        }

        static void ShowMainForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
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

            esbProxy.InvokeQueue("ESB_QUEUE_20", "HelloWorld", "Queue");


            stopWatch.Stop();

            Console.WriteLine("第1次调用 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);
            Console.ReadKey();
        }

        static void TestEsbProxy(String serviceName, String methodName = "HelloAction", String message = null, int callNum = 10, Int32 version = 0)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ESBProxy esbProxy = ESBProxy.GetInstance();
            stopWatch.Stop();

            Console.WriteLine("ESBProxy Init 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);;
            Console.ReadKey();

            stopWatch.Restart();
            String msgBody = (message == null) ? new String('A', 1024 * 10) : message;

            try
            {
                String response = esbProxy.Invoke(serviceName, methodName, msgBody, version);
            }
            catch (Exception ex)
            {
                Console.WriteLine("调用总线发生异常：" + ex.Message);
            }
            stopWatch.Stop();

            Console.WriteLine("第1次调用 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);
            Console.ReadKey();

            Int64 elapsedMS = 0;
            for (int i = 0; i < callNum; i++)
            {
                stopWatch.Restart();
                String ret = esbProxy.Invoke(serviceName, methodName, msgBody, version);
                stopWatch.Stop();

                elapsedMS += stopWatch.ElapsedMilliseconds;

                Console.WriteLine("第{0}次调用 耗时：{1}ms。", i + 2, stopWatch.ElapsedMilliseconds);
            }

            Console.WriteLine("排除第一次后 {0} 平均耗时：{1}ms。", callNum, elapsedMS / callNum);

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
