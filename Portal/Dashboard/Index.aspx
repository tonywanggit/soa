<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Dashboard_Index" %>

<%@ Register Src="~/Dashboard/ctl/LeftNav.ascx" TagPrefix="uc1" TagName="LeftNav" %>
<%@ Register Src="~/Dashboard/ctl/InnerNav.ascx" TagPrefix="uc1" TagName="InnerNav" %>
<%@ Register Src="~/Dashboard/ctl/TopNav.ascx" TagPrefix="uc1" TagName="TopNav" %>
<%@ Register Src="~/Dashboard/ctl/OverviewStat.ascx" TagPrefix="uc1" TagName="OverviewStat" %>

<!DOCTYPE html>
<html lang="en">
    <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="../../favicon.ico">

    <title>MBSOA 看板</title>

    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="css/dashboard.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="css/font-awesome.min.css" rel="stylesheet">

    <!-- Just for debugging purposes. Don't actually copy these 2 lines! -->
    <!--[if lt IE 9]><script src="../../assets/js/ie8-responsive-file-warning.js"></script><![endif]-->
    <script src="js/ie-emulation-modes-warning.js"></script>

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    </head>

    <body>

    <uc1:TopNav runat="server" ID="TopNav" />

    <div class="container-fluid">
        <div class="row">
        <div class="col-sm-3 col-md-2 sidebar">
            <uc1:LeftNav runat="server" ID="LeftNav" />
        </div>
        <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
            <h1 class="page-header">看板</h1>
            
            <uc1:InnerNav runat="server" ID="InnerNav" />

            <div class="row stats-overview-cont">
				<div class="col-md-2 col-sm-4">
                    <uc1:OverviewStat runat="server" ID="osService" />
				</div>
				<div class="col-md-2 col-sm-4">
                    <uc1:OverviewStat runat="server" ID="osContract" />
				</div>
				<div class="col-md-2 col-sm-4">
                    <uc1:OverviewStat runat="server" ID="osInvoke" />
				</div>
				<div class="col-md-2 col-sm-4">
                    <uc1:OverviewStat runat="server" ID="osInvokeQueue" />
				</div>
				<div class="col-md-2 col-sm-4">
                    <uc1:OverviewStat runat="server" ID="osCache" />
				</div>
				<div class="col-md-2 col-sm-4">
                    <uc1:OverviewStat runat="server" ID="osException" />
				</div>
			</div>


            <div class="portlet">
				<div class="portlet-title">
					<div class="caption">
						<i class="fa fa-reorder"></i>服务调用情况分析
					</div>
					<div class="tools">
						<a href="javascript:;" class="reload"></a>
					</div>
				</div>
				<div class="portlet-body" style="display: block;">
					<div id="chart_2" class="chart" style="padding: 0px; position: relative;"></div>
				</div>
			</div>

            <h2 class="sub-header">服务调用排行榜</h2>
            <div class="table-responsive">
            <table class="table table-striped table-hover">
                <thead>
                <tr>
                    <th>Top10</th>
                    <th>服务提供者</th>
                    <th>服务名称</th>
                    <th>调用次数</th>
                    <th>调用次数（>200ms）</th>
                    <th>缓存命中率</th>
                </tr>
                </thead>
                <tbody>
                <tr>
                    <td>1</td>
                    <td>邦购网</td>
                    <td>BG_ORDER</td>
                    <td>100</td>
                    <th>30</th>
                    <th>40%</th>
                </tr>
                <tr>
                    <td>2</td>
                    <td>邦购网</td>
                    <td>BG_ORDER</td>
                    <td>100</td>
                    <th>30</th>
                    <th>40%</th>
                </tr>
                <tr>
                    <td>3</td>
                    <td>邦购网</td>
                    <td>BG_ORDER</td>
                    <td>100</td>
                    <th>30</th>
                    <th>40%</th>
                </tr>
                
                </tbody>
            </table>
            </div>
        </div>
        </div>
    </div>

    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/jquery.peity.min.js" type="text/javascript"></script>
    <script src="js/flot/jquery.flot.js" type="text/javascript"></script>
    <script src="js/flot/jquery.flot.resize.js" type="text/javascript"></script>
    <script src="js/flot/jquery.flot.pie.js" type="text/javascript"></script>
    <script src="js/flot/jquery.flot.stack.js" type="text/javascript"></script>
    <script src="js/flot/jquery.flot.crosshair.js" type="text/javascript"></script>

    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="js/index.js" type="text/javascript"></script>



    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="js/ie10-viewport-bug-workaround.js" type="text/javascript"></script>
    </body>
</html>
