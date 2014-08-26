<%@ Page Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="Security_UserList" %>
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
                <dxe:ASPxButton ID="btnAddAdmin" runat="server" Text="新增管理员" UseSubmitBehavior="False" AutoPostBack="false">
                    <ClientSideEvents Click="function(){ grid.AddNewRow(); }" />
                </dxe:ASPxButton>
            </td>
        </tr>
    </table>
    <br />
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsUser" KeyFieldName="PersonalID" AutoGenerateColumns="False" OnRowValidating="grid_RowValidating" OnRowUpdating="grid_RowUpdating" Width="800px">
        <%-- BeginRegion Columns --%>
        <Columns>
            <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="操作" HeaderStyle-HorizontalAlign="Center">
                <EditButton Visible="True" Text="编辑" />
                <DeleteButton Visible="True" Text="删除" />
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn FieldName="PersonalName" Caption="姓名" VisibleIndex="1">
                <EditFormSettings VisibleIndex="0" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataColumn FieldName="Phone" Caption="电话" VisibleIndex="2">
                <EditFormSettings VisibleIndex="1" />
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="PersonalAccount" VisibleIndex="3" Visible="True"  Caption="登陆帐号">
                <EditFormSettings VisibleIndex="3" ColumnSpan="1" Visible="True"/>
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="Mail" VisibleIndex="4" Caption="邮件帐号">
                <EditFormSettings VisibleIndex="4" ColumnSpan="1" />
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataComboBoxColumn FieldName="permission" VisibleIndex="5" Caption="角色">
                <PropertiesComboBox TextField="Text" ValueField="Value" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="xdsRole">
                </PropertiesComboBox>
                <EditFormSettings VisibleIndex="5" ColumnSpan="1" />
            </dxwgv:GridViewDataComboBoxColumn>    
        </Columns>
        <ClientSideEvents RowDblClick="function(s, e){
            grid.StartEditRow(e.visibleIndex);
        }" />
        <%-- EndRegion --%>
        <SettingsBehavior ConfirmDelete="true" />
        <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="500px" NewItemRowPosition="Top" />
        <SettingsPager AlwaysShowPager="true" /> 
        <SettingsText EmptyDataRow="暂无数据！" ConfirmDelete="您确定要删除吗！" CommandCancel="取消" CommandUpdate="保存" PopupEditFormCaption="编辑用户" />
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
    <asp:ObjectDataSource ID="OdsUser" runat="server" 
        TypeName="ESB.UddiService" 
        DataObjectTypeName="ESB.Personal" 
        SelectMethod="GetAllPerson" DeleteMethod="DeletePerson" UpdateMethod="UpdatePerson" InsertMethod="InsertPerson"
        >
    </asp:ObjectDataSource>
    <asp:XmlDataSource DataFile="~/App_Data/RoleEnum.xml" XPath="//Role" ID="xdsRole" runat="server" />
<%-- EndRegion --%>
</asp:Content>
