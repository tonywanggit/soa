using ESB.Core.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace ESB.ProviderWebService.Wcf
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ESB_WCF”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 ESB_WCF.svc 或 ESB_WCF.svc.cs，然后开始调试。
    public class ESB_WCF : WcfHttpAdapter
    {
        protected override string DoEsbAction(string esbAction, string request)
        {
            //throw new FaultException("Falut");
            return String.Format("收到参数：{0}={1}。", esbAction, request);
        }
    }
}
