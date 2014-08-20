using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ESB.ServiceStack.ServiceModel;
using ESB.Core;

namespace ESB.ServiceStack.ServiceInterface
{
    public class MyServices : Service
    {
        ESBProxy esbProxy = ESBProxy.GetInstance();

        public HelloResponse Any(Hello request)
        {
            String response = esbProxy.Invoke("ESB_COM_WS", "HelloAction", "Hello From ServiceStack!");
            return new HelloResponse { Result = "Hello, {0}!".Fmt(request.Name) };
        }
    }
}