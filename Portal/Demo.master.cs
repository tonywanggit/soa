using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using DevExpress.Web.ASPxClasses.Internal;
using DevExpress.Web.ASPxNavBar;
using DevExpress.Web.ASPxRoundPanel;
using DevExpress.Web.ASPxHeadline;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxSiteMapControl;
using DevExpress.Web.ASPxTabControl;

public partial class Demo : System.Web.UI.MasterPage {
    private bool isHomePage = false;
    private bool showTermsHeader = true;
    private string title = "";
    private string description = "";
    private string generalTerms = "";
    private string titleImageUrl = "";
    private string demoName = "";
    private CodeRender fCodeRender = new CodeRender();
    private bool showCodePages = true;

	const string HighlightedFeaturesName = "Highlighted Features";

    public bool IsHomePage {
        get { return isHomePage; }
    }
    public string TitleImageUrl {
        get { return titleImageUrl; }
    }
    public string Title {
        get { return title; }
    }
    public string Description {
        get { return description; }
    }
    public string GeneralTerms {
        get { return generalTerms; }
    }
    public string DemoName {
        get { return demoName; }
    }
    public bool ShowTermsHeader {
        get { return showTermsHeader; }
    }
    public string GroupName {
        get { return (nbMenu.SelectedItem != null) ? nbMenu.SelectedItem.Group.Name : "管理平台"; }
    }
    public string Name {
        get { return (nbMenu.SelectedItem != null) ? nbMenu.SelectedItem.Name : "企业服务总线 - 管理平台"; }
    }

    public bool ShowCodePages {
        get { return showCodePages; }
        set { showCodePages = value; }
    }

    /* Page Load */
    protected void Page_Load(object sender, EventArgs e) {
        //获取页面基页数据
        BasePage owner = (BasePage)Page;

        //菜单绑定
        nbMenu.DataBind();

        /* Title Image */
        iTitleImage.ImageUrl = TitleImageUrl;
        iTitleImage.AlternateText = GroupName + " - " + Name;

        if (String.IsNullOrEmpty(TitleImageUrl)) {
            lGroupName.Style.Add(HtmlTextWriterStyle.MarginLeft, "11px");
            iTitleImage.Visible = false;
            iTitleImage.EnableViewState = false;
        }
        
        // Header
        string currentUrl = Request.AppRelativeCurrentExecutionFilePath.ToLower();
        if (IsHomePage || Logotype.NavigateUrl.ToLower() == currentUrl)
            Logotype.NavigateUrl = "";

        // Logotype
        Logotype.ImageUrl = "~/App_Themes/" + Page.Theme + "/Demo/LogoESB.png";

        //UserName

        lblUserName.Text = "【" + owner.AuthUser.UserName + "】已登陆";

        if (phOnceContent.Controls.Count != 0)
            tdFooter.Style.Add(HtmlTextWriterStyle.PaddingLeft, "37px");

        if (!String.IsNullOrEmpty(Title))
            Page.Header.Title = Title + Page.Header.Title;

        if (phContent.Controls.Count == 0) {
            phContent.Visible = false;
            tblSourceCode.Visible = false;
            phContent.EnableViewState = false;
            tblTitle.Visible = false;
            tblTitle.EnableViewState = false;
            tblHeader.Visible = false;
            tblHeader.EnableViewState = false;
            tblFooter.Visible = false;
            tblFooter.EnableViewState = false;
            tblContent.Visible = false;
            tblContent.EnableViewState = false;
        }

        if (phOnceContent.Controls.Count == 0) {
            phOnceContent.Visible = false;
            phOnceContent.EnableViewState = false;
        }
        // general terms
        if (!string.IsNullOrEmpty(GeneralTerms)) {
            lGeneralTerms.Visible = true;
            lGeneralTerms.Text = GeneralTerms;
            //pDescription.Controls.Add(new LiteralControl(demoMaster.Description));
        }

        // SourceCode		
        if (!ShowCodePages)
            tblSourceCode.Visible = false;
        SetDescriptionTabVisibility();
        CreateCustomSourceCodeFilesTabs();
        SetCustomSourceCodePanelWidth();
        LoadActiveTabPageContent();
        pcSourceCode.TabPages.FindByName("JS").Visible = IsJSIncludeInCurrentPage();

        // Title
        lGroupName.Text = Name;// GroupName;
        //lName.Text = Name;
        if (string.IsNullOrEmpty(lName.Text)) {
            hName.Visible = false;
            hName.EnableViewState = false;
        }

        // Version
        string[] versionInfo = AssemblyInfo.Version.Split('.');
        lblVersion.Text = string.Format("v{0} vol {1}.{2}", 2000 + Int32.Parse(versionInfo[0]), versionInfo[1], versionInfo[2]);
    }

