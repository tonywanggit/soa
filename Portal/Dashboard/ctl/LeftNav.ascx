<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftNav.ascx.cs" Inherits="Dashboard_LeftNav" %>
<ul class="nav nav-sidebar">
    <%foreach (NavItem item in Nav){ %>
      <li class="<%=item.Class %>"><a href="<%=CurrentPageName %>?BusinessID=<%=item.ID%>"><%=item.Name%></a></li>
    <%} %>
</ul>