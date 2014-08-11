using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _DefaultPage : BasePage {
    protected void Page_Load(object sender, EventArgs e) {

        CSSLink = "~/CSS/Default.css"; // Register css file
        if (Theme == "Youthful")
            CSSLink = "~/CSS/DefaultYouthful.css"; // Register css file

        DataBind();
        Demo demoMaster = Master as Demo;
        if (demoMaster != null) {
            lGeneralTerms.Text = demoMaster.GeneralTerms;
            pDescription.Controls.Add(new LiteralControl(demoMaster.Description));
        }


    }

    protected bool IsImageVisible(object visible) {
        if (visible != null)
            return bool.Parse(visible.ToString());
        return true;
    }
}
