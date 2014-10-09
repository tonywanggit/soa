using ESB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_Demo : System.Web.UI.Page
{
    protected String m_ServiceName;
    protected String m_MethodName;
    protected String m_BusinessID;

    private ContractSerivce m_ContractSerivce = new ContractSerivce();
    protected BusinessServiceVersion[] m_BusinessServiceVersion;

    protected ESB.UddiService m_UddiService = new ESB.UddiService();

    protected void Page_Load(object sender, EventArgs e)
    {
        m_BusinessID = "All";
        if (!String.IsNullOrEmpty(Request["BusinessID"]))
        {
            m_BusinessID = Request["BusinessID"];
        }

        if (m_BusinessID == "All")
        {
            m_ServiceName = "MBSOA_Demo";
            m_MethodName = "Echo";
        }
        else
        {
            m_BusinessServiceVersion = m_ContractSerivce.GetPublishServiceVersion(m_BusinessID);
            if (m_BusinessServiceVersion.Length > 0)
                m_ServiceName = m_BusinessServiceVersion[0].ServiceName;
        }
    }
}