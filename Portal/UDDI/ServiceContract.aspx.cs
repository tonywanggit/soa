using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UDDI_ServiceContract : BasePage
{
    ESB.ContractSerivce m_ContractSerivce = new ESB.ContractSerivce();

    #region 初始化函数
    protected void Page_Load(object sender, EventArgs e)
    {
        HideSourceCodeTable();
        InitRight();

        if (!IsCallback && !IsPostBack)
        {
            InitPage();
        }
    }
    protected void InitPage()
    {
        if (String.IsNullOrEmpty(Request["SID"]))
        {
            cbProvider.SelectedIndex = 0;
            cbService.SelectedIndex = 0;
            cbServiceVersion.SelectedIndex = 0;
        }
        else
        {
            string sid = Request["SID"];
            ESB.UddiService uddiService = new ESB.UddiService();
            ESB.BusinessService service = uddiService.GetServiceByID(sid);

            cbProvider.Value = service.BusinessID;
            cbService.Value = service.ServiceID;
        }
    }

    protected void InitRight()
    {
    }
    #endregion

    #region 数据源接口函数
    protected void OdsService_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["businessID"] = cbProvider.Value;
    }

    protected void OdsServiceVersion_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["serviceID"] = cbService.Value;
    }

    protected void OdsServiceContract_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["serviceID"] = cbService.Value;
        e.InputParameters["versionID"] = cbServiceVersion.Value;
        e.InputParameters["status"] = 0;
    }

    protected void OdsServiceContract_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
    }

    protected void OdsServiceContract_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        ESB.ServiceContract sc = e.InputParameters["entity"] as ESB.ServiceContract;
        sc.ServiceID = cbService.Value.ToString();
        sc.ServiceVersionID = cbServiceVersion.Value.ToString();
        sc.CreateDateTime = DateTime.Now;
        sc.CreatePersonID = AuthUser.UserID;
        sc.Status = 0;
    }

    #endregion

    #region 控件接口函数

    protected void cbProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbService.DataBind();
        cbService.SelectedIndex = 0;
    }

    protected void cbService_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbServiceVersion.DataBind();
        cbServiceVersion.SelectedIndex = 0;
    }

    protected void cbServiceVersion_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid.Selection.UnselectAll();
        grid.PageIndex = 0;
        grid.DataBind();

        SetUIStatus();
    }

    /// <summary>
    /// 设置UI的状态
    /// </summary>
    /// <param name="status"></param>
    private void SetUIStatus()
    {
        SetVersionInfo();

        if (cbServiceVersion.Text.Contains("已发布"))
        {
            this.btnAdd.Enabled = false;
            this.btnCommit.Enabled = false;
            this.btnCommit.Text = "提交评审";
            this.cbConfirmPerson.ReadOnly = true;
            this.mmVersionDesc.ReadOnly = true;

            grid.Columns[0].Visible = false;
        }
        else if (cbServiceVersion.Text.Contains("提交评审"))
        {
            this.btnAdd.Enabled = false;
            this.btnCommit.Enabled = true;
            this.btnCommit.Text = "取消评审";
            this.cbConfirmPerson.ReadOnly = true;
            this.mmVersionDesc.ReadOnly = true;

            grid.Columns[0].Visible = false;
        }
        else
        {
            this.btnAdd.Enabled = true;
            this.btnCommit.Enabled = true;
            this.btnCommit.Text = "提交评审";
            this.cbConfirmPerson.ReadOnly = false;
            this.mmVersionDesc.ReadOnly = false;

            grid.Columns[0].Visible = true;
        }
    }

    /// <summary>
    /// 更新服务版本信息
    /// </summary>
    private void SetVersionInfo()
    {
        if (cbServiceVersion.Value == null) return;
        ESB.BusinessServiceVersion version = m_ContractSerivce.GetServiceVersionByID(cbServiceVersion.Value.ToString());

        if (version != null)
        {
            this.cbCreatePerson.Value = version.CreatePersionID;
            this.cbConfirmPerson.Value = version.ConfirmPersonID;
            this.deCreateDateTime.Value = version.CreateDateTime;
            this.deConfirmDateTime.Value = version.ConfirmDateTime;
            this.mmVersionDesc.Value = version.Description;
        }
    }

    /// <summary>
    /// 修改服务版本的状态：0-编辑中，1-已提交
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (this.btnCommit.Text == "提交评审")
            m_ContractSerivce.UpdateServiceVersionStatus(cbServiceVersion.Value.ToString(), 1);
        else
            m_ContractSerivce.UpdateServiceVersionStatus(cbServiceVersion.Value.ToString(), 0);

        this.Page.DataBind();
    }

    protected void cbServiceVersion_DataBound(object sender, EventArgs e)
    {
        SetUIStatus();
    }

    /// <summary>
    /// 保存版本信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveVersion_Click(object sender, EventArgs e)
    {
        m_ContractSerivce.UpdateServiceVersionInfo(cbServiceVersion.Value.ToString(), cbConfirmPerson.Value.ToString(), mmVersionDesc.Text);
    }
    #endregion
}