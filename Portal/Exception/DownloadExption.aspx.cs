using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JN.Esb.Portal.ServiceMgt.审计服务;

public partial class Exception_DownloadException : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string oid = Request["ID"];
        string type = Request["Type"];
        string msgBody = "";

        AuditServcie auditService = new AuditServcie();
        AuditBusiness auditBusiness = auditService.GetAuditBusinessByOID(new Guid(oid));

        if(type == "Req")
            if (!(String.IsNullOrEmpty(auditBusiness.MessageBody)))
            {
                msgBody = auditBusiness.MessageBody;
            }
            else
            {
                msgBody = "请求消息体为空！";
            }
        else
            if (!(String.IsNullOrEmpty(auditBusiness.ReturnMessageBody)))
            {
                msgBody = auditBusiness.ReturnMessageBody;
            }
            else
            {
                msgBody = "响应消息体为空！";
            }
        string fileName = type + auditBusiness.HostName + "-" + auditBusiness.ServiceName + auditBusiness.ReqBeginTime + ".txt";

        Response.Clear();
        Response.Buffer = true;
        Response.Expires = 0;
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
        Response.Write(msgBody);
        Response.End();
    }
}
