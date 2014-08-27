using DevExpress.Web.ASPxGridView;
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


}
