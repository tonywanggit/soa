using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESB.Core;
using ESB.Core.Registry;
using System.Diagnostics;

namespace ESBTestFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ESBProxy esbProxy = ESBProxy.GetInstance();
            stopWatch.Stop();

            Console.WriteLine("ESBProxy Init 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);

            stopWatch.Restart();
            String message = esbProxy.ReceiveRequest("ESB_ASHX", "HelloWorld", "HelloWorld!");
            stopWatch.Stop();

            Console.WriteLine("第1次调用 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);

            for (int i = 0; i < 2; i++)
            {
                stopWatch.Restart();
                esbProxy.ReceiveRequest("ESB_ASHX", "HelloWorld", "HelloWorld!");
                stopWatch.Stop();

                Console.WriteLine("第{0}次调用 耗时：{1}ms。", i + 2, stopWatch.ElapsedMilliseconds);
            }

            Console.ReadKey();
        }
    }
}
