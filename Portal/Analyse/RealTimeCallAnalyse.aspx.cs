using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Analyse_RealTimeCallAnalyse : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HideSourceCodeTable();

        this.statTable.Rows.Add(BuildTableRow("WXSC_WeiXinServiceForApp", 200, 1000, 10, 1000));
        this.statTable.Rows.Add(BuildTableRow("ERP_Order", 300, 5000, 300, 800));
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