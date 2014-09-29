using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_Demo : System.Web.UI.Page
{
    public String m_Today = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_Today = DateTime.Now.ToString("yyyy年MM月dd日 星期ddd");
        m_Today = m_Today.Replace("周", "");
    }
}