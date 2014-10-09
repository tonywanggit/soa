<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServiceVersionTable.ascx.cs" Inherits="Dashboard_ctl_ServiceVersionTable" %>
<table class="table table-striped table-hover">
    <thead>
        <tr>
            <th>序号</th>
            <th>服务提供者</th>
            <th>服务名称</th>
            <th>版本</th>
            <th>描述</th>
            <th>创建日期</th>
            <th>发布日期</th>
            <th>管理员</th>
            <th>是否默认</th>
        </tr>
    </thead>
    <tbody>
        <%
            Int32 order = 0;
            foreach (ESB.BusinessServiceVersion item in m_BusinessServiceVersion)
            {
                order++;
        %>        
        <tr>
            <td><%=order %></td>
            <td><%=item.BusinessName %></td>
            <td><%=item.ServiceName %></td>
            <td><%=item.BigVer %></td>
            <td><%=item.ServiceDesc %></td>
            <th><%=item.CreateDateTime.ToString("yyyy-MM-dd HH:ss") %></th>
            <th><%=item.ConfirmDateTime.ToString("yyyy-MM-dd HH:ss") %></th>
            <th><%=item.Manager %></th>
            <th><%=item.DefaultVersion == item.BigVer ? "是" : "否" %></th>
        </tr>
        <%} %>
    </tbody>
</table>