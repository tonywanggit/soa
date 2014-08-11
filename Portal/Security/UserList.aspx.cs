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
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.Data;
using DevExpress.Web.ASPxGridView;
using System.Collections.Generic;

public partial class Security_UserList : BasePage
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

    protected void grid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        //ASPxPageControl pageControl = grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        //ASPxMemo memo = pageControl.FindControl("notesEditor") as ASPxMemo;
        //e.NewValues["Notes"] = memo.Text;
        //e.Cancel = true;
    }

    protected void grid_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        // Checks for null values.
        foreach (GridViewColumn column in grid.Columns)
        {
            GridViewDataColumn dataColumn = column as GridViewDataColumn;
            if (dataColumn == null) continue;
            if (e.NewValues[dataColumn.FieldName] == null)
                e.Errors[dataColumn] = "该字段不能为空.";
        }

        // Displays the error row if there is at least one error.
        if (e.Errors.Count > 0) e.RowError = "请填完所有字段.";

        //if (e.NewValues["ContactName"] != null && e.NewValues["ContactName"].ToString().Length < 2)
        //{
        //    AddError(e.Errors, grid.Columns["ContactName"], "Contact Name must be at least two characters long.");
        //}
        //if (e.NewValues["CompanyName"] != null && e.NewValues["CompanyName"].ToString().Length < 2)
        //{
        //    AddError(e.Errors, grid.Columns["CompanyName"], "Company Name must be at least two characters long.");
        //}

        if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0) e.RowError = "请修正所有问题.";
    }

    void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
    {
        if (errors.ContainsKey(column)) return;
        errors[column] = errorText;
    }

}
