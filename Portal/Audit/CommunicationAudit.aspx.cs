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
using DevExpress.Web.ASPxGridView;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxTreeList;
using System.Drawing;
using ESB;

public partial class Audit_CommunicationAudit : BasePage
{
    private const string SESSION_AuditSearchCondition = "SESSION_AuditSearchCondition";

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
        cbExceptionType.SelectedIndex = 0;
        dateScopeBegin.Value = DateTime.Now;
        dateScopeEnd.Value = DateTime.Now;
    }

    protected void InitRight()
    {
        this.grid.Columns[0].Visible = AuthUser.IsSystemAdmin;
    }
    #endregion

    #region 数据源接口函数
    protected void OdsAuditTrace_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (grid.IsEditing)
        {
            Int32 i = grid.EditingRowVisibleIndex;
            String oid = grid.GetRowValues(i, "OID").ToString();

            e.InputParameters["traceID"] = oid;
        }
    }

    protected void OdsService_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["businessID"] = cbProvider.Value;
    }

    protected void OdsAuditBusiness_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        AuditBusinessSearchCondition condition = new AuditBusinessSearchCondition();

        condition.Status = (AuditBusinessStatus)Enum.Parse(typeof(AuditBusinessStatus), this.cbExceptionType.SelectedItem.Value.ToString());
        condition.DateScopeBegin = DateTime.Parse(DateTime.Parse(dateScopeBegin.Value.ToString()).ToString("yyyy-MM-dd") + " 00:00:00.000");
        condition.DateScopeEnd = DateTime.Parse(DateTime.Parse(dateScopeEnd.Value.ToString()).ToString("yyyy-MM-dd") + " 23:59:59.999");
        condition.HostName = this.txtHostName.Text;

        if (cbProvider.Value == null)
        {
            condition.BusinessID = String.Empty;
        }
        else
        {
            condition.BusinessID = this.cbProvider.Value.ToString();
        }

        if (cbService.SelectedItem == null)
        {
            condition.ServiceID = String.Empty;
        }
        else
        {
            condition.ServiceID = this.cbService.Value.ToString();
        }

        condition.IfShowHeartBeat = chkShowHeartBeat.Checked;
        
        e.InputParameters.Clear();
        e.InputParameters["condition"] = condition;
    }

    #endregion

    #region 控件接口函数

    protected void cbProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (IsCallback && IsPostBack)
            return;

        cbService.Value = "";
        cbService.Items.Clear();
        cbService.DataBind();
    }

    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        grid.Selection.UnselectAll();
        grid.PageIndex = 0;
        grid.DataBind();
    }

    protected void treeList_DataBinding(object sender, EventArgs e)
    {
    }

    protected void grid_OnHtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        Control pc = grid.FindEditFormTemplateControl("pageControl");
        Control tbl = grid.FindEditFormTemplateControl("tblDownload");
        Control cMB = pc.FindControl("txtMessageBody");
        Control cRMB = pc.FindControl("txtReturnMessageBody");
        ASPxHyperLink hlReq = tbl.FindControl("lnkReq") as ASPxHyperLink;
        ASPxHyperLink hlRes = tbl.FindControl("lnkRes") as ASPxHyperLink;

        if (cMB != null && cRMB != null)
        {
            ASPxMemo txtMB = cMB as ASPxMemo;
            ASPxMemo txtRMB = cRMB as ASPxMemo;

            string msgBody = "无法寻找请求消息体！";
            string retMsgBody = "无法寻找响应消息体！";
            string oid = txtMB.Text;

            try
            {
                Guid g = new Guid(oid);
                Session["TraceID"] = oid;
            }
            catch
            {
                return;
            }

            hlReq.NavigateUrl += oid;
            hlRes.NavigateUrl += oid;

            AuditService auditService = new AuditService();
            AuditBusiness auditBusiness = auditService.GetAuditBusinessByOID(oid);

            if (!(String.IsNullOrEmpty(auditBusiness.MessageBody)))
            {
                String msgContent = auditBusiness.MessageBody;
                if (msgContent.Length > 102400)
                    msgBody = msgContent.Substring(0, 102400) + "(只显示100K数据，剩余数据隐藏)";
                else
                    msgBody = msgContent;
            }
            else
            {
                msgBody = "请求消息体为空！";
            }

            if (!(String.IsNullOrEmpty(auditBusiness.ReturnMessageBody)))
            {
                String msgContent = auditBusiness.ReturnMessageBody;
                if (msgContent.Length > 102400)
                    retMsgBody = msgContent.Substring(0, 102400) + "(只显示100K数据，剩余数据隐藏)";
                else
                    retMsgBody = msgContent;
            }
            else
            {
                retMsgBody = "响应消息体为空！";
            }

            txtMB.Text = msgBody;
            txtRMB.Text = retMsgBody;
        }
    }

    /// <summary>
    /// 分析依赖
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void treeList_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
    {
        if (grid.IsEditing)
        {
            Int32 i = grid.EditingRowVisibleIndex;
            String oid = grid.GetRowValues(i, "OID").ToString();

            if (Object.Equals(e.GetValue("OID"), oid))
                e.Row.BackColor = Color.FromArgb(211, 235, 183);
        }
    }
    #endregion
}