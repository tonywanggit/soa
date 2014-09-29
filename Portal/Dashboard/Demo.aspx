<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Demo.aspx.cs" Inherits="Dashboard_Demo" %>
<!DOCTYPE html>
<html lang="en">
    <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="../../favicon.ico">

    <title>MBSOA 演示中心</title>

    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="css/dashboard.css" rel="stylesheet">

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
            <li><a href="Index.aspx">看板</a></li>
            <li><a href="#">演示</a></li>
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
            <h1 class="page-header">演示中心</h1>

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
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown"> 演示 <span class="caret"></span></a>
                        <ul class="dropdown-menu" role="menu">
                            <li><a href="Index.aspx"> 看板 </a></li>
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

            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">SOA实验室</h3>
                </div>
                <div class="panel-body">
                    <form class="form-horizontal" role="form">
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="input-group">
                                <div class="input-group-addon">服务名称：</div>
                                <input type="text" class="form-control" id="txtServiceName" placeholder="服务名称" value="MBSOA_Demo" />
                                <div class="input-group-btn btn-no-radius">
                                    <select id="selNoCache" class="form-control" style="width:140px;border-left:none">
                                        <option selected="selected" value="0">Enable Cache</option>
                                        <option value="1">NoCache</option>
                                    </select>
                                </div>
                                <div class="input-group-btn btn-no-radius">
                                    <button type="button" class="btn btn-warning" id="btnInvoke">Invoke</button>
                                </div>
                                <div class="input-group-btn">
                                    <button type="button" class="btn btn-danger" id="btnInvokeQueue">InvokeQueue</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="input-group">
                                <div class="input-group-addon">方法名称：</div>
                                
                                <div class="input-group-btn btn-no-radius">
                                    <select id="selMethodType" class="form-control" style="width:90px;">
                                        <option selected="selected" value="GET">GET</option>
                                        <option value="POST">POST</option>
                                    </select>
                                </div>
                                
                                <div class="input-group-btn btn-no-radius">
                                    <select id="selDataType" class="form-control" style="width:90px;">
                                        <option selected="selected" value="JSON">JSON</option>
                                        <option value="XML">XML</option>
                                    </select>
                                </div>
                                <input type="text" class="form-control" id="txtMethodName" placeholder="方法名称" value="Echo">
                                <div id="lblDuration" class="input-group-addon">耗时</div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="input-group">
                                <div class="input-group-addon">请求消息：</div>
                                <textarea rows="8" class="form-control" id="txtRequest" placeholder="请输入请求消息">Hello World!</textarea>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <div class="input-group">
                                <div class="input-group-addon">响应消息：</div>
                                <textarea rows="8" readonly="readonly" class="form-control" id="txtResponse"></textarea>
                            </div>
                        </div>
                    </div>
                    </form>
                </div>
            </div>
        </div>
        </div>
    </div>

    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="http://192.168.56.1/CallCenter/Script/mb-esb-1.0.0.js" type="text/javascript"></script>
    <script src="js/demo.js" type="text/javascript"></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="js/ie10-viewport-bug-workaround.js" type="text/javascript"></script>
    </body>
</html>
