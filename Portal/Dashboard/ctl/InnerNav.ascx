<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InnerNav.ascx.cs" Inherits="Dashboard_ctl_InnerNav" %>
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
            <a href="#" class="dropdown-toggle" data-toggle="dropdown"> <%=PageName %> <span class="caret"></span></a>
            <ul class="dropdown-menu" role="menu">
                <li><a href="<%=PageUrl1 %>">  <%=PageName1 %> </a></li>
            </ul>
        </li>
        <li class="dropdown active">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown"> <%=BusinessName %> <span class="caret"></span></a>
            <ul class="dropdown-menu" role="menu">
            <%foreach (NavItem item in Nav){ %>
              <li><a href="<%=CurrentPageName %>?BusinessID=<%=item.ID%>"><%=item.Name%></a></li>
            <%} %>
            </ul>
        </li>
        </ul>
        <form class="navbar-form navbar-left" role="search">
        <div class="form-group" style="display:none;">
            <div class="input-group">
                <input type="text" class="form-control" placeholder="请输入服务名称">
                <span class="input-group-btn">
                <button class="btn btn-primary" type="button">查找</button>
                </span>
            </div><!-- /input-group -->
        </div>
        </form>
        <ul class="nav navbar-nav navbar-right">
            <li><a href="#"><% =Today %></a></li>
        </ul>
    </div><!-- /.navbar-collapse -->
    </div><!-- /.container-fluid -->
</nav>