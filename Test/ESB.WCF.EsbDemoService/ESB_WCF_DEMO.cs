using ESB.Core.Adapter;
using ESB.WCF.DemoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.WCF.EsbDemoService
{
    public class ESB_WCF_DEMO : WcfHttpAdapter
    {
        Demo demoService = new Demo();

        protected override string DoEsbAction(string esbAction, string request)
        {
            switch (esbAction)
            {
                case "Echo":
                    return Echo(request);
                default:
                    throw new NotImplementedException(esbAction);
            }
        }

        private String Echo(String request)
        {
            return demoService.Echo(request);
        }
    }
}
