using ESB.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ESB.CSConsumer
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
            Console.ReadKey();

            stopWatch.Restart();
            String message = esbProxy.Invoke("WXSC_WeiXinServiceForApp", "GET:XML:CollocationDetailFilter", "collocationId=11");
            stopWatch.Stop();

            Console.WriteLine("第1次调用 耗时：{0}ms。", stopWatch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
