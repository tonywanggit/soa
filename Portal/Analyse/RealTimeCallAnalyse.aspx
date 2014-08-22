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
    		var data = new GaugeData();
    		var chartsGauge = [];

    		$(".epoch.gauge-tiny").each(function (e) {
    		    chartsGauge.push($(this).epoch({
    		        type: 'time.gauge', domain: [0, 500], value: 500, format: function (v) {
    		            return v.toFixed(0) + " TPS";
    		        }
    		    }));
    		});

    		function updateCharts() {
    		    for (var i = 0; i < chartsGauge.length; i++) {
    		        chartsGauge[i].update(data.next() * 500);
    		    }
    		}

    		setInterval(updateCharts, 2000);
    		updateCharts();
    	});

		$(function () {
		    var chartsArea = [];

		    $(".epoch.area").each(function (e) {
		        var data = new RealTimeData(1);
		        var chart = $(this).epoch({
		            type: 'time.area',
		            data: data.history(1),
		            axes: ['left', 'bottom', 'right']
		        }); 

		        chartsArea.push({ id: $(this).attr("id"), chart: chart, data: data});
		    });;

		    setInterval(function () {
		        $.ajax({
		            type: "POST",
		            url: "../Handler/MonitorData.ashx",
		            dataType: "json",
		            success: function (msg) {
		                ProcessMonitorData(msg);
		            }
		        })
		    }, 1000);

            //--处理监控数据
		    function ProcessMonitorData(msg) {
		        for (var i = 0; i < chartsArea.length; i++) {
		            var chartArea = chartsArea[i];
		            var sName = chartArea.id;
		            var callNum = GetServiceCallNum(sName, msg);

		            console.log(sName);

		            chartArea.chart.push(chartArea.data.next(callNum));
		        }
		    }

            //--从监控数据中获取到调用次数
		    function GetServiceCallNum(serviceName, msg) {
		        for (var i = 0; i < msg.length; i++) {
		            console.log(msg[i].ServiceName);
		            if ("div_area_" + msg[i].ServiceName == serviceName) {

		                return msg[i].CallNum;
		            }
		        }
		        return 0;
		    };
		});

		
	</script>
</asp:Content>

