<%@ Page Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="ScheduleHistory.aspx.cs" Inherits="Schedule_ScheduleHistory" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dxlp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="localCssPlaceholder" runat="server">
    <style type="text/css">
        td.buttonCell {
            padding-right: 6px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="phContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <dxlp:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="False" />
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="buttonCell">
                    <dxe:ASPxDateEdit ID="dateScopeBegin" runat="server" ToolTip="请选择开始时间" AutoPostBack="true"
                        OnDateChanged="dateScopeBegin_OnDateChanged" Width="100px" EditFormat="Custom" EditFormatString="yyyy-MM-dd" />
                </td>
                <td class="buttonCell">
                    <dxe:ASPxDateEdit ID="dateScopeEnd" runat="server" ToolTip="请选择结束时间" AutoPostBack="true"
                        OnDateChanged="dateScopeEnd_OnDateChanged" Width="100px" EditFormat="Custom" EditFormatString="yyyy-MM-dd" />
                </td>
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbExceptionType" runat="server" ToolTip="请选择调度的状态" AutoPostBack="true"
                        OnSelectedIndexChanged="cbExceptionType_SelectedIndexChanged" Width="100px" >
                        <Items>
                            <dxe:ListEditItem Text="成功调度"　Value="1" />
                            <dxe:ListEditItem Text="失败调度"　Value="0" />
                        </Items>
                    </dxe:ASPxComboBox>                
                </td>
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbSchedulerType" runat="server" ToolTip="请选择调度类型" AutoPostBack="true" Width="140px"
                        DataSourceID="xdsScheduleType" ValueField="Value" TextField="Text" OnSelectedIndexChanged="cbSchedulerType_SelectedIndexChanged"
                    >
                    </dxe:ASPxComboBox>                
                </td> 
                <td class="buttonCell" style="width:200px">
                </td>              
            </tr>
            <tr>
                <td class="buttonCell" colspan="2" style="padding-top:5px;">
                    <dxe:ASPxComboBox ID="cbProvider" runat="server" ToolTip="请选择调度系统" AutoPostBack="true" DataSourceID="OdsProvider" 
                        ValueField="业务编码" TextField="描述" TextFormatString="{0}({1})" Width="205px" AutoResizeWithContainer="true" DropDownStyle="DropDownList"
                        OnSelectedIndexChanged="cbProvider_SelectedIndexChanged" ValueType="System.String"
                        >
                        <Columns>
                            <dxe:ListBoxColumn Caption="系统代码" FieldName="业务名称" ToolTip="系统代码" Width="50px" />
                            <dxe:ListBoxColumn Caption="系统名称" FieldName="描述" ToolTip="系统名称" Width="155px" />     
                        </Columns>               
                    </dxe:ASPxComboBox>    
                </td>
                <td class="buttonCell" colspan="3" style="padding-top:5px;">
                    <dxe:ASPxComboBox ID="cbHostScheduler" runat="server" ToolTip="请选择调度实例" AutoPostBack="true" DataSourceID="OdsHostScheduler"  Width="350px"
                        ValueField="SCHD_ID" TextField="SCHD_NAME" OnSelectedIndexChanged="cbHostScheduler_SelectedIndexChanged" >
                    </dxe:ASPxComboBox>
                </td>
            </tr>
        </table>
        <br />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsSchedulerHistory" KeyFieldName="OID" AutoGenerateColumns="False" Width="100%">
            <%-- BeginRegion Columns --%>
            <Columns>
               <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="50">
                    <EditButton Visible="True" Text="查看" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataColumn FieldName="HOST_NAME" Caption="调度主机" VisibleIndex="1" ReadOnly="true">
                    <EditFormSettings VisibleIndex="1" Visible="true" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="SCHD_NAME" VisibleIndex="2" Caption="调度名称" ReadOnly="true">
                    <EditFormSettings VisibleIndex="2" CaptionLocation="Near"  Visible="true" />
                    <EditFormCaptionStyle VerticalAlign="Top" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataDateColumn FieldName="BEGIN_TIME" VisibleIndex="3" Caption="调度开始时间" ReadOnly="true">
                    <EditFormSettings VisibleIndex="3"  />
                    <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn FieldName="END_TIME" VisibleIndex="4"  Caption="调度结束时间" ReadOnly="true">
                    <EditFormSettings VisibleIndex="4" />
                    <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>  
                <dxwgv:GridViewDataColumn FieldName="SCHD_ID" VisibleIndex="5" Caption="消息编码" Visible="false" ReadOnly="true">
                    <EditFormSettings VisibleIndex="5" Visible="false" ColumnSpan="2" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataMemoColumn FieldName="CALL_MEMO" VisibleIndex="6" Caption="调用结果" Visible="false" ReadOnly="true">
                    <PropertiesMemoEdit Height="200" />
                    <EditFormSettings VisibleIndex="6" Visible="true" ColumnSpan="2" />
                </dxwgv:GridViewDataMemoColumn>
            </Columns>
            <%-- EndRegion --%>
            <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="600px" />
            <SettingsPager AlwaysShowPager="true" />
            <SettingsText EmptyDataRow="暂无数据！" CommandCancel="取消" CommandUpdate="保存" PopupEditFormCaption="查看异常" />
            <Templates>
                <EditForm>
                    <dxwgv:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                </EditForm>
            </Templates>
        </dxwgv:ASPxGridView>   
        <script type="text/javascript">
            //调用条件：页面包含ASPxLoadingPanel ClientInstanceName="LoadingPanel"
            InitCustomLoadingPanel(); 
        </script>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <%-- BeginRegion DataSource --%>
    <asp:ObjectDataSource ID="OdsProvider" runat="server" 
        TypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.注册服务目录服务" 
        DataObjectTypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.业务实体" 
        SelectMethod="获得所有服务提供者">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsSchedulerHistory" runat="server" 
        TypeName="JN.Esb.Portal.ServiceMgt.调度服务.SchedulerService"
        DataObjectTypeName="JN.Esb.Portal.ServiceMgt.调度服务.ESB_SCHD_HISTORY_VIEW"
        SelectMethod="SearchScheduleHistory"
        OnSelecting="OdsSchedulerHistory_Selecting">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsHostScheduler" runat="server" 
        TypeName="JN.Esb.Portal.ServiceMgt.调度服务.SchedulerService"
        DataObjectTypeName="JN.Esb.Portal.ServiceMgt.调度服务.ESB_SCHD_VIEW"
        SelectMethod="GetSchedulerByHost"
        OnSelecting="OdsHostScheduler_Selecting">
    </asp:ObjectDataSource>
    <asp:XmlDataSource DataFile="~/App_Data/ScheduleTypeEnum.xml" XPath="//ScheduleType" ID="xdsScheduleType" runat="server" />
    
    <%-- EndRegion DataSource --%>
</asp:Content>

