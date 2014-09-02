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

public partial class UDDI_ServiceBinding : BasePage
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
        if (String.IsNullOrEmpty(Request["SID"]))
        {
            cbProvider.SelectedIndex = 0;
            cbService.SelectedIndex = 0;
            cbServiceVersion.SelectedIndex = 0;
        }
        else
        {
            string sid = Request["SID"];
            ESB.UddiService uddiService = new ESB.UddiService();
            ESB.BusinessService service = uddiService.GetServiceByID(sid);

            cbProvider.Value = service.BusinessID;
            cbService.Value = service.ServiceID;
            cbServiceVersion.SelectedIndex = 0;
        }
    }

    protected void InitRight()
    {
        this.grid.Columns[0].Visible = AuthUser.IsSystemAdmin;
        //this.grid.FindControl("gridTmodel").Visible = AuthUser.IsSystemAdmin;
        //this.grid.FindDetailRowTemplateControl(0, "gridTmodel").Visible = AuthUser.IsSystemAdmin;
        this.btnAdd.Visible = AuthUser.IsSystemAdmin;
        this.grid.SettingsDetail.ShowDetailRow = false;
    }
    #endregion

    #region 数据源接口函数
    protected void OdsService_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["businessID"] = cbProvider.Value;
    }

    protected void OdsServiceVersion_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["serviceID"] = cbService.Value;
    }

    protected void OdsBinding_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (cbService.Value == null)
            this.btnAdd.Enabled = false;
        else
            this.btnAdd.Enabled = AuthUser.IsSystemAdmin;

        e.InputParameters["serviceID"] = cbService.Value;
        e.InputParameters["version"] = cbServiceVersion.Value;
    }

    protected void OdsBinding_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
    }

    protected void OdsBinding_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        ESB.BindingTemplate template = e.InputParameters["entity"] as ESB.BindingTemplate;
        template.ServiceID = cbService.Value.ToString();
        template.Version = Int32.Parse(cbServiceVersion.Value.ToString());
    }

    #endregion

    #region 控件接口函数

    protected void cbProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbService.DataBind();
        cbService.SelectedIndex = 0;
    }

    protected void cbService_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbServiceVersion.DataBind();
        cbServiceVersion.SelectedIndex = 0;
    }

    protected void cbServiceVersion_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid.Selection.UnselectAll();
        grid.PageIndex = 0;
        grid.DataBind();
    }

    #endregion
}
