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
        <asp:TableRow>
            <asp:TableCell>
                <div id="real-time-gauge1" class="epoch gauge-tiny"></div>
				<div style="font-weight:bold">WXSC_WeiXinServiceForApp</div>
				<div>调用峰值：300TPS</div>
				<div>调用总数：10000</div>
				<div>流量 入：10  出：10000</div>
            </asp:TableCell>
            <asp:TableCell>
                <div id="real-time-area1" class="epoch area" style="height:160px;width:800px"></div>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <div id="real-time-gauge2" class="epoch gauge-tiny"></div>
				<div style="font-weight:bold">ERP_OrderService</div>
				<div>调用峰值：100TPS</div>
				<div>调用总数：10000</div>
				<div>流量 入：10  出：10000</div>
            </asp:TableCell>
            <asp:TableCell>
                <div id="real-time-area2" class="epoch area" style="height:160px;width:800px"></div>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <div id="Div1" class="epoch gauge-tiny"></div>
				<div style="font-weight:bold">ERP_OrderService</div>
				<div>调用峰值：100TPS</div>
				<div>调用总数：10000</div>
				<div>流量 入：10  出：10000</div>
            </asp:TableCell>
            <asp:TableCell>
                <div id="Div2" class="epoch area" style="height:160px;width:800px"></div>
            </asp:TableCell>
        </asp:TableRow>
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
		    var data = new RealTimeData(2);
		    var chartsArea = [];

		    $(".epoch.area").each(function (e) {
		        chartsArea.push($(this).epoch({
		            type: 'time.area',
		            data: data.history(),
		            axes: ['left', 'top', 'right']
		        }));
		    });


		    setInterval(function () {
		        for (var item in chartsArea) {
		            chartsArea[item].push(data.next());
		        }
		    }, 2000);

		});
	</script>
</asp:Content>