	void CreateHighlightedFeatures() {
		XmlDocument xmlDoc = BasePage.GetDemoXmlDocument(Page);

        int hfPosition = -1;
		if(xmlDoc.DocumentElement.Attributes["HighlightedFeaturesPosition"] == null ||
			!Int32.TryParse(xmlDoc.DocumentElement.Attributes["HighlightedFeaturesPosition"].Value, out hfPosition) ||
			hfPosition < 0) {
			return;
		}

		NavBarItem selectedItem = nbMenu.SelectedItem;

		NavBarGroup hfGroup = new NavBarGroup(HighlightedFeaturesName);
		BasePage owner = (BasePage)Page;
		List<NavBarItem> items = new List<NavBarItem>();
		SiteMapNode hfNode = null;
		UnboundSiteMapProvider provider = null;
		if(owner.IsSiteMapCreated) {
			provider = owner.SiteMapProvider;
			if(FindSiteMapNodeByTitle(provider, HighlightedFeaturesName) == null)
				hfNode = CreateHFSiteMapNode(provider, hfNode);
		}
		for(int i = 0; i < nbMenu.Groups.Count; i++) {
			for(int j = 0; j < nbMenu.Groups[i].Items.Count; j++) {
				if(!owner.IsHighlighted(nbMenu.Groups[i].Items[j])) continue;
				NavBarItem clone = new NavBarItem();
				clone.Text = nbMenu.Groups[i].Items[j].Text;
				clone.Name = nbMenu.Groups[i].Items[j].Name;
				clone.NavigateUrl = nbMenu.Groups[i].Items[j].NavigateUrl;

				if(GetUrl(clone.NavigateUrl).ToLower() == Request.AppRelativeCurrentExecutionFilePath.ToLower() &&
						!string.IsNullOrEmpty(Request["Highlighted"])) {
					selectedItem = clone;
				}

				items.Add(clone);
			}
		}
		for(int i = 0; i < items.Count - 1; i++)
			for(int j = i + 1; j < items.Count; j++) {
				if(owner.GetHighlightedIndex(items[j]) < owner.GetHighlightedIndex(items[i])) {
					NavBarItem item = items[j];
					items[j] = items[i];
					items[i] = item;
				}
			}
		for(int i = 0; i < items.Count; i++) {
			items[i].NavigateUrl += (items[i].NavigateUrl.Contains("?") ? "&" : "?") + "Highlighted=True";
			hfGroup.Items.Add(items[i]);
			if(hfNode != null) {
				SiteMapNode node = provider.CreateNode(items[i].NavigateUrl, items[i].Text);
				provider.AddSiteMapNode(node, hfNode);
			}
		}
		if(hfPosition < nbMenu.Groups.Count)
		    nbMenu.Groups.Insert(hfPosition, hfGroup);

        if(selectedItem != null) {
            selectedItem.Selected = true;
            selectedItem.Group.Expanded = true;
        }
        owner.EnsureSiteMapIsBound();
	}

