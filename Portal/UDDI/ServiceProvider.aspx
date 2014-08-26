<%@ Page Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="ServiceProvider.aspx.cs" Inherits="UDDI_ServiceProvider" %>
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
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnAddAdmin" runat="server" Text="新增服务提供者" UseSubmitBehavior="False" AutoPostBack="false">
                    <ClientSideEvents Click="function(){ grid.AddNewRow(); }" />
                </dxe:ASPxButton>
            </td>
        </tr>
    </table>
    <br />
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsService" KeyFieldName="BusinessID" AutoGenerateColumns="False" Width="100%">
        <%-- BeginRegion Columns --%>
        <Columns>
            <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="操作" HeaderStyle-HorizontalAlign="Center">
                <EditButton Visible="True" Text="编辑" />
                <DeleteButton Visible="True" Text="删除" />
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn FieldName="BusinessName" Caption="系统代码" VisibleIndex="1">
                <EditFormSettings VisibleIndex="0" ColumnSpan="2" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataColumn FieldName="Description" Caption="系统名称" VisibleIndex="2">
                <EditFormSettings VisibleIndex="1" ColumnSpan="2" />
            </dxwgv:GridViewDataColumn>  
        </Columns>
        <%-- EndRegion --%>
        <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="400px" NewItemRowPosition="Top" />
        <SettingsPager AlwaysShowPager="true" />
        <SettingsText EmptyDataRow="暂无数据！" CommandCancel="取消" CommandUpdate="保存" PopupEditFormCaption="编辑用户" ConfirmDelete="您确定要删除这条记录吗？" />
        <SettingsBehavior ConfirmDelete="true" />
        <Templates>
            <EditForm>
            <div style="padding:4px 4px 3px 4px">
            <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="100%" Height="100px">
            <TabPages>
                <dxtc:TabPage Text="基本信息" Visible="true">
                    <ContentCollection><dxw:ContentControl ID="ContentControl1" runat="server">
                        <dxwgv:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                    </dxw:ContentControl></ContentCollection>
                </dxtc:TabPage>
            </TabPages>
            </dxtc:ASPxPageControl>
            </div>
            <div style="text-align:right; padding:2px 2px 2px 2px">
                <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
            </div>
            </EditForm>
        </Templates>
    </dxwgv:ASPxGridView>
    <asp:ObjectDataSource ID="OdsService" runat="server" 
        TypeName="ESB.UddiService" 
        DataObjectTypeName="ESB.BusinessEntity" 
        SelectMethod="GetAllBusinessEntity" DeleteMethod="DeleteBusinessEntity" UpdateMethod="UpdateBusinessEntity" InsertMethod="InsertBusinessEntity" >
    </asp:ObjectDataSource>
</asp:Content>

