using ESB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_ctl_ServiceVersionTable : System.Web.UI.UserControl
{
    private ContractSerivce m_ContractSerivce = new ContractSerivce();
    private String m_BusinessID;
    protected BusinessServiceVersion[] m_BusinessServiceVersion;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_BusinessID = "All";
        if (!String.IsNullOrEmpty(Request["BusinessID"]))
        {
            m_BusinessID = Request["BusinessID"];
        }

        m_BusinessServiceVersion = m_ContractSerivce.GetPublishServiceVersion(m_BusinessID);
    }
}