    protected void CreateCustomSourceCodeFilesTabs() {
        BasePage owner = (BasePage)Page;
        NavBarItem selectedItem = nbMenu.SelectedItem;
        List<string> sourceCodeFiles = owner.GetSourceCodeFiles(selectedItem);

        if(sourceCodeFiles != null && sourceCodeFiles.Count > 0) {
            foreach(string curFileName in sourceCodeFiles) {
                string filePath = MapPath(curFileName);
                if(!File.Exists(filePath)) {
                    DirectoryInfo diAppPath = new DirectoryInfo(Page.Request.PhysicalApplicationPath);
                    filePath = diAppPath.Parent.FullName;
                    FileInfo fi = new FileInfo(curFileName);
                    if(fi.Extension == ".cs")
                        filePath += "\\" + "CS";
                    else if(fi.Extension == ".vb")
                        filePath += "\\" + "VB";
                    filePath += Path.GetDirectoryName(curFileName).Replace("~", "");
                    filePath += "\\" + Path.GetFileName(curFileName);
                }
                if(!string.IsNullOrEmpty(filePath) && File.Exists(filePath)) {
                    Control control = CreateTabPageCustomContentControl(filePath);
                    if (control != null) {
                        TabPage page = new TabPage(Path.GetFileName(curFileName));
                        page.Controls.Add(control);
                        pcSourceCode.TabPages.Add(page);
                    }
                }
            }
        }
    }
    protected void SetCustomSourceCodePanelWidth() {
        BasePage owner = (BasePage)Page;
        NavBarItem selectedItem = nbMenu.SelectedItem;
        Unit customWidth = owner.GetCustomSourceCodeWidth(selectedItem);
        if(!customWidth.IsEmpty)
            pcSourceCode.Width = customWidth;
    }
	private SiteMapNode CreateHFSiteMapNode(UnboundSiteMapProvider provider, SiteMapNode hfNode) {
		SiteMapNode introductionNode = FindSiteMapNodeByTitle(provider, "Introduction");
		if(introductionNode == null) return null;
		hfNode = provider.CreateNode("", HighlightedFeaturesName);
		provider.AddSiteMapNode(hfNode, introductionNode.ParentNode);
		return hfNode;
	}
	SiteMapNode FindSiteMapNodeByTitle(UnboundSiteMapProvider provider, string title) {
		foreach(SiteMapNode node in provider.GetChildNodes(provider.RootNode)) {
			if(node.Title == title) return node;
			foreach(SiteMapNode childNode in provider.GetChildNodes(node))
				if(childNode.Title == title) return childNode;
		}
		return null;
	}

