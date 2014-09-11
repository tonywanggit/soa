using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESB.Core;
using ESB.Core.Monitor;

namespace ESB.SimpleTest
{
    /// <summary>
    /// 简单的测试调用
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ESBProxy esbProxy = ESBProxy.GetInstance();

            Console.ReadKey();

            String request = esbProxy.Invoke("BG_DUBBO", "histr", "['1=?*/&==\\/%中国2','2']");

            request = esbProxy.Invoke("BG_DUBBO", "histr", "['1=?*/&==\\/%中国2','2']");


            //RabbitMQClient mqClient = new RabbitMQClient("10.100.20.100", "admin", "osroot");


            Console.ReadKey();
        }
    }
}
