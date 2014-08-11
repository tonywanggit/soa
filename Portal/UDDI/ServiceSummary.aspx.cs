using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JN.Esb.Portal.ServiceMgt.服务目录服务;

public partial class UDDI_ServiceSummary : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HideSourceCodeTable();
        InitRight();

        if (!IsPostBack && !IsCallback)
        {
            InitPage();
            grid.GroupBy(grid.VisibleColumns[0]);
            //grid.ExpandAll();
        }
    }

    protected void InitPage()
    {
    }

    protected void InitRight()
    {
    }

    protected void OdsService_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        业务实体 svrEntity = new 业务实体();
        svrEntity.业务编码 = Guid.Empty;

        e.InputParameters["服务提供者"] = svrEntity;
    }
}
