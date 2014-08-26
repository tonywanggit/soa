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

public partial class UDDI_ServiceReg : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitPage();
        InitRight();

        if (!IsCallback && !IsPostBack)
        {
            cbProvider.SelectedIndex = 0;
        }
    }

    protected void InitPage()
    {
        HideSourceCodeTable();
    }

    protected void InitRight()
    {
        this.grid.Columns[0].Visible = AuthUser.IsSystemAdmin;
        this.btnAdd.Visible = AuthUser.IsSystemAdmin;
    }

    protected void OdsService_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        string svrID = Session["UserRight_SvrID"] == null ? cbProvider.Value.ToString() : Session["UserRight_SvrID"].ToString();
        e.InputParameters["businessID"] = svrID;
    }

    protected void OdsService_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //个人 admin = new 个人();
        //admin.个人编码 = (Guid)e.InputParameters["个人编码"];

        //服务 service = new 服务();
        //service.服务名称 = (String)e.InputParameters["服务名称"];
        //service.服务种类 = "0";
        //service.描述 = (String)e.InputParameters["描述"];
        //service.业务编码 = (Guid)e.InputParameters["业务编码"];

        //e.InputParameters.Clear();
        //e.InputParameters["服务管理员"] = admin;
        //e.InputParameters["具体服务"] = service;
    }

    protected void OdsService_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //服务 service = new 服务();
        //service.服务名称 = (String)e.InputParameters["服务名称"];
        //service.服务种类 = "0";
        //service.描述 = (String)e.InputParameters["描述"];
        //service.业务编码 = (Guid)e.InputParameters["业务编码"];
        //service.服务编码 = (Guid)e.InputParameters["服务编码"];
        //service.个人编码 = (Guid)e.InputParameters["个人编码"];

        //e.InputParameters.Clear();
        //e.InputParameters["具体服务"] = service;
    }

    protected void OdsService_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //服务 service = new 服务();
        //service.服务编码 = (Guid)e.InputParameters["服务编码"];

        //e.InputParameters.Clear();
        //e.InputParameters["具体服务"] = service;
    }

    protected void cbProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SvrID"] = (string)cbProvider.Value;
        grid.Selection.UnselectAll();
        grid.PageIndex = 0;
        grid.DataBind();
    }


}
