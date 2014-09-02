using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UDDI_ServiceReview : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitPage();
        InitRight();

        if (!IsCallback && !IsPostBack)
        {
            cbFilter.SelectedIndex = 0;
        }
    }

    protected void InitPage()
    {
        HideSourceCodeTable();
    }

    protected void InitRight()
    {
    }

    protected void detailGrid_DataSelect(object sender, EventArgs e)
    {
    }

    protected void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid.DataBind();
    }

    protected void OdsServiceVersionView_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["personalID"] = AuthUser.UserID;
        e.InputParameters["status"] = cbFilter.Value;
    }
}
