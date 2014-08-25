<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="RealTimeCallAnalyse.aspx.cs" Inherits="Analyse_RealTimeCallAnalyse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="localCssPlaceholder" Runat="Server">
    <meta name="viewport" content="width=800" />
    <link rel="stylesheet" href="../css/epoch.min.css" />

    <script src="../scripts/jquery.js" type="text/javascript"></script>
    <script src="../scripts/d3.js" type="text/javascript"></script>
    <script src="../scripts/epoch.min.js" type="text/javascript"></script>
    <script src="../scripts/data.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phContent" Runat="Server">
    <asp:Table ID="statTable" runat="server" Width="900px">
    </asp:Table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="phOnceContent" Runat="Server">
    <script type="text/javascript">
		$(function () {
		    var chartsArea = [];
		    var chartsGauge = [];

		    $(".epoch.gauge-tiny").each(function (e) {
		        var data = new GaugeData();
		        var chart = $(this).epoch({
		            type: 'time.gauge', domain: [0, 500], value: 0, format: function (v) {
		                return v.toFixed(0) + " TPS";
		            }
		        });
		        chartsGauge.push({ id: $(this).attr("id"), chart: chart, data: data, elem: $(this) });
		    });

		    $(".epoch.area").each(function (e) {
		        var data = new RealTimeData(1);
		        var chart = $(this).epoch({
		            type: 'time.area',
		            data: data.history(1),
		            axes: ['left', 'bottom', 'right']
		        }); 

		        chartsArea.push({ id: $(this).attr("id"), chart: chart, data: data});
		    });

            //--长连接获取到监控中心发布的数据
		    function comin() {
		        var xmlHttp = ajaxFunction();
		        var url = "../Handler/MonitorData.ashx?sessionId=AAA";

		        xmlHttp.onreadystatechange = function () {
		            if (xmlHttp.readyState == 4) {
		                if (xmlHttp.status == 200) {
		                    ProcessMonitorData(xmlHttp.responseText);

		                    //连接已经结束，马上开启另外一个连接 
		                    comin();
		                }
		            }
		        }
		        xmlHttp.open("get", url, true);
		        xmlHttp.send(null);
		    }


            //-启动对监控数据的监控
		    comin();

            //--获取到Ajax对象
		    function ajaxFunction() {
		        var xmlHttp;
		        try {
		            xmlHttp = new XMLHttpRequest();
		        }
		        catch (e) {
		            try {
		                xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
		            }
		            catch (e) {
		                try {
		                    xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
		                }
		                catch (e) {
		                    alert("您的浏览器不支持AJAX！");
		                    return false;
		                }
		            }
		        }
		        return xmlHttp;
		    };

            //--处理监控数据
		    function ProcessMonitorData(data) {
		        var msg = eval('(' + data + ')');

		        for (var i = 0; i < chartsArea.length; i++) {
		            var chartArea = chartsArea[i];
		            var chartGauge = chartsGauge[i];
		            var sName = chartArea.id;
		            var sm = GetServiceMonitor(sName, msg);
                    
		            if (sm) {
		                RefreshGaugeUI(chartGauge, sm);
		                chartArea.chart.push(chartArea.data.next(sm.CallNum));
		            }
		            else {
		                RefreshGaugeUI(chartGauge, sm);
		                chartArea.chart.push(chartArea.data.next(0));
		            }
		        }
		    };

            //--刷新左侧图表部分
		    function RefreshGaugeUI(chartGauge, sm) {
		        if (sm == null) {
		            sm = { CallNum: 0, Tps: 0, InBytes: 0, OutBytes: 0 };
		        }

		        chartGauge.chart.update(sm.CallNum);

		        var elemParent = $(chartGauge.elem).parent();
		        var elemTps = elemParent.find("span.esb_tps").first();
		        var elemCallSum = elemParent.find("span.esb_callSum").first();
		        var elemInBytes = elemParent.find("span.esb_inBytes").first();
		        var elemOutBytes = elemParent.find("span.esb_outBytes").first();

		        if(parseInt(elemTps.text()) < sm.Tps)
		            elemTps.text(sm.Tps)

		        elemCallSum.text(parseInt(elemCallSum.text()) + sm.CallNum);

		        var inBytes = parseInt(elemInBytes.attr("data"));
		        inBytes += sm.InBytes;
		        elemInBytes.attr("data", inBytes);
		        elemInBytes.text(formatByte(inBytes));

		        var outBytes = parseInt(elemOutBytes.attr("data"));
		        outBytes += sm.OutBytes;
		        elemOutBytes.attr("data", outBytes);
		        elemOutBytes.text(formatByte(outBytes));

		    };

            //--格式化流量
		    function formatByte(raw) {
		        if (raw < 1024) return raw + "Byte"
		        if (raw < 1024 * 1024) return (raw / 1024).toFixed(2) + "K"
		        if (raw < 1024 * 1024 * 1024) return (raw / 1024 / 1024).toFixed(2) + "M"
		    }

            //--从监控数据中获取到监控数据
		    function GetServiceMonitor(serviceName, msg) {
		        for (var i = 0; i < msg.length; i++) {
		            if ("div_area_" + msg[i].ServiceName == serviceName) {

		                return msg[i];
		            }
		        }
		        return null;
		    };
		});

		
	</script>
</asp:Content>

