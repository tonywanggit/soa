<%@ WebHandler Language="C#" Class="MonitorData" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;

public class MonitorData : IHttpAsyncHandler {
    
    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write(GetMonitorData());
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
            return true;
        }
    }


    public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
    {            
        //不让客户端缓存
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //获取唯一标识
        string sessionId = context.Request.QueryString["sessionId"];

        List<MonitorStatAsyncResult> queueResult = MonitorStatAsyncResult.Queue;

        if (queueResult.Count(fun => fun.SessionId == sessionId) > 0)
        {
            int index = queueResult.IndexOf(queueResult.Find(fun => fun.SessionId == sessionId));
            queueResult[index].Context = context;
            queueResult[index].CallBack = cb;
            return queueResult[index];
        }

        //如果不存在则加入队列
        MonitorStatAsyncResult asyncResult = new MonitorStatAsyncResult(context, cb, sessionId);
        queueResult.Add(asyncResult);
        return asyncResult;
    }

    public void EndProcessRequest(IAsyncResult result)
    {
        //长连接结束前写入内容
        MonitorStatAsyncResult rslt = (MonitorStatAsyncResult)result;

        //拼装返回内容
        rslt.Context.Response.Write(rslt.Message);
        rslt.Message = string.Empty;
    }
}