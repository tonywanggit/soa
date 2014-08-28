using ESB.Core.Entity;
using ESB.Core.Monitor;
using NewLife.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Analyse_RealTimeCallAnalyse : BasePage
{
    MonitorCenterClient mcClient;

    protected void Page_Load(object sender, EventArgs e)
    {
        HideSourceCodeTable();

        ESB.UddiService uddiService = new ESB.UddiService();
        ESB.BusinessService[] lstBusinessService = uddiService.GetServiceList();

        foreach (var item in lstBusinessService)
        {
            this.statTable.Rows.Add(BuildTableRow(item.ServiceName, 0, 0, 0, 0));
        }

        mcClient = MonitorCenterClient.GetInstance(ESB.Core.Rpc.CometClientType.Portal);
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
    private void mcClient_OnMonitorStatPublish(object sender, MonitorStatEventArgs e)
    {
        List<ServiceMonitor> lstServiceMonitor = e.ListServiceMonitor;
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
    private StringBuilder GetJsonString(List<ServiceMonitor> lstServiceMonitor)
    {
        StringBuilder sb = new StringBuilder("[");

        var sum = from q in lstServiceMonitor
                  group q by q.ServiceName into g
                  select new
                  {
                      ServiceName = g.Key,
                      CallNum = g.Sum( x => x.CallFailureNum + x.CallSuccessNum ),
                      Tps = g.Sum( x => x.TpsPeak ),
                      InBytes = g.Sum( x => x.InBytes ),
                      OutBytes = g.Sum( x => x.OutBytes )
                  };
        int i = 0;
        int cnt = sum.Count();
        foreach(var item in sum)
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

    /// <summary>
    /// 构建报表HTML文件
    /// </summary>
    /// <param name="serviceName"></param>
    /// <param name="callPeak"></param>
    /// <param name="callSum"></param>
    /// <param name="inBytes"></param>
    /// <param name="outBytes"></param>
    /// <returns></returns>
    protected TableRow BuildTableRow(String serviceName, Int32 callPeak, Int32 callSum, Int32 inBytes, Int32 outBytes)
    {
        TableRow row = new TableRow();
        row.Cells.Add(new TableCell(){ ID = String.Format("td_gauge_{0}", serviceName), Text = String.Format(@"<div id=""div_gauge_{0}"" class=""epoch gauge-tiny""></div>
				<div style=""font-weight:bold"">{0}</div>
				<div>调用峰值：<span class=""esb_tps"">{1}</span>TPS</div>
				<div>调用总数：<span class=""esb_callSum"">{2}</span></div>
				<div>流量 <font color=""green"">入</font>：<span class=""esb_inBytes"" data=""0"">{3}</span><span style=""color:red;padding-left:10px;"">出</span>：<span class=""esb_outBytes"" data=""0"">{4}</span></div>"
            , serviceName, callPeak, callSum, inBytes, outBytes)
        });
        row.Cells.Add(new TableCell() { Text = String.Format(@"<div id=""div_area_{0}"" class=""epoch area"" style=""height:160px;width:800px""></div>", serviceName) });

        return row;
    }
}