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
using JN.Esb.Portal.ServiceMgt.服务目录服务;

public partial class Security_UserRight : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HideSourceCodeTable();
        InitRight();

        if (!IsPostBack && !IsCallback)
        {
            InitPage();
        }

    }

    protected void InitPage()
    {
        cbProvider.SelectedIndex = 0;
    }

    protected void InitRight()
    {
        this.grid.Columns[0].Visible = AuthUser.IsSystemAdmin;
    }

    protected void OdsService_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        string svrID = Session["UserRight_SvrID"] == null ? cbProvider.Value.ToString() : Session["UserRight_SvrID"].ToString();
        e.InputParameters["businessID"] = svrID;
    }

    protected void cbProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SvrID"] = (string)cbProvider.Value;
        grid.Selection.UnselectAll();
        grid.PageIndex = 0;
        grid.DataBind();
    }

}
