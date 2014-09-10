using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using ESB.Core;
using ESB.Core.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Security_RegistryCenterInfo : BasePage
{
    /// <summary>
    /// 客户端代理
    /// </summary>
    ESBProxy esbProxy = ESBProxy.GetInstance();

    protected void Page_Load(object sender, EventArgs e)
    {
        HideSourceCodeTable();
        btnRefresh_Click(null, null);
    }

    protected void InitRight()
    {
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        List<RegistryClient> lstRegistClient = new List<RegistryClient>();
        try
        {
            lstRegistClient = esbProxy.RegistryConsumerClient.GetRegistryClientList();
        }
        catch (Exception)
        {
            
        }
        this.grid.DataSource = lstRegistClient;
        this.grid.DataBind();
    }
}