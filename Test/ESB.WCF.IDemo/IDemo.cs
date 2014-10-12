using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ESB.WCF
{
    [ServiceContract]
    public interface IDemo
    {
        [OperationContract]
        String Echo(String message);
    }
}
