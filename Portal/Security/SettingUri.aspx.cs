using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Security_SettingUri : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HideSourceCodeTable();
        InitRight();
    }

    protected void InitRight()
    {
        this.btnAddAdmin.Enabled = AuthUser.IsSystemAdmin;
        this.grid.Columns[0].Visible = AuthUser.IsSystemAdmin;
    }

    void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
    {
        if (errors.ContainsKey(column)) return;
        errors[column] = errorText;
    }
    protected void grid_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
}