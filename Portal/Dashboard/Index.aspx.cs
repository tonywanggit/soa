using ESB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_Index : System.Web.UI.Page
{
    MonitorStatService msService = new MonitorStatService();

    protected Int32 m_CallSuccessNum = 0;
    protected Int32 m_CallFailureNum = 0;
    protected Int32 m_CallHitCacheNum = 0;
    protected Int32 m_CallQueueNum = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ServiceMonitor[] lstSM = msService.GetMonitorServiceStatic();
        m_CallSuccessNum = lstSM.Sum(x => x.CallSuccessNum);
        m_CallFailureNum = lstSM.Sum(x => x.CallFailureNum);
        m_CallHitCacheNum = lstSM.Sum(x => x.CallHitCacheNum);
        m_CallQueueNum = lstSM.Sum(x => x.CallQueueNum);
    }
}