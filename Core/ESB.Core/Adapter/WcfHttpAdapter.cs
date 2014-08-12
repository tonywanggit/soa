using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ESB.Core.Adapter
{

    [ServiceContract(Namespace = "http://mb.com", Name = "IEsbAction")]
    public interface IEsbAction
    {
        [OperationContract(Action = "http://mb.com/EsbAction")]
        String EsbAction(String action, String request);
    }

    /// <summary>
    /// WcfHttp
    /// </summary>
    public abstract class WcfHttpAdapter : IEsbAction
    {
        public string EsbAction(String action, String request)
        {
            return DoEsbAction(action, request);
        }

        protected abstract String DoEsbAction(String esbAction, String request);
    }
}
