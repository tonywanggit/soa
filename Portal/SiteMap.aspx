<%@ Page Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="SiteMap.aspx.cs" Inherits="_SiteMap" Title="企业服务总线 - 功能导航" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxSiteMapControl" TagPrefix="dxsm" %>
<%@ Register assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSiteMapControl" TagPrefix="dxsm" %>
<%@ Register assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHeadline" TagPrefix="dxhl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phContent" Runat="Server">
    <%-- BeginRegion DataSource --%>
    <dxsm:ASPxSiteMapDataSource ID="ASPxSiteMapDataSource1" runat="server"/>      
    <%-- EndRegion --%>
    <dxsm:ASPxSiteMapControl Categorized="True" MaximumDisplayLevels="3" Width="800px" ID="ASPxSiteMapControl1" runat="server" DataSourceID="ASPxSiteMapDataSource1">
        <Columns>
            <dxsm:Column></dxsm:Column>
            <dxsm:Column></dxsm:Column>
            <dxsm:Column></dxsm:Column>
        </Columns>
        <DefaultLevelProperties Wrap="False" />
        <NodeTextTemplate>
            <dxhl:ASPxHeadline TailPosition="KeepWithLastWord" CssClass="PageStatus ShowInline" id="hlItem" runat="server" ShowContentAsLink="True" NavigateUrl='<%# Eval("Url") %>' EnableViewState="False" EnableDefaultAppearance="False" ContentText='<%# Eval("Title") %>' EnableTheming="False" OnDataBinding="hlItem_DataBinding">
                <ContentStyle LineHeight="118%" />
            </dxhl:ASPxHeadline> 
        </NodeTextTemplate>
    </dxsm:ASPxSiteMapControl>
</asp:Content>
