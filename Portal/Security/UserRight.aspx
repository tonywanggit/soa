<%@ Page Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="UserRight.aspx.cs" Inherits="Security_UserRight" %>
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
                    <dxe:ASPxComboBox ID="cbProvider" runat="server" ToolTip="请选择服务提供者" AutoPostBack="true" DataSourceID="OdsProvider" 
                        ValueField="BusinessID" TextField="Description" OnSelectedIndexChanged="cbProvider_SelectedIndexChanged" />
                </td>                
            </tr>
        </table>
        <br />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsService" KeyFieldName="ServiceID" AutoGenerateColumns="False" Width="800px">
            <%-- BeginRegion Columns --%>
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="100">
                    <EditButton Visible="True" Text="修改管理员" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="BusinessID" Caption="服务提供者" VisibleIndex="1" ReadOnly="true" Width="120px">
                    <PropertiesComboBox TextField="BusinessName" ValueField="BusinessID" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsProvider" />
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataColumn FieldName="ServiceName" Caption="服务名称" VisibleIndex="2" ReadOnly="true">
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="Description" VisibleIndex="3" Caption="服务描述" ReadOnly="true">
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="PersonalID" VisibleIndex="4" Caption="服务管理员" Width="80px">
                    <PropertiesComboBox TextField="PersonalName" ValueField="PersonalID" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsUser">
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>    
            </Columns>
            <%-- EndRegion --%>
            <SettingsEditing Mode="Inline"/>
            <SettingsPager AlwaysShowPager="true" />
            <SettingsText EmptyDataRow="暂无数据！" CommandCancel="取消" CommandUpdate="保存" />
        </dxwgv:ASPxGridView>           
        <script type="text/javascript">
            //调用条件：页面包含ASPxLoadingPanel ClientInstanceName="LoadingPanel"
            InitCustomLoadingPanel(); 
        </script>
    </ContentTemplate>
    </asp:UpdatePanel>
    <%-- BeginRegion DataSource --%>
    <asp:ObjectDataSource ID="OdsUser" runat="server" 
        TypeName="ESB.UddiService" 
        DataObjectTypeName="ESB.Personal" 
        SelectMethod="GetAllPerson"
        >
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsProvider" runat="server" 
        TypeName="ESB.UddiService" 
        DataObjectTypeName="ESB.BusinessEntity" 
        SelectMethod="GetAllBusinessEntity">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsService" runat="server" 
        TypeName="ESB.UddiService"
        DataObjectTypeName="ESB.BusinessService"
        SelectMethod="GetBusinessServiceByBussinessID" UpdateMethod="UpdateBusinessService"
        OnSelecting="OdsService_Selecting" >
    </asp:ObjectDataSource>
</asp:Content>

