using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESB.Core;

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

            String request = esbProxy.Invoke("ESB_ASHX", "MethodName", "你好，MBSOA！");

            Console.ReadKey();
        }
    }
}
