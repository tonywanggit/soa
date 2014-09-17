using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxPopupControl;
using DevExpress.Web.ASPxTabControl;
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
    ESB.UddiService uddiService = new ESB.UddiService();
    ESB.ContractSerivce contractService = new ESB.ContractSerivce();

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

    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
        Session["ServiceReg_ServiceID"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void OdsService_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        string svrID = Session["UserRight_SvrID"] == null ? cbProvider.Value.ToString() : Session["UserRight_SvrID"].ToString();
        e.InputParameters["businessID"] = svrID;
    }

    protected void OdsService_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        ESB.BusinessService service = e.InputParameters["service"] as ESB.BusinessService;

        //--检测是否存在相同名字的服务
        ESB.BusinessService bs = uddiService.GetServiceByName(service.ServiceName);
        if (bs != null)
        {
            e.Cancel = true;
        }

        service.DefaultVersion = 1;
        service.Category = AuthUser.UserID; //--利用Category字段传递UserID
    }

    protected void OdsService_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
    }

    protected void OdsService_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
    }

    protected void OdsServiceVersion_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        ESB.BusinessServiceVersion version = e.InputParameters["entity"] as ESB.BusinessServiceVersion;
        version.ServiceID = Session["ServiceReg_ServiceID"].ToString();
        version.CreateDateTime = DateTime.Now;
        version.CreatePersionID = AuthUser.UserID;
    }


    protected void cbProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SvrID"] = (string)cbProvider.Value;
        grid.Selection.UnselectAll();
        grid.PageIndex = 0;
        grid.DataBind();
    }

    /// <summary>
    /// 验证服务名字是否可用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //--如果是编辑状态，并且服务名称没有发生变化，则无需验证
        if (!e.IsNewRow)
        {
            if (e.NewValues["ServiceName"].ToString() == e.OldValues["ServiceName"].ToString())
                return;
        }

        //--检测是否存在相同名字的服务
        ESB.BusinessService bs = uddiService.GetServiceByName(e.NewValues["ServiceName"].ToString());
        if (bs != null)
        {
            e.Errors.Add(grid.Columns["ServiceName"], "服务名称已经被使用了！");
        }
    }
    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["BusinessID"] = this.cbProvider.Value;
        e.NewValues["PersonalID"] = AuthUser.UserID;
    }

    /// <summary>
    /// 在删除服务前判断是否存在已经发布的版本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        String serviceID = e.Values["ServiceID"].ToString();
        ESB.BusinessServiceVersion[] svs = contractService.GetServiceVersionByServiceID(serviceID);

        //--如果服务包含发布的版本则无法删除
        if (svs.Where(x => x.Status == 2).Count() > 0)
        {
            e.Cancel = true;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), System.DateTime.Now.Ticks.ToString(), "pcAlert.Show();", true);
        }

            
    }

    protected void grdServiceConfig_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["MethodName"] = "*";
        e.NewValues["Timeout"] = 100000;
        e.NewValues["CacheDuration"] = 0;
        e.NewValues["IsAudit"] = 1;
        e.NewValues["HBPolicy"] = 1;
    }
    protected void grdServiceConfig_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxGridView gridSC = sender as ASPxGridView;
        e.NewValues["ServiceID"] = gridSC.GetMasterRowKeyValue();
    }
    protected void grdServiceConfig_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        //--如果是编辑状态，并且服务名称没有发生变化，则无需验证
        if (!e.IsNewRow)
        {
            if (e.NewValues["MethodName"].ToString() == e.OldValues["MethodName"].ToString())
                return;
        }

        ASPxGridView gridSC = sender as ASPxGridView;
        String ServiceID = gridSC.GetMasterRowKeyValue().ToString();

        //--检测是否存在相同名字的服务
        ESB.ServiceConfig[] scs = contractService.GetServiceConfig(ServiceID);
        if (scs != null && scs.Length > 0)
        {
            if(scs.Count(x=>x.MethodName == e.NewValues["MethodName"].ToString()) > 0)
                e.Errors.Add(gridSC.Columns["MethodName"], "该方法已经被配置了！");
        }
    }
}
