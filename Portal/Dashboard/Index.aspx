<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Dashboard_Index" %>

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

    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container-fluid">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="#" style="color:white">MBSOA</a>
        </div>
        <div id="navbar" class="navbar-collapse collapse">
            <ul class="nav navbar-nav navbar-right">
                <li><a href="#">看板</a></li>
                <li><a href="Demo.aspx">演示</a></li>
                <li><a href="../Default.aspx">管理</a></li>
            </ul>
        </div>
        </div>
    </nav>

    <div class="container-fluid">
        <div class="row">
        <div class="col-sm-3 col-md-2 sidebar">
            <ul class="nav nav-sidebar">
            <li class="active"><a href="#">概览</a></li>
            <li><a href="#">微信商城</a></li>
            <li><a href="#">企业服务总线</a></li>
            <li><a href="#">邦购网</a></li>
            </ul>
        </div>
        <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
            <h1 class="page-header">看板</h1>
          
            
            <nav class="navbar navbar-default" role="navigation">
                <div class="container-fluid">
                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="#"><span class="glyphicon glyphicon-home"></span></a>
                </div>

                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav">
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown"> 看板 <span class="caret"></span></a>
                        <ul class="dropdown-menu" role="menu">
                            <li><a href="Demo.aspx"> 演示 </a></li>
                        </ul>
                    </li>
                    <li class="dropdown active">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown"> 概览 <span class="caret"></span></a>
                        <ul class="dropdown-menu" role="menu">
                        <li><a href="#">微信商城</a></li>
                        <li><a href="#">邦购网</a></li>
                        <li><a href="#">企业服务总线</a></li>
                        </ul>
                    </li>
                    </ul>
                    <form class="navbar-form navbar-left" role="search">
                    <div class="form-group">
                        <div class="input-group">
                          <input type="text" class="form-control" placeholder="请输入服务名称">
                          <span class="input-group-btn">
                            <button class="btn btn-primary" type="button">查找</button>
                          </span>
                        </div><!-- /input-group -->
                    </div>
                    </form>
                    <ul class="nav navbar-nav navbar-right">
                        <li><a href="#"><% Response.Write(m_Today); %></a></li>
                    </ul>
                </div><!-- /.navbar-collapse -->
                </div><!-- /.container-fluid -->
            </nav>

            <div class="row stats-overview-cont">
				<div class="col-md-2 col-sm-4">
					<div class="stats-overview stat-block">
						<div class="display stat ok huge">
							<span class="line-chart" style="display: inline;"><span style="display: none;"><span style="display: none;"><span style="display: none;">
							5, 6, 7, 11, 14, 10, 15, 19, 15, 2 </span><canvas width="50" height="20"></canvas></span><canvas width="50" height="20"></canvas></span><canvas width="40" height="20"></canvas></span>
							<div class="percent">
								 +66%
							</div>
						</div>
						<div class="details">
							<div class="title">Service</div>
							<div class="numbers">4</div>
						</div>
						<div class="progress">
							<span style="width: 40%;" class="progress-bar progress-bar-info" aria-valuenow="66" aria-valuemin="0" aria-valuemax="100">
							<span class="sr-only">
							66% Complete </span>
							</span>
						</div>
					</div>
				</div>
				<div class="col-md-2 col-sm-4">
					<div class="stats-overview stat-block">
						<div class="display stat good huge">
							<span class="line-chart" style="display: inline;"><span style="display: none;"><span style="display: none;"><span style="display: none;">
							2,6,8,12, 11, 15, 16, 11, 16, 11, 10, 3, 7, 8, 12, 19 </span><canvas width="50" height="20"></canvas></span><canvas width="50" height="20"></canvas></span><canvas width="40" height="20"></canvas></span>
							<div class="percent">
								 +16%
							</div>
						</div>
						<div class="details">
							<div class="title">
								 Contract
							</div>
							<div class="numbers">
								 100
							</div>
							<div class="progress">
								<span style="width: 16%;" class="progress-bar progress-bar-warning" aria-valuenow="16" aria-valuemin="0" aria-valuemax="100">
								<span class="sr-only">
								16% Complete </span>
								</span>
							</div>
						</div>
					</div>
				</div>
				<div class="col-md-2 col-sm-4">
					<div class="stats-overview stat-block">
						<div class="display stat bad huge">
							<span class="line-chart" style="display: inline;"><span style="display: none;"><span style="display: none;"><span style="display: none;">
							2,6,8,11, 14, 11, 12, 13, 15, 12, 9, 5, 11, 12, 15, 9,3 </span><canvas width="50" height="20"></canvas></span><canvas width="50" height="20"></canvas></span><canvas width="40" height="20"></canvas></span>
							<div class="percent">
								 +6%
							</div>
						</div>
						<div class="details">
							<div class="title">
								 Invoke
							</div>
							<div class="numbers">
								 509
							</div>
							<div class="progress">
								<span style="width: 16%;" class="progress-bar progress-bar-success" aria-valuenow="16" aria-valuemin="0" aria-valuemax="100">
								<span class="sr-only">
								16% Complete </span>
								</span>
							</div>
						</div>
					</div>
				</div>
				<div class="col-md-2 col-sm-4">
					<div class="stats-overview stat-block">
						<div class="display stat good huge">
							<span class="bar-chart" style="display: inline;"><span style="display: none;"><span style="display: none;"><span style="display: none;">
							1,4,9,12, 10, 11, 12, 15, 12, 11, 9, 12, 15, 19, 14, 13, 15 </span><canvas width="50" height="20"></canvas></span><canvas width="50" height="20"></canvas></span><canvas width="40" height="20"></canvas></span>
							<div class="percent">
								 +86%
							</div>
						</div>
						<div class="details">
							<div class="title" style="font-size:13px">
								 InvokeQueue
							</div>
							<div class="numbers">
								 1550
							</div>
							<div class="progress">
								<span style="width: 56%;" class="progress-bar progress-bar-warning" aria-valuenow="56" aria-valuemin="0" aria-valuemax="100">
								<span class="sr-only">
								56% Complete </span>
								</span>
							</div>
						</div>
					</div>
				</div>
				<div class="col-md-2 col-sm-4">
					<div class="stats-overview stat-block">
						<div class="display stat ok huge">
							<span class="line-chart" style="display: inline;"><span style="display: none;"><span style="display: none;"><span style="display: none;">
							2,6,8,12, 11, 15, 16, 17, 14, 12, 10, 8, 10, 2, 4, 12, 19 </span><canvas width="50" height="20"></canvas></span><canvas width="50" height="20"></canvas></span><canvas width="40" height="20"></canvas></span>
							<div class="percent">
								 +72%
							</div>
						</div>
						<div class="details">
							<div class="title">
								 CacheHit
							</div>
							<div class="numbers">
								 9600
							</div>
							<div class="progress">
								<span style="width: 72%;" class="progress-bar progress-bar-danger" aria-valuenow="72" aria-valuemin="0" aria-valuemax="100">
								<span class="sr-only">
								72% Complete </span>
								</span>
							</div>
						</div>
					</div>
				</div>
				<div class="col-md-2 col-sm-4">
					<div class="stats-overview stat-block">
						<div class="display stat bad huge">
							<span class="line-chart" style="display: inline;"><span style="display: none;"><span style="display: none;"><span style="display: none;">
							1,7,9,11, 14, 12, 6, 7, 4, 2, 9, 8, 11, 12, 14, 12, 10 </span><canvas width="50" height="20"></canvas></span><canvas width="50" height="20"></canvas></span><canvas width="40" height="20"></canvas></span>
							<div class="percent">
								 +15%
							</div>
						</div>
						<div class="details">
							<div class="title">
								 Exception
							</div>
							<div class="numbers">
								 2090
							</div>
							<div class="progress">
								<span style="width: 15%;" class="progress-bar progress-bar-success" aria-valuenow="15" aria-valuemin="0" aria-valuemax="100">
								<span class="sr-only">
								15% Complete </span>
								</span>
							</div>
						</div>
					</div>
				</div>
			</div>


            <div class="portlet">
				<div class="portlet-title">
					<div class="caption">
						<i class="fa fa-reorder"></i>服务调用情况分析
					</div>
					<div class="tools">
						<a href="javascript:;" class="collapse"></a>
						<a href="#portlet-config" data-toggle="modal" class="config"></a>
						<a href="javascript:;" class="reload"></a>
						<a href="javascript:;" class="remove"></a>
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
