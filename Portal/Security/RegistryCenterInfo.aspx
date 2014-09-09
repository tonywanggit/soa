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
                <dxe:ASPxButton ID="btnRefresh" runat="server" Text="刷新"></dxe:ASPxButton>
            </td>
        </tr>
    </table>
    <br />
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsSettingUri" KeyFieldName="OID" AutoGenerateColumns="False" Width="900px"
        OnInitNewRow="grid_InitNewRow">
        <%-- BeginRegion Columns --%>
        <Columns>
            <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="操作" HeaderStyle-HorizontalAlign="Center">
                <EditButton Visible="True" Text="编辑" />
                <DeleteButton Visible="True" Text="删除" />
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataComboBoxColumn FieldName="UriType" Caption="地址类型">
                <PropertiesComboBox TextField="Text" ValueField="Value" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="xdsUriType">
                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                        <RequiredField IsRequired="true" ErrorText="请填写地址类型！" />
                    </ValidationSettings>
                </PropertiesComboBox>
                <EditFormSettings VisibleIndex="1" />
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataDateColumn FieldName="CreateDateTime" Caption="创建时间" ReadOnly="true">
                <EditFormSettings VisibleIndex="2" />
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss" EditFormatString="yyyy-MM-dd HH:mm:ss" EditFormat="Custom">
                    <ValidationSettings>
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </PropertiesDateEdit>
            </dxwgv:GridViewDataDateColumn>
            <dxwgv:GridViewDataTextColumn FieldName="Uri" Caption="地址" >
                <PropertiesTextEdit>
                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                        <RequiredField IsRequired="true" ErrorText="请填写地址！" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings VisibleIndex="3" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataSpinEditColumn FieldName="Port" Caption="端口号">
                <PropertiesSpinEdit NumberType="Integer">
                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                        <RequiredField IsRequired="true" ErrorText="请填写端口号！" />
                    </ValidationSettings>
                </PropertiesSpinEdit>
                <EditFormSettings VisibleIndex="4" />
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataTextColumn FieldName="UserName" Caption="用户名">
                <EditFormSettings VisibleIndex="5" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="PassWord" Caption="密码">
                <EditFormSettings VisibleIndex="6" />
                <PropertiesTextEdit Password="true">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>  
            <dxwgv:GridViewDataMemoColumn FieldName="Remark" Caption="备注">
                <EditFormSettings VisibleIndex="7" ColumnSpan="2" />
            </dxwgv:GridViewDataMemoColumn>
        </Columns>
        <ClientSideEvents RowDblClick="function(s, e){
            grid.StartEditRow(e.visibleIndex);
        }" />
        <%-- EndRegion --%>
        <SettingsBehavior ConfirmDelete="true" />
        <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="500px" NewItemRowPosition="Top" />
        <SettingsPager AlwaysShowPager="true" /> 
        <SettingsText EmptyDataRow="暂无数据！" ConfirmDelete="您确定要删除吗！" CommandCancel="取消" CommandUpdate="保存" PopupEditFormCaption="编辑地址" />
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

