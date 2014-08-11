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
        }
        else
        {
            string sid = Request["SID"];
            注册服务目录服务 目录服务 = new 注册服务目录服务();
            服务 svrService = 目录服务.获得服务(new Guid(sid));

            cbProvider.Value = svrService.业务编码.ToString();
            cbService.Value = svrService.服务编码;
        }

    }

    protected void InitRight()
    {
        this.grid.Columns[0].Visible = AuthUser.IsSystemAdmin;
        //this.grid.FindControl("gridTmodel").Visible = AuthUser.IsSystemAdmin;
        //this.grid.FindDetailRowTemplateControl(0, "gridTmodel").Visible = AuthUser.IsSystemAdmin;
        this.btnAdd.Visible = AuthUser.IsSystemAdmin;
        this.grid.SettingsDetail.ShowDetailRow = AuthUser.IsSystemAdmin;
    }
    #endregion

    #region 数据源接口函数
    protected void OdsService_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        string svrID = cbProvider.Value.ToString();

        业务实体 svrEntity = new 业务实体();
        svrEntity.业务编码 = new Guid(svrID);

        e.InputParameters["服务提供者"] = svrEntity;
    }

    protected void OdsBinding_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        服务 svrService = new 服务();
        svrService.服务编码 = (cbService.Value == null) ? Guid.NewGuid() : new Guid(cbService.Value.ToString());

        if (cbService.Value == null)
            this.btnAdd.Enabled = false;
        else
            this.btnAdd.Enabled = AuthUser.IsSystemAdmin;

        e.InputParameters["具体服务单元"] = svrService;
    }

    protected void OdsBinding_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string svrID = cbService.Value.ToString();

        ((服务地址)e.InputParameters[0]).服务编码 = new Guid(svrID);
    }

    protected void OdsBinding_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string svrID = cbService.Value.ToString();

        ((服务地址)e.InputParameters[0]).服务编码 = new Guid(svrID);
    }

    protected void OdsTModel_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        服务地址 svrBinding = new 服务地址();
        svrBinding.服务地址编码 = new Guid(Session["ServiceBinding_BindingID"].ToString());

        e.InputParameters["绑定服务"] = svrBinding;
    }

    protected void OdsTModel_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string bindingID = Session["ServiceBinding_BindingID"].ToString();

        ((服务约束)e.InputParameters[0]).服务地址编码 = new Guid(bindingID);
    }

    protected void OdsTModel_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string bindingID = Session["ServiceBinding_BindingID"].ToString();

        ((服务约束)e.InputParameters[0]).服务地址编码 = new Guid(bindingID);
    }

    #endregion

    #region 控件接口函数

    protected void gridTmodel_DataSelect(object sender, EventArgs e)
    {
        Session["ServiceBinding_BindingID"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void cbProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbService.DataBind();
        cbService.SelectedIndex = 0;
    }

    protected void cbService_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid.Selection.UnselectAll();
        grid.PageIndex = 0;
        grid.DataBind();
    }

    #endregion
}
