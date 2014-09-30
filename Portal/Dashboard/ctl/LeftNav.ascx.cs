using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_LeftNav : NavBaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Nav = new List<NavItem>();

        if(SelectValue == "All")
            Nav.Add(new NavItem() { ID = "All", Name = "概览", Class = "active" });
        else
            Nav.Add(new NavItem() { ID = "All", Name = "概览", Class = "" });

        ESB.BusinessEntity[] lsBE = uddiService.GetAllBusinessEntity();
        foreach (ESB.BusinessEntity item in lsBE)
        {
            String navClass = item.BusinessID == SelectValue ? "active" : "";
            Nav.Add(new NavItem() { ID = item.BusinessID, Name = item.Description, Class = navClass });
        }

    }

}