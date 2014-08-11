using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using DevExpress.Web;
using DevExpress.Web.ASPxRoundPanel;
using DevExpress.Web.ASPxSiteMapControl;
using DevExpress.Web.ASPxSiteMapControl.Internal;
using DevExpress.Web.ASPxHeadline;

public partial class _SiteMap: BasePage {
    protected void Page_Load(object sender, EventArgs e) {
        Control tblSourceCode = Page.Master.FindControl("tblSourceCode") as Control;
        if (tblSourceCode != null)
            tblSourceCode.Visible = false;

		if(!IsPostBack) {
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(Page.MapPath("~/App_Data/Demos.xml"));

			byte columnCount = 3;
			if(xmlDoc.DocumentElement["ColumnCount"] != null &&
					byte.TryParse(xmlDoc.DocumentElement["ColumnCount"].Value, out columnCount))
				ASPxSiteMapControl1.ColumnCount = Convert.ToByte(columnCount);
		}		

		ASPxSiteMapDataSource1.Provider = SiteMapProvider;
        ASPxSiteMapControl1.DataBind();
    }
	public override void EnsureSiteMapIsBound() {
		base.EnsureSiteMapIsBound();
		ASPxSiteMapControl1.DataBind();
	}

    /* Headline Main NavBar Template */
    protected void hlItem_DataBinding(object sender, EventArgs e) {
		PrepareStatusHeadlineGroups((ASPxHeadline)sender);
		PrepareStatusHeadlineItems((ASPxHeadline)sender);
        
        ASPxHeadline hl = sender as ASPxHeadline;
        if (hl != null && string.IsNullOrEmpty(hl.TailImage.Url)) {
            hl.TailImage.Url = "spacer.gif";
            hl.TailImage.Width = Unit.Pixel(0);
            hl.TailImage.Height = Unit.Pixel(0);
            hl.CssClass = "PageStatus ShowInline PageStatusHideImage";
        }
    }
    protected bool IsFirstLevel(object container) {
        bool result = false;
        DevExpress.Web.ASPxSiteMapControl.NodeTemplateContainer nodeContainer = (container as DevExpress.Web.ASPxSiteMapControl.NodeTemplateContainer);
        if (nodeContainer.SiteMapNode.ParentNode.ParentNode == null ||
                nodeContainer.SiteMapNode.ParentNode.ParentNode.ParentNode == null)
            result = true;
        return result;
    }
}
