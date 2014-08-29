<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="RealTimeCallAnalyse.aspx.cs" Inherits="Analyse_RealTimeCallAnalyse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="localCssPlaceholder" Runat="Server">
    <meta name="viewport" content="width=800" />
    <link rel="stylesheet" href="../css/epoch.min.css" />

    <script src="../scripts/jquery.js" type="text/javascript"></script>
    <script src="../scripts/d3.js" type="text/javascript"></script>
    <script src="../scripts/epoch.min.js" type="text/javascript"></script>
    <script src="../scripts/data.js" type="text/javascript"></script>
    <script src="../scripts/smooth.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phContent" Runat="Server">
    <asp:Table ID="statTable" runat="server" Width="900px">
    </asp:Table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="phOnceContent" Runat="Server">
    <script type="text/javascript">
        $(function () {
            var chartsAreaTps = [];
            var chartsAreaByte = [];
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

		    $(".tony.tps").each(function (e) {
		        var data = new TimeSeries({ timestampFormatter: SmoothieChart.timeFormatter });

		        var chart = new SmoothieChart();
		        chart.addTimeSeries(data, { strokeStyle: 'rgba(0, 255, 0, 1)', fillStyle: 'rgba(0, 255, 0, 0.2)', lineWidth: 2 });
		        chart.streamTo($(this)[0], 5);

		        chartsAreaTps.push({ id: $(this).attr("id"), chart: chart, data: data });
		    });

		    $(".tony.byte").each(function (e) {
		        var dataIn = new TimeSeries();
		        var dataOut = new TimeSeries();

		        var chart = new SmoothieChart();
		        chart.addTimeSeries(dataIn, { strokeStyle: 'rgba(0, 0, 255, 1)', fillStyle: 'rgba(0, 255, 0, 0.2)', lineWidth: 2 });
		        chart.addTimeSeries(dataOut, { strokeStyle: 'rgba(255, 0, 0, 1)', fillStyle: 'rgba(255, 0, 0, 0.2)', lineWidth: 2 });
		        chart.streamTo($(this)[0], 5);

		        chartsAreaByte.push({ id: $(this).attr("id"), chart: chart, dataIn: dataIn, dataOut: dataOut });
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

                if(msg.length > 0)
                    console.log(msg);

                for (var i = 0; i < chartsGauge.length; i++) {
                    var chartAreaTps = chartsAreaTps[i];
                    var chartAreaByte = chartsAreaByte[i];
		            var chartGauge = chartsGauge[i];
		            var sName = chartAreaTps.id;
		            var sm = GetServiceMonitor(sName, msg);
                    
		            if (sm) {
		                RefreshGaugeUI(chartGauge, sm);
		                chartAreaTps.data.append(new Date().getTime(), sm.CallNum);
		                chartAreaByte.dataIn.append(new Date().getTime(), sm.InBytes);
		                chartAreaByte.dataOut.append(new Date().getTime(), sm.OutBytes);

		            }
		            else {
		                RefreshGaugeUI(chartGauge, sm);
		                chartAreaTps.data.append(new Date().getTime(), 0);
		                chartAreaByte.dataIn.append(new Date().getTime(), 0);
		                chartAreaByte.dataOut.append(new Date().getTime(), 0);
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
		        //if (raw < 1024) return raw + "Byte"
		        if (raw < 1024 * 1024) return (raw / 1024).toFixed(2) + "K"
		        if (raw < 1024 * 1024 * 1024) return (raw / 1024 / 1024).toFixed(2) + "M"
		    }

            //--从监控数据中获取到监控数据
		    function GetServiceMonitor(serviceName, msg) {
		        for (var i = 0; i < msg.length; i++) {
		            if ("div_area_tps_" + msg[i].ServiceName == serviceName) {

		                return msg[i];
		            }
		        }
		        return null;
		    };
		});
		
	</script>
</asp:Content>

