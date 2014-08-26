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
using System.Data.SqlClient;
using DevExpress.Web.ASPxGridView;
using JN.Esb.Portal.ServiceMgt.服务目录服务;
using JN.Esb.Portal.ServiceMgt.异常服务;
using JN.Esb.Portal.ServiceMgt.总线单向服务;
using JN.Esb.Portal.ServiceMgt.审计服务;
using System.Xml;
using System.Web.Services.Protocols;
using System.Collections.Generic;

public partial class Exception_ExceptionList : BasePage
{

    ESB.UddiService m_UddiService = new ESB.UddiService();

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
        //cbProvider.SelectedIndex = 0;
    }

    protected void InitRight()
    {

    }
    #endregion

    #region 自定义函数
    public string GetBindingReqBody(string exceptionID)
    {
        string retBody = "ExceptionID为空,无法寻找请求消息体！";

        if (!(String.IsNullOrEmpty(exceptionID)))
        {
            ESB.ExceptionService exceptionService = new ESB.ExceptionService();
            ESB.ExceptionCoreTb exception = exceptionService.GetExceptionByID(exceptionID);

            XmlDocument doc = new XmlDocument();
            if (!(String.IsNullOrEmpty(exception.MessageBody)))
            {
                try
                {
                    doc.LoadXml(exception.MessageBody);
                    XmlNodeList list = doc.GetElementsByTagName("消息内容");
                    if (list.Count > 0)
                    {
                        String msgContent = list[0].InnerText;
                        if (msgContent.Length > 1024000)
                            retBody = msgContent.Substring(0, 1024000) + "(剩余数据隐藏)";
                        else
                            retBody = msgContent;
                    }
                    else
                    {
                        retBody = exception.MessageBody;
                    }
                }
                catch
                {
                    retBody = exception.MessageBody;
                }
            }
            else
            {
                retBody = "请求消息体为空！";
            }
        }

        return retBody;
    }

    /// <summary>
    /// Tony : 2011-06-10
    /// 取出日志中的消息内容
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public string GetRequestMessage(string message)
    {
        string retMessage = "";

        XmlDocument doc = new XmlDocument();
        if (!(String.IsNullOrEmpty(message)))
        {
            try
            {
                doc.LoadXml(message);
                XmlNodeList list = doc.GetElementsByTagName("消息内容");
                if (list.Count > 0)
                {
                    retMessage = list[0].InnerText;
                }
                else
                {
                    retMessage = message;
                }
            }
            catch
            {
                retMessage = message;
            }
        }

        return retMessage;
    }
    #endregion

    #region 数据源接口函数

    protected void OdsException_Selecting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //if (cbProvider.Value != null)
        //{
        //    OdsException.SelectMethod = "获得分页错误消息_服务提供者_用户编码";
        //    OdsException.SelectCountMethod = "获得分页错误消息数量_服务提供者_用户编码";
        //    e.InputParameters["服务提供者编码"] = new Guid(cbProvider.Value.ToString());        
        //}

        //e.InputParameters["用户编码"] = AuthUser.UserID;

        e.InputParameters["personID"] = AuthUser.UserID;
    }

    protected void OdsException_OnUpdating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Guid ExceptionID = (Guid)e.InputParameters["异常编码"];

        错误消息服务 异常服务对象 = new 错误消息服务();
        异常信息对象 异常消息 = 异常服务对象.获得错误消息_异常编码(ExceptionID);

        注册服务目录服务 目录服务 = new 注册服务目录服务();
        服务地址 serviceAddress = 目录服务.获得绑定信息_服务地址编码(new Guid(异常消息.绑定地址编码.Value.ToString()));

        服务请求 req = new 服务请求();

        req.主机名称 = this.Server.MachineName;
        req.服务名称 = 目录服务.获得具体服务_绑定信息(serviceAddress).服务名称.Trim();
        req.方法名称 = 异常消息.方法名称.Trim();
        req.消息内容 = GetRequestMessage(异常服务对象.获得错误消息内容(ExceptionID));
        req.请求时间 = System.DateTime.Now;
        req.密码 = 异常消息.请求密码;

        //if ( string.IsNullOrEmpty(req.消息内容) )
        //{
        //    throw new Exception("重发没有成功：消息内容为空！");
        //}

        //req.错误消息编码 = ExceptionID.ToString();

        try
        {
            //Core_Service_Bus_ReProcessBus_ReProcessPort 重发端口 = new Core_Service_Bus_ReProcessBus_ReProcessPort();
            //重发端口.ReceiveRequest(req);
            Core_Service_Bus_OnewayMainBus_OneWayReceive port = new Core_Service_Bus_OnewayMainBus_OneWayReceive();
            port.ReceiveRequest(req);

        }
        catch {}

        try
        {
            AuditServcie auditService = new AuditServcie();
            auditService.ExceptionResend(new Guid(异常消息.消息编码.ToString()), new Guid(异常消息.绑定地址编码.ToString()));
        }
        catch(Exception ex) {

            throw new Exception("将审计库中的异常日志标记为重发时发生错误：" + ex.Message);
        }

    }

    #endregion

    #region 控件接口函数

    protected void grid_OnCustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
    {
        if (e.Column.FieldName != "BindingTemplateID") return;
        if (e.Value != null)
        {
            ESB.BusinessService service = m_UddiService.GetBusinessServiceByTemplateID(e.Value.ToString());
            ESB.BusinessEntity[] entityArray = m_UddiService.GetAllBusinessEntity();
            ESB.BusinessEntity entity = entityArray.First(x => x.BusinessID == service.BusinessID);

            if (e.Column.Caption == "调用服务")
            {
                e.DisplayText = service.ServiceName;

            }else if(e.Column.Caption == "调用系统")
            {
                e.DisplayText = entity.Description;
            }
        }

    }

    protected void grid_OnHtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        Control wc = grid.FindEditFormTemplateControl("UpdateButton");
        if (wc != null)
        {
            wc.Visible = AuthUser.IsSystemAdmin;
        }

    }

    protected void grid_OnCustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID != "btnPigeonhole") return;

        if (!AuthUser.IsSystemAdmin)
        {
            throw new Exception("您不是系统管理员，不能进行归档操作！");
        }
        else
        {
            //错误消息服务 异常服务对象 = new 错误消息服务();
            //异常信息对象 异常消息 = 异常服务对象.获得错误消息_异常编码(new Guid());
            //try
            //{
            //    AuditServcie auditService = new AuditServcie();
            //    auditService.ExceptionPigeonhole(new Guid(异常消息.消息编码.ToString()));
            //}
            //catch (Exception)
            //{
            //    throw new Exception("归档操作失败！");
            //}
                
            String exceptionID = grid.GetRowValues(e.VisibleIndex, "ExceptionID").ToString();
            String messageID = grid.GetRowValues(e.VisibleIndex, "MessageID").ToString();

            ESB.ExceptionService exceptionService = new ESB.ExceptionService();
            exceptionService.DeleteExceptionByID(exceptionID);

            ESB.AuditService auditService = new ESB.AuditService();
            auditService.ExceptionPigeonhole(messageID);

            grid.DataBind();
        }
    }

    protected void cbProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid.Selection.UnselectAll();
        grid.PageIndex = 0;
        grid.DataBind();
    }

    #endregion
}
