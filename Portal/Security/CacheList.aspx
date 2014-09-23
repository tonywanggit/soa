<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="CacheList.aspx.cs" Inherits="Security_CacheList" %>
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
    <script type="text/javascript">
        //--清除缓存的事件
        function OnRemoveCache(s, e) {
            if (confirm("您确定要清除缓存吗, 确保清空不会对应用造成影响！")) {
                e.processOnServer = true;
            }
            else {
                e.processOnServer = false;
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="phContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <dxlp:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="False" />
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always">
    <ContentTemplate>
<%-- EndRegion --%>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td class="buttonCell">
                <dxe:ASPxComboBox ID="cbProvider" runat="server" ToolTip="请选择服务提供者" AutoPostBack="true" DataSourceID="OdsProvider" 
                    OnSelectedIndexChanged="cbProvider_SelectedIndexChanged"
                    ValueField="BusinessID" TextField="Description" />
            </td>  
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click"></dxe:ASPxButton>
            </td>
        </tr>
    </table>
    <br />
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"  AutoGenerateColumns="False" Width="900px" OnCustomButtonCallback="grid_CustomButtonCallback" DataSourceID="OdsServiceConfig">
        <%-- BeginRegion Columns --%>
        <Columns>         
            <dxwgv:GridViewCommandColumn Caption="操作" HeaderStyle-HorizontalAlign="Center">
                <CustomButtons>
                    <dxwgv:GridViewCommandColumnCustomButton Text="清空缓存"></dxwgv:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataComboBoxColumn FieldName="BusinessID" Caption="服务提供者" >
                <PropertiesComboBox TextField="Description" ValueField="BusinessID" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsProvider" />
            </dxwgv:GridViewDataComboBoxColumn>   
            <dxwgv:GridViewDataTextColumn FieldName="ServiceName" Caption="服务"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="MethodName" Caption="方法"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="CacheDuration" Caption="缓存时间"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="CacheKeyNum" Caption="Key数量"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataProgressBarColumn Caption="缓存命中率(近1小时)" FieldName="CacheHitRate" Width="110" Visible="false"></dxwgv:GridViewDataProgressBarColumn>
            <dxwgv:GridViewDataProgressBarColumn Caption="缓存命中率(当天)" FieldName="CacheHitRate" Width="90"></dxwgv:GridViewDataProgressBarColumn>
        </Columns>
        <ClientSideEvents CustomButtonClick="OnRemoveCache" RowDblClick="function(s, e){
            grid.StartEditRow(e.visibleIndex);
        }" />
        <%-- EndRegion --%>
        <SettingsBehavior ConfirmDelete="true" />
        <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="500px" NewItemRowPosition="Top" />
        <SettingsPager AlwaysShowPager="true" /> 
        <SettingsText EmptyDataRow="暂无数据！" ConfirmDelete="您确定要删除吗！" CommandCancel="取消" CommandUpdate="保存" PopupEditFormCaption="查看详情" />
    </dxwgv:ASPxGridView>
    <script type="text/javascript">
        //调用条件：页面包含ASPxLoadingPanel ClientInstanceName="LoadingPanel"
        InitCustomLoadingPanel();
    </script>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="OdsProvider" runat="server" 
        TypeName="ESB.UddiService" 
        DataObjectTypeName="ESB.BusinessEntity" 
        SelectMethod="GetAllBusinessEntity">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsServiceConfig" runat="server" 
        TypeName="ESB.ContractSerivce" 
        DataObjectTypeName="ESB.EsbView_ServiceConfig" 
        SelectMethod="GetCachedServiceConfig"
        OnSelecting="OdsServiceConfig_Selecting"
        OnSelected="OdsServiceConfig_Selected"
        >
    </asp:ObjectDataSource>
<%-- EndRegion --%>
</asp:Content>
