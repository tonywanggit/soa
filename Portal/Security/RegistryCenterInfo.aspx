<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="RegistryCenterInfo.aspx.cs" Inherits="Security_RegistryCenterInfo" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>

<asp:Content ID="Content2" ContentPlaceHolderID="localCssPlaceholder" runat="server">
    <style type="text/css">
        td.buttonCell {
            padding-right: 6px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="phContent" Runat="Server">
<%-- EndRegion --%>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnRefresh" runat="server" Text="刷新" OnClick="btnRefresh_Click"></dxe:ASPxButton>
            </td>
        </tr>
    </table>
    <br />
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"  AutoGenerateColumns="False" Width="900px" >
        <%-- BeginRegion Columns --%>
        <Columns>
            <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="操作" HeaderStyle-HorizontalAlign="Center" Visible="false">
                <EditButton Visible="True" Text="编辑" />
                <DeleteButton Visible="True" Text="删除" />
            </dxwgv:GridViewCommandColumn>
            
            <dxwgv:GridViewDataDateColumn FieldName="ReceiveDateTime" Caption="连接时间" ReadOnly="true">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss" EditFormatString="yyyy-MM-dd HH:mm:ss" EditFormat="Custom">
                </PropertiesDateEdit>
            </dxwgv:GridViewDataDateColumn>
            
            <dxwgv:GridViewDataTextColumn FieldName="ClientIP" Caption="客户端IP"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="ClientVersion" Caption="版本"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="RegistryClientType" Caption="类型"></dxwgv:GridViewDataTextColumn>
        </Columns>
        <ClientSideEvents RowDblClick="function(s, e){
            grid.StartEditRow(e.visibleIndex);
        }" />
        <%-- EndRegion --%>
        <SettingsBehavior ConfirmDelete="true" />
        <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="500px" NewItemRowPosition="Top" />
        <SettingsPager AlwaysShowPager="true" /> 
        <SettingsText EmptyDataRow="暂无数据，无法连接注册中心！" ConfirmDelete="您确定要删除吗！" CommandCancel="取消" CommandUpdate="保存" PopupEditFormCaption="编辑地址" />
    </dxwgv:ASPxGridView>
    <asp:ObjectDataSource ID="OdsSettingUri" runat="server" 
        TypeName="ESB.SystemSettingService" 
        DataObjectTypeName="ESB.SettingUri" 
        SelectMethod="GetAllSettingUri" DeleteMethod="DeleteSettingUri" UpdateMethod="UpdateSettingUri" InsertMethod="InsertSettingUri"
        >
    </asp:ObjectDataSource>
    <asp:XmlDataSource DataFile="~/App_Data/UriTypeEnum.xml" XPath="//UriType" ID="xdsUriType" runat="server" />
<%-- EndRegion --%>
</asp:Content>

