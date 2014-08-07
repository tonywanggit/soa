using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESB.Core;
using ESB.Core.Registry;

namespace ESBTestFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            //ESBProxy esbProxy = ESBProxy.GetInstance();

            //Console.ReadKey();

            CometClient cometClient = new CometClient("http://localhost:8080/RegistryCenter/Demo.aspx");

            cometClient.Connect();
        }
    }
}
