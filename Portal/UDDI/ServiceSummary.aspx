<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="ServiceSummary.aspx.cs" Inherits="UDDI_ServiceSummary" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dxlp" %>
<%@ Register TagPrefix="dxe" Namespace="DevExpress.Web.ASPxEditors" Assembly="DevExpress.Web.ASPxEditors.v9.1" %>
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
                </td>                
            </tr>
        </table>
        <br />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsService" KeyFieldName="ServiceID" AutoGenerateColumns="False" Width="800px">
            <%-- BeginRegion Columns --%>
            <Columns>
                <dxwgv:GridViewDataComboBoxColumn FieldName="BusinessID" Caption="服务提供者" VisibleIndex="1" ReadOnly="true">
                    <PropertiesComboBox TextField="BusinessName" ValueField="BusinessID" TextFormatString="{0}({1})" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsProvider" >
                        <Columns>
                            <dxe:ListBoxColumn Caption="系统代码" FieldName="BusinessName" ToolTip="系统代码" Width="100px" />
                            <dxe:ListBoxColumn Caption="系统名称" FieldName="Description" ToolTip="系统名称" Width="200px" />
                        </Columns>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataHyperLinkColumn FieldName="ServiceID" Caption="服务名称" VisibleIndex="2" ReadOnly="true" >
                    <PropertiesHyperLinkEdit TextField="ServiceName" TextFormatString="{0}" NavigateUrlFormatString="ServiceBinding.aspx?SID={0}" >
                    </PropertiesHyperLinkEdit>
                </dxwgv:GridViewDataHyperLinkColumn>
                <dxwgv:GridViewDataColumn FieldName="Description" VisibleIndex="3" Caption="服务描述" ReadOnly="true">
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="PersonalID" VisibleIndex="4" Caption="服务管理员">
                    <PropertiesComboBox TextField="PersonalName" ValueField="PersonalID" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsUser">
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>    
            </Columns>
            <%-- EndRegion --%>
            <Settings ShowGroupPanel="true" ShowFooter="true" />
            <SettingsPager AlwaysShowPager="true" PageSize="15" />
            <SettingsText EmptyDataRow="暂无数据！" CommandCancel="取消" CommandUpdate="保存" GroupPanel="可将字段拖到这里进行分组" />
            <GroupSummary>
                <dxwgv:ASPxSummaryItem FieldName="BusinessID" SummaryType="Count" />
            </GroupSummary>
            <TotalSummary>
                <dxwgv:ASPxSummaryItem FieldName="BusinessID" SummaryType="Count" />
            </TotalSummary>
        </dxwgv:ASPxGridView>           
        <script type="text/javascript">
            //调用条件：页面包含ASPxLoadingPanel ClientInstanceName="LoadingPanel"
            InitCustomLoadingPanel(); 
        </script>
    </ContentTemplate>
    </asp:UpdatePanel>
    <%-- BeginRegion DataSource --%>
    <asp:ObjectDataSource ID="OdsProvider" runat="server" 
        TypeName="ESB.UddiService" 
        DataObjectTypeName="ESB.BusinessEntity" 
        SelectMethod="GetAllBusinessEntity">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsUser" runat="server" 
        TypeName="ESB.UddiService" 
        DataObjectTypeName="ESB.Personal" 
        SelectMethod="GetAllPerson">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsService" runat="server" 
        TypeName="ESB.UddiService" 
        DataObjectTypeName="ESB.BusinessService" 
        SelectMethod="GetServiceList">
    </asp:ObjectDataSource>
</asp:Content>


