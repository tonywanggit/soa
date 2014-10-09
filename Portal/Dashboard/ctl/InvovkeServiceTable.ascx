<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InvovkeServiceTable.ascx.cs" Inherits="Dashboard_ctl_InvovkeServiceTable" %>
<table class="table table-striped table-hover">
    <thead>
        <tr>
            <th>序号</th>
            <th>服务提供者</th>
            <th>服务名称</th>
            <th>方法名称</th>
            <th>调用次数</th>
            <th>调用次数（100~200ms）</th>
            <th>调用次数（>200ms）</th>
            <th>异常次数</th>
            <th>缓存命中次数</th>
        </tr>
    </thead>
    <tbody>
        <%
            Int32 order = 0;
            foreach (ESB.ServiceMonitor item in m_ServiceMonitorList)
            {
                order++;
        %>        
        <tr>
            <td><%=order %></td>
            <td><%=item.BusinessName %></td>
            <td><%=item.ServiceName %></td>
            <td><%=item.MethodName %></td>
            <td><%=item.CallSuccessNum %></td>
            <th><%=item.CallLevel2Num %></th>
            <th><%=item.CallLevel3Num %></th>
            <th><%if(item.CallFailureNum > 0){ %><span style="color:red"><%} %><%=item.CallFailureNum %></span></th>
            <th><%=item.CallHitCacheNum %></th>
        </tr>
        <%} %>
    </tbody>
</table>