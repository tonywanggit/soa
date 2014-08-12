using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESB.Core;
using ESB.Core.Registry;
using System.Diagnostics;
using System.Reflection;

namespace ESBTestFramework
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

            SeleniumUtil.getFileName();


            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ESBProxy esbProxy = ESBProxy.GetInstance();
            stopWatch.Stop();

            Console.WriteLine("ESBProxy Init 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);

            stopWatch.Restart();
            String message = esbProxy.ReceiveRequest("ESB_ASHX", "HelloWorld", "HelloWorld!");
            stopWatch.Stop();

            Console.WriteLine("第1次调用 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);

            for (int i = 0; i < 1; i++)
            {
                stopWatch.Restart();
                String ret = esbProxy.ReceiveRequest("ESB_ASHX", "HelloWorld", "HelloWorld!");
                stopWatch.Stop();

                Console.WriteLine("第{0}次调用 耗时：{1}ms。", i + 2, stopWatch.ElapsedMilliseconds);
            }

            Console.ReadKey();
        }
    }
}
