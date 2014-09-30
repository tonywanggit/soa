<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopNav.ascx.cs" Inherits="Dashboard_ctl_TopNav" %>

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
        <li><a href="Demo.aspx">演示</a></li>
        <li><a href="../Default.aspx" target="_blank">管理</a></li>
        </ul>
    </div>
    </div>
</nav>