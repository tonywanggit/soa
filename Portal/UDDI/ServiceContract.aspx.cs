using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxHtmlEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UDDI_ServiceContract : BasePage
{
    ESB.ContractSerivce m_ContractSerivce = new ESB.ContractSerivce();
    ESB.UddiService m_UddiService = new ESB.UddiService();

    /// <summary>
    /// 确定评审意见文本框是否出现
    /// </summary>
    public String m_OpinionTrStyle = String.Empty;

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
            this.btnSaveVersion.Enabled = false;
            this.btnRevise.Enabled = true;
            this.btnUpdate.Enabled = true;
            this.btnDelete.Enabled = false;
            this.btnObsolete.Enabled = true;
            this.btnSetDefault.Enabled = true;
            this.cbConfirmPerson.ReadOnly = true;
            this.mmVersionDesc.ReadOnly = true;

            //--控制评审意见是否出现
            this.m_OpinionTrStyle = String.Empty;

            //--控制修订版本、升级版本两个按钮
            SetPublishVersionUI();

            grid.Columns[0].Visible = false;
        }
        else if (cbServiceVersion.Text.Contains("已废弃"))
        {
            this.btnAdd.Enabled = false;
            this.btnCommit.Enabled = false;
            this.btnCommit.Text = "提交评审";
            this.btnSaveVersion.Enabled = false;
            this.btnRevise.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.btnDelete.Enabled = false;
            this.btnObsolete.Enabled = false;
            this.btnSetDefault.Enabled = false;
            this.cbConfirmPerson.ReadOnly = true;
            this.mmVersionDesc.ReadOnly = true;

            //--控制评审意见是否出现
            this.m_OpinionTrStyle = String.Empty;

            //--控制修订版本、升级版本两个按钮
            SetPublishVersionUI();

            grid.Columns[0].Visible = false;
        }
        else if (cbServiceVersion.Text.Contains("提交评审"))
        {
            this.btnAdd.Enabled = false;
            this.btnCommit.Enabled = true;
            this.btnCommit.Text = "取消评审";
            this.btnSaveVersion.Enabled = false;
            this.btnRevise.Enabled = false;
            this.btnUpdate.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnObsolete.Enabled = false;
            this.btnSetDefault.Enabled = false;
            this.cbConfirmPerson.ReadOnly = true;
            this.mmVersionDesc.ReadOnly = true;

            //--控制评审意见是否出现
            this.m_OpinionTrStyle = "display:none;";

            grid.Columns[0].Visible = false;
        }
        else
        {
            this.btnAdd.Enabled = true;
            this.btnCommit.Enabled = true;
            this.btnCommit.Text = "提交评审";
            this.btnSaveVersion.Enabled = true;
            this.btnRevise.Enabled = false;
            this.btnUpdate.Enabled = false;
            this.btnDelete.Enabled = true;
            this.btnObsolete.Enabled = false;
            this.btnSetDefault.Enabled = false;
            this.cbConfirmPerson.ReadOnly = false;
            this.mmVersionDesc.ReadOnly = false;

            //--控制评审意见是否出现
            if (cbServiceVersion.Text.Contains("评审拒绝"))
                this.m_OpinionTrStyle = String.Empty;
            else
                this.m_OpinionTrStyle = "display:none;";


            grid.Columns[0].Visible = true;
        }
    }

    /// <summary>
    /// 当选择发布版本时，需要判断该版本下是否有编辑中、提交审核、审核失败的版本
    /// </summary>
    private void SetPublishVersionUI()
    {
        String bigVer = cbServiceVersion.Text.Split('.')[0];
        Boolean isEdit = false;
        foreach (ListEditItem item in this.cbServiceVersion.Items)
        {
            //--如果同一个大版本下有编辑中、提交审核、审核失败的版本，则不允许修订或升级版本
            if (item.Text.StartsWith(bigVer + ".") && !item.Text.Contains("已发布") && !item.Text.Contains("已废弃"))
            {
                isEdit = true;
                break;
            }
        }
        if (isEdit)
        {
            this.btnRevise.Enabled = false;
            this.btnUpdate.Enabled = false;
        }

        //--如果存在大版本的非发布版，则不允许升级版本
        foreach (ListEditItem item in this.cbServiceVersion.Items)
        {
            //--如果同一个大版本下有编辑中、提交审核、审核失败的版本，则不允许修订或升级版本
            if (item.Text.Contains(".0") && !item.Text.Contains("已发布") && !item.Text.Contains("已废弃"))
            {
                this.btnUpdate.Enabled = false;
                break;
            }
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
            this.txtOpinion.Value = version.Opinion;
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
        {
            //--提交评审前保存版本信息
            btnSaveVersion_Click(null, null);
            m_ContractSerivce.UpdateServiceVersionStatus(cbServiceVersion.Value.ToString(), 1, String.Empty);
        }
        else
            m_ContractSerivce.UpdateServiceVersionStatus(cbServiceVersion.Value.ToString(), 0, String.Empty);

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

    /// <summary>
    /// 修订版本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRevise_Click(object sender, EventArgs e)
    {
        m_ContractSerivce.ReviseServiceVersion(cbServiceVersion.Value.ToString(), AuthUser.UserID);

        this.cbServiceVersion.DataBind();
        this.cbServiceVersion.SelectedIndex = 0;
        this.Page.DataBind();
    }

    /// <summary>
    /// 升级版本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        m_ContractSerivce.UpgradeServiceVersion(cbServiceVersion.Value.ToString(), AuthUser.UserID);
        this.cbServiceVersion.DataBind();
        this.cbServiceVersion.SelectedIndex = 0;
        this.Page.DataBind();
    }

    /// <summary>
    /// 删除版本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        m_ContractSerivce.DeleteServiceVersionAndContract(cbServiceVersion.Value.ToString());
        this.cbServiceVersion.DataBind();
        this.cbServiceVersion.SelectedIndex = 0;
        this.Page.DataBind();
    }

    /// <summary>
    /// 废弃版本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnObsolete_Click(object sender, EventArgs e)
    {
        m_ContractSerivce.ObsoleteServiceVersion(cbServiceVersion.Value.ToString(), AuthUser.UserID);
        this.Page.DataBind();
    }

    /// <summary>
    /// 设置服务的默认版本
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSetDefault_Click(object sender, EventArgs e)
    {
        String version = cbServiceVersion.Text;

        m_UddiService.SetServiceDefaultVersion(cbService.Value.ToString(), Int32.Parse(version.Split('.')[0]));
    }
    #endregion
    protected void grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        e.NewValues["MethodContract"] = GetHtmlEditorText();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }

    protected string GetHtmlEditorText()
    {
        ASPxHtmlEditor htmlEditor = grid.FindEditFormTemplateControl("HtmlEditor") as ASPxHtmlEditor;

        return htmlEditor.Html;
    }
    protected void grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        e.NewValues["MethodContract"] = GetHtmlEditorText();
        e.NewValues["CreateDateTime"] = DateTime.Now;
    }
    protected void HtmlEditor_Init(object sender, EventArgs e)
    {
        ASPxHtmlEditor htmlEditor = sender as ASPxHtmlEditor;
        //htmlEditor.DesignMode = false;
        //htmlEditor.SetDefaultBooleanProperty("", DevExpress.Web.ASPxClasses.DefaultBoolean.
    }
}