		/* Source Code */
	protected void pcSourceCode_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e) {
        LoadActiveTabPageContent();
    }

    /* Skins */
    protected void cbSkins_DataBound(object sender, EventArgs e) {
        if (!IsPostBack) {
            ListEditItem item = cbSkins.Items.FindByValue(Page.Theme);
            if (item == null)
                item = cbSkins.Items.FindByValue(BasePage.DefaultThemeName);
            if (item != null)
                cbSkins.SelectedItem = item;
        }
    }

    /* Main NavBar */
    protected void nbMenu_ItemDataBound(object source, DevExpress.Web.ASPxNavBar.NavBarItemEventArgs e) {
        e.Item.Name = e.Item.Text;

        IHierarchyData itemHierarchyData = (e.Item.DataItem as IHierarchyData);
        XmlElement xmlElement = itemHierarchyData.Item as XmlElement;

        if (xmlElement.Attributes["Caption"] != null)
            e.Item.Name = xmlElement.Attributes["Caption"].Value;

        if (string.IsNullOrEmpty(DemoName)) {
            this.demoName = "ASPxperience";
            if (xmlElement.OwnerDocument.DocumentElement.Attributes["Name"] != null)
                this.demoName = xmlElement.OwnerDocument.DocumentElement.Attributes["Name"].Value;
        }

        if (GetUrl(e.Item.NavigateUrl).ToLower() == Request.AppRelativeCurrentExecutionFilePath.ToLower()) {
            if (Request.QueryString["Section"] != null) {
                if (xmlElement.Attributes["Section"] == null ||
                     Request.QueryString["Section"] != xmlElement.Attributes["Section"].Value) return;
            }
            e.Item.Selected = true;
            e.Item.Group.Expanded = true;

            XmlAttribute useFullTitle = xmlElement.Attributes["UseFullTitle"];
            if(useFullTitle != null && !bool.Parse(useFullTitle.Value)) {
                if(xmlElement.Attributes["Title"] != null)
                    this.title = xmlElement.Attributes["Title"].Value;
            } else {
                XmlNode xmlGroupNode = xmlElement.ParentNode;
                XmlNode xmlMainNode = xmlGroupNode.ParentNode;
                string titleFormatString = xmlMainNode.Attributes["TitleFormatString"] != null ? xmlMainNode.Attributes["TitleFormatString"].Value : "";
                string mainTitle = xmlMainNode.Attributes["Title"] != null ? xmlMainNode.Attributes["Title"].Value : "";
                string groupTitle = xmlGroupNode.Attributes["Title"] != null ? xmlGroupNode.Attributes["Title"].Value : "";
                string demoTitle = xmlElement.Attributes["Title"] != null ? xmlElement.Attributes["Title"].Value : "";

                if(string.IsNullOrEmpty(titleFormatString)) {
                    if(!string.IsNullOrEmpty(mainTitle)) titleFormatString = "{0}";
                    if(!string.IsNullOrEmpty(groupTitle)) titleFormatString += " - {1}";
                    if(!string.IsNullOrEmpty(demoTitle)) titleFormatString += " - {2}";
                }
                this.title = string.Format(titleFormatString, mainTitle, groupTitle, demoTitle);
            }

            foreach (XmlNode itemNode in xmlElement.ChildNodes) {
                switch (itemNode.Name) {
                    case "Description": {
                            this.description = itemNode.InnerXml;
                            break;
                        }
                    case "GeneralTerms": {
                            this.generalTerms = itemNode.InnerXml;
                            if (itemNode.Attributes["ShowHeader"] != null &&
                                itemNode.Attributes["ShowHeader"].Value.ToLower() == "false")
                                this.showTermsHeader = false;
                            break;
                        }
                }
            }

        }
    }
    protected void nbMenu_GroupDataBound(object source, NavBarGroupEventArgs e) {
        IHierarchyData hierarchyData = (e.Group.DataItem as IHierarchyData);
        XmlElement xmlElement = hierarchyData.Item as XmlElement;
        XmlAttributeCollection attributes = xmlElement.Attributes;

        if (xmlElement.Attributes["Caption"] != null)
            e.Group.Name = xmlElement.Attributes["Caption"].Value;
        else
            e.Group.Name = xmlElement.Attributes["Text"].Value;

        if (xmlElement.Attributes["Visible"] != null && xmlElement.Attributes["Visible"].Value.ToLower() == "false")
            e.Group.Visible = false;

        if (e.Group.Expanded && TitleImageUrl == "")
            this.titleImageUrl = xmlElement.Attributes["ImageUrl"] != null ? xmlElement.Attributes["ImageUrl"].Value : "";
    }
    protected string GetUrl(string url) {
        int i = url.IndexOf("?");
        if (i != -1) return url.Substring(0, i);
        return url;
    }

    /* Headline Main NavBar Template */
    protected void hlGroup_DataBinding(object sender, EventArgs e) {
		BasePage owner = Page as BasePage;
        if(owner != null)
			owner.PrepareStatusHeadlineGroups((ASPxHeadline)sender);
    }
    protected void hlItem_DataBinding(object sender, EventArgs e) {
        BasePage owner = Page as BasePage;
        if(owner != null)
			owner.PrepareStatusHeadlineItems((ASPxHeadline)sender);
    }


    // SourceCode
    protected Control CreateNotAvailableTextControl() {
        return new LiteralControl("<div class=\"cr-div-description\">File is not available</div>");
    }
    protected Control CreateTabPageContentControl(string name) {
        switch (name) {
            case "Description":
                return GetDescriptionTextControl();
            case "C#":
                return GetCSharpLanguageCodeControl();
            case "VB":
                return GetVBLanguageCodeControl();
            case "ASPX":
                return GetASPXLanguageCodeControl();
            case "JS":
                return GetJSLanguageCodeControl();
            default:
                return null;
        }
    }
    protected Control CreateTabPageCustomContentControl(string path) {
        FileInfo fi = new FileInfo(path);
        switch(fi.Extension) {
            case ".aspx":
            case ".ascx":
                return fCodeRender.GetHTMLFormattedAspxFileControl(path);
            case ".js":
                return fCodeRender.GetHTMLFormattedJSFileControl(path);
            case ".cs":
                return fCodeRender.GetHTMLFormattedCSFileControl(path);
            case ".vb":
                return fCodeRender.GetHTMLFormattedVBFileControl(path);
            default:
                return fCodeRender.GetHTMLFormattedXmlFileControl(path);
        }
    }
    protected Control GetJSLanguageCodeControl() {
        return fCodeRender.GetHTMLFormattedJSFileControl(GetPhisicalASPXFileName());
    }
    protected Control GetASPXLanguageCodeControl() {
        return fCodeRender.GetHTMLFormattedAspxFileControl(GetPhisicalASPXFileName());
    }
    protected Control GetCSharpLanguageCodeControl() {
        string codeFileName = GetCodeFileName("CS");

        if (!System.IO.File.Exists(codeFileName))
            return CreateNotAvailableTextControl();
        return fCodeRender.GetHTMLFormattedCSFileControl(codeFileName);
    }
    protected Control GetVBLanguageCodeControl() {
        string codeFileName = GetCodeFileName("VB");

        if (!System.IO.File.Exists(codeFileName))
            return CreateNotAvailableTextControl();
        return fCodeRender.GetHTMLFormattedVBFileControl(codeFileName);
    }
    protected Control GetDescriptionTextControl() {
        return fCodeRender.GetDescriptionTextControl(Description);
    }
    protected string GetRootLanguageDemo(string physicalDir, string language) {
        while (!System.IO.Directory.Exists(physicalDir + "\\" + language) &&
                 physicalDir != Path.GetPathRoot(physicalDir))
            physicalDir = System.IO.Directory.GetParent(physicalDir).ToString();
        return physicalDir + "\\" + language;
    }
    protected string GetCodeFileName(string language) {
        string physicalPath = GetPhisicalASPXFileName();

        string physicalDir = Path.GetDirectoryName(physicalPath);
        string fileExtension = language == "CS" ? ".cs" : ".vb";
        string physicalFile = Path.GetFileName(physicalPath) + fileExtension;

        string currentDirectory = System.IO.Directory.CreateDirectory(physicalDir).Name;

        if (System.IO.File.Exists(physicalDir + "\\" + physicalFile)) {
            return physicalDir + "\\" + physicalFile;
        } else {
            physicalDir = GetRootLanguageDemo(physicalDir, language);

            if (System.IO.File.Exists(physicalDir + "\\" + currentDirectory + "\\" + physicalFile)) {
                return physicalDir + "\\" + currentDirectory + "\\" + physicalFile;
            } else if (System.IO.Directory.Exists(physicalDir)) {
                foreach (string dir in System.IO.Directory.GetDirectories(physicalDir)) {
                    if (System.IO.File.Exists(dir + "\\" + currentDirectory + "\\" + physicalFile))
                        return dir + "\\" + currentDirectory + "\\" + physicalFile;
                }
            }
        }
        return "";
    }
    protected string GetPhisicalASPXFileName() {
        return MapPath(Request.AppRelativeCurrentExecutionFilePath.ToLower());
    }
    protected bool IsJSIncludeInCurrentPage() {
        return GetJSLanguageCodeControl() != null;
    }
    protected void LoadActiveTabPageContent() {
        if (pcSourceCode.ActiveTabPage.Controls.Count == 0) {
            Control contentControl = CreateTabPageContentControl(pcSourceCode.ActiveTabPage.Name);
            if (contentControl != null)
                pcSourceCode.ActiveTabPage.Controls.Add(contentControl);
        }
    }
    protected void SetDescriptionTabVisibility() {
        if (string.IsNullOrEmpty(Description)) {
            pcSourceCode.TabPages.FindByName("Description").Visible = false;
        }
    }
	protected void nbMenu_DataBound(object sender, EventArgs e) {
		CreateHighlightedFeatures();
	}
}
