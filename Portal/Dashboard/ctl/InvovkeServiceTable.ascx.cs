using ESB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_ctl_InvovkeServiceTable : System.Web.UI.UserControl
{
    private MonitorStatService m_MonitorStatService = new MonitorStatService();
    private String m_BusinessID;
    protected ServiceMonitor[] m_ServiceMonitorList;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_BusinessID = "All";
        if (!String.IsNullOrEmpty(Request["BusinessID"]))
        {
            m_BusinessID = Request["BusinessID"];
        }

        m_ServiceMonitorList = m_MonitorStatService.GetInvokeTopService(m_BusinessID);
    }
}