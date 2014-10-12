using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;

namespace ESB.WCF.DemoService
{
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class Demo : IDemo
    {
        public string Echo(string message)
        {
            return "Echo:" + message;
        }
    }
}
