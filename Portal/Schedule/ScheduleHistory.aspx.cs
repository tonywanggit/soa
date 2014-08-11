using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DevExpress.Web.ASPxGridView;
using JN.Esb.Portal.ServiceMgt.服务目录服务;
using JN.Esb.Portal.ServiceMgt.审计服务;
using JN.Esb.Portal.ServiceMgt.调度服务;
using System.Xml;

public partial class Schedule_ScheduleHistory : BasePage
{
    #region 初始化函数
    protected void Page_Load(object sender, EventArgs e)
    {
        HideSourceCodeTable();
        InitRight();

        if (!IsCallback && !IsPostBack)
        {
            InitPage();
        }
    }

    protected void InitPage()
    {
        cbExceptionType.SelectedIndex = 0;
        dateScopeBegin.Value = DateTime.Now;
        dateScopeEnd.Value = DateTime.Now;
    }

    protected void InitRight()
    {
        this.grid.Columns[0].Visible = AuthUser.IsSystemAdmin;
    }
    #endregion

    #region 绑定函数

    #endregion

    #region 数据源接口函数
    protected void OdsSchedulerHistory_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        ScheduleHistoryCondition condition = new ScheduleHistoryCondition();
        condition.DateScopeBegin = DateTime.Parse(DateTime.Parse(dateScopeBegin.Value.ToString()).ToString("yyyy-MM-dd") + " 00:00:00");
        condition.DateScopeEnd = DateTime.Parse(DateTime.Parse(dateScopeEnd.Value.ToString()).ToString("yyyy-MM-dd") + " 23:59:59");
        condition.Status = int.Parse(cbExceptionType.SelectedItem.Value.ToString());

        if (cbProvider.Value == null)
        {
            condition.Host = Guid.Empty;
        }
        else
        {
            condition.Host = new Guid(this.cbProvider.Value.ToString());
        }

        if (cbSchedulerType.Value == null)
        {
            condition.Type = "";
        }
        else
        {
            condition.Type = this.cbSchedulerType.Value.ToString();
        }

        e.InputParameters.Clear();
        e.InputParameters["condition"] = condition;
    }

    protected void OdsHostScheduler_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        Guid host;

        if (cbProvider.Value == null)
        {
            host = Guid.Empty;
        }
        else
        {
            host = new Guid(cbProvider.Value.ToString());
        }

        e.InputParameters.Clear();
        e.InputParameters["host"] = host;
    }


    #endregion

    #region 控件接口函数

    protected void dateScopeBegin_OnDateChanged(object sender, EventArgs e)
    {
        grid.DataBind();
    }

    protected void dateScopeEnd_OnDateChanged(object sender, EventArgs e)
    {
        grid.DataBind();
    }

    protected void cbProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbHostScheduler.Value = "";
        cbHostScheduler.Items.Clear();
        cbHostScheduler.DataBind();
        grid.DataBind();
    }

    protected void cbHostScheduler_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid.Selection.UnselectAll();
        grid.PageIndex = 0;
        grid.DataBind();
    }

    protected void cbExceptionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid.Selection.UnselectAll();
        grid.PageIndex = 0;
        grid.DataBind();
    }

    protected void cbSchedulerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid.Selection.UnselectAll();
        grid.PageIndex = 0;
        grid.DataBind();
    }

    #endregion
}
