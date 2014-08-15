using ESB.Core.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ESB.Core.Adapter
{
    /// <summary>
    /// ESB服务适配器接口
    /// </summary>
    interface IServiceAdapter
    {
        ESBTraceContext TraceContext { get; }
    }
}
