using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_ctl_InnerNav : NavBaseControl{

    /// <summary>
    /// 当前页面名称
    /// </summary>
    protected String PageName
    {
        get
        {
            if (CurrentPageName == "Index.aspx")
                return "看板";
            else
                return "演示";
        }
    }

    /// <summary>
    /// 下一个页面的Url
    /// </summary>
    protected String PageUrl1
    {
        get
        {
            if (CurrentPageName == "Index.aspx")
                return "Demo.aspx";
            else
                return "Index.aspx";
        }
    }

    /// <summary>
    /// 下一个页面的名称
    /// </summary>
    protected String PageName1
    {
        get
        {
            if (CurrentPageName == "Index.aspx")
                return "演示";
            else
                return "看板";
        }
    }

    /// <summary>
    /// 实体名称
    /// </summary>
    protected String BusinessName
    {
        get;
        set;
    }

    private String m_Today;
    /// <summary>
    /// 当天的日期
    /// </summary>
    protected String Today
    {
        get
        {
            if (String.IsNullOrEmpty(m_Today))
            {
                m_Today = DateTime.Now.ToString("yyyy年MM月dd日 星期ddd").Replace("周", "");
            }
            return m_Today;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Nav = new List<NavItem>();

        if (SelectValue == "All")
        {
            BusinessName = "概览";
        }
        else
        {
            Nav.Add(new NavItem() { ID = "All", Name = "概览", Class = "active" });
        }

        ESB.BusinessEntity[] lsBE = uddiService.GetAllBusinessEntity();
        foreach (ESB.BusinessEntity item in lsBE)
        {
            if (item.BusinessID == SelectValue)
                BusinessName = item.Description;
            else
                Nav.Add(new NavItem() { ID = item.BusinessID, Name = item.Description, Class = "" });
        }
    }
}