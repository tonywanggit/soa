using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESB.Core.Rpc
{
    /// <summary>
    /// 地址绑定类型
    /// </summary>
    public enum BindingType
    {
        WebService = 0,
        WCF_HTTP = 1,
        REST = 2,
        ASHX = 3
    }
}