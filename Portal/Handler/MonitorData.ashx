<%@ WebHandler Language="C#" Class="MonitorData" %>

using System;
using System.Web;

public class MonitorData : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Write(GetMonitorData());
    }

    public String GetMonitorData()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append("[{ServiceName:'WXSC_WeiXinServiceForApp',CallNum:10}]");
        Random r = new Random();
        r.Next(100);

        sb.Append(@"[{""ServiceName"":""WXSC_WeiXinServiceForApp"",""CallNum"":" + r.Next(100) + @"}");
        sb.Append(@",{""ServiceName"":""ERP_Order"",""CallNum"":" + r.Next(100) + @"}]");
        return sb.ToString();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}