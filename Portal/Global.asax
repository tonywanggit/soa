<%@ Application Language="C#" %>
<script runat="server">
    void Application_Start(object sender, EventArgs e) 
    {
        ESB.Core.Monitor.MonitorCenterClient mcClient = ESB.Core.Monitor.MonitorCenterClient.GetInstance(ESB.Core.Rpc.CometClientType.Portal);
        mcClient.OnMonitorStatPublish += mcClient_OnMonitorStatPublish;
        mcClient.Connect();
    }

    /// <summary>
    /// 当收到监控中心的发布事件时通知个客户端刷新数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void mcClient_OnMonitorStatPublish(object sender, ESB.Core.Monitor.MonitorStatEventArgs e)
    {
        List<ESB.Core.Entity.ServiceMonitor> lstServiceMonitor = e.ListServiceMonitor;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (lstServiceMonitor == null || lstServiceMonitor.Count == 0)
        {
            sb.Append(@"[]");
        }
        else
        {
            sb = GetJsonString(lstServiceMonitor);
        }

        MonitorStatAsyncResult.SetAllResultComplete(sb.ToString());
    }

    /// <summary>
    /// 将监控数据转化成JSON格式
    /// </summary>
    /// <returns></returns>
    private StringBuilder GetJsonString(List<ESB.Core.Entity.ServiceMonitor> lstServiceMonitor)
    {
        StringBuilder sb = new StringBuilder("[");

        var sum = from q in lstServiceMonitor
                  group q by q.ServiceName into g
                  select new
                  {
                      ServiceName = g.Key,
                      CallNum = g.Sum(x => x.CallFailureNum + x.CallSuccessNum),
                      Tps = g.Sum(x => x.TpsPeak),
                      InBytes = g.Sum(x => x.InBytes),
                      OutBytes = g.Sum(x => x.OutBytes)
                  };
        int i = 0;
        int cnt = sum.Count();
        foreach (var item in sum)
        {
            sb.AppendFormat(@"{{""ServiceName"":""{0}"",""CallNum"":{1},""Tps"":{2},""InBytes"":{3},""OutBytes"":{4}}}",
                item.ServiceName, item.CallNum, item.Tps, item.InBytes, item.OutBytes);
            i++;

            if (i < cnt)
                sb.Append(",");
        }

        sb.Append("]");
        return sb;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
