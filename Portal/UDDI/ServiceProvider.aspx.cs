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

public partial class UDDI_ServiceProvider : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsCallback)
        {
            InitPage();
            InitRight();
        }
    }

    protected void InitPage()
    {
        HideSourceCodeTable();
    }

    protected void InitRight()
    {
        this.grid.Columns[0].Visible = AuthUser.IsSystemAdmin;
        this.btnAddAdmin.Enabled = AuthUser.IsSystemAdmin;
    }


}
