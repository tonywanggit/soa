<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OverviewStat.ascx.cs" Inherits="Dashboard_ctl_OverviewStat" %>

<div class="stats-overview stat-block">
	<div class="display stat <%=Chart %> huge">
		<span class="<%=ChartLOB %>-chart" style="display: inline;">
            <span style="display: none;">
                <span style="display: none;">
                    <span style="display: none;"><%=ChartData %></span>
                    <canvas width="50" height="20"></canvas>
                </span>
                <canvas width="50" height="20"></canvas>
            </span>
            <canvas width="40" height="20"></canvas>
		</span>
		<div class="percent">
		    <%=Percent %>%
		</div>
	</div>
	<div class="details">
		<div class="title" style="<%= TitleStyle%>">
		    <%=Title %>
		</div>
		<div class="numbers">
		    <%= Value %>
		</div>
		<div class="progress">
			<span style="width: <%=Percent %>%;" class="progress-bar progress-bar-<%=ProgressBar %>" aria-valuenow="<%=Percent %>" aria-valuemin="0" aria-valuemax="100">
			<span class="sr-only">
			<%=Percent %>% Complete </span>
			</span>
		</div>
	</div>
</div>