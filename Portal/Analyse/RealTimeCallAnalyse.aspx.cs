using ESB.Core.Entity;
using ESB.Core.Monitor;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Analyse_RealTimeCallAnalyse : BasePage
{
    MonitorCenterClient mcClient = MonitorCenterClient.GetInstance(ESB.Core.Rpc.CometClientType.Portal);

    protected void Page_Load(object sender, EventArgs e)
    {
        HideSourceCodeTable();

        this.statTable.Rows.Add(BuildTableRow("WXSC_WeiXinServiceForApp", 200, 1000, 10, 1000));
        this.statTable.Rows.Add(BuildTableRow("ESB_ServiceStack", 300, 5000, 300, 800));

        if (!mcClient.IsSubscribe)
        {
            mcClient.OnMonitorStatPublish += mcClient_OnMonitorStatPublish;
        }
    }

    /// <summary>
    /// 当收到监控中心的发布事件时通知个客户端刷新数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void mcClient_OnMonitorStatPublish(object sender, MonitorStatEventArgs e)
    {
        List<ServiceMonitor> lstServiceMonitor = e.ListServiceMonitor;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        Random r = new Random();
        r.Next(100);

        if (lstServiceMonitor == null || lstServiceMonitor.Count == 0)
        {
            sb.Append(@"[{""ServiceName"":""WXSC_WeiXinServiceForApp"",""CallNum"":" + r.Next(100) + @"}");
            sb.Append(@",{""ServiceName"":""ESB_ServiceStack"",""CallNum"":" + 0 + @"}]");
        }
        else
        {
            int ssCall = lstServiceMonitor.Sum(x =>
            {
                if (x.ServiceName == "ESB_ServiceStack")
                    return x.CallSuccessNum + x.CallFailureNum;
                else
                    return 0;
            });

            sb.Append(@"[{""ServiceName"":""WXSC_WeiXinServiceForApp"",""CallNum"":" + r.Next(100) + @"}");
            sb.Append(@",{""ServiceName"":""ESB_ServiceStack"",""CallNum"":" + ssCall + @"}]");
        }

        MonitorStatAsyncResult.SetAllResultComplete(sb.ToString());
    }

    protected TableRow BuildTableRow(String serviceName, Int32 callPeak, Int32 callSum, Int32 inBytes, Int32 outBytes)
    {
        TableRow row = new TableRow();
        row.Cells.Add(new TableCell(){ Text = String.Format(@"<div id=""div_gauge_{0}"" class=""epoch gauge-tiny""></div>
				<div style=""font-weight:bold"">{0}</div>
				<div>调用峰值：{1}TPS</div>
				<div>调用总数：{2}</div>
				<div>流量 <font color=""green"">入</font>：{3}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font color=""red"">出</font>：{4}</div>"
            , serviceName, callPeak, callSum, inBytes, outBytes)
        });
        row.Cells.Add(new TableCell() { Text = String.Format(@"<div id=""div_area_{0}"" class=""epoch area"" style=""height:160px;width:800px""></div>", serviceName) });

        return row;
    }
}