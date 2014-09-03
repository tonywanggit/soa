<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="ServiceReview.aspx.cs" Inherits="UDDI_ServiceReview" %>
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
                    <dxe:ASPxComboBox ID="cbFilter" runat="server" ToolTip="请选择筛选类型" AutoPostBack="true" OnSelectedIndexChanged="cbFilter_SelectedIndexChanged" >
                        <Items>
                            <dxe:ListEditItem Text="等我评审的服务契约" Value="1" />
                            <dxe:ListEditItem Text="评审通过" Value="2" />
                            <dxe:ListEditItem Text="被我拒绝" Value="3" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>  
            </tr>
        </table>
        <br />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsServiceVersionView" KeyFieldName="VersionID" 
            AutoGenerateColumns="False" Width="900px">
            <%-- BeginRegion Columns --%>
            <Columns>
                <dxwgv:GridViewDataComboBoxColumn FieldName="BusinessID" Caption="服务提供者" VisibleIndex="1" >
                    <PropertiesComboBox TextField="Description" ValueField="BusinessID" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsProvider" />
                    <EditFormSettings Visible="True" VisibleIndex="1" />
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataHyperLinkColumn FieldName="VersionID" Caption="服务名称" VisibleIndex="2" ReadOnly="true" >
                    <PropertiesHyperLinkEdit TextField="ServiceName" NavigateUrlFormatString="ServiceContractReview.aspx?VID={0}" >
                    </PropertiesHyperLinkEdit>
                    <EditFormSettings Visible="False" />
                </dxwgv:GridViewDataHyperLinkColumn>
                <dxwgv:GridViewDataColumn FieldName="BigVer" VisibleIndex="3" Caption="版本号" Visible="true">
                    <EditFormSettings Visible="true" VisibleIndex="3" ColumnSpan="2" />
                </dxwgv:GridViewDataColumn>   
                <dxwgv:GridViewDataColumn FieldName="SmallVer" VisibleIndex="3" Caption="修订号" Visible="true">
                    <EditFormSettings Visible="true" VisibleIndex="3" ColumnSpan="2" />
                </dxwgv:GridViewDataColumn>                
                <dxwgv:GridViewDataMemoColumn FieldName="Description" VisibleIndex="3" Caption="版本描述" >
                    <EditFormSettings Visible="true" VisibleIndex="4" ColumnSpan="2" />
                    <PropertiesMemoEdit Height="80px" />
                </dxwgv:GridViewDataMemoColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="CreatePersionID" VisibleIndex="4" Caption="契约撰写人">
                    <PropertiesComboBox TextField="PersonalName" ValueField="PersonalID" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsUser">
                    </PropertiesComboBox>
                    <EditFormSettings Visible="true" VisibleIndex="2" />
                </dxwgv:GridViewDataComboBoxColumn>    
                <dxwgv:GridViewDataDateColumn FieldName="CreateDateTime" Caption="提交时间" Width="130" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss"></dxwgv:GridViewDataDateColumn>
            </Columns>
            <%-- EndRegion --%>
            <SettingsDetail ShowDetailRow="false" />
            <SettingsEditing Mode="EditFormAndDisplayRow"/>
            <SettingsPager AlwaysShowPager="true" />
            <SettingsBehavior ConfirmDelete="true" />
            <SettingsText EmptyDataRow="暂无数据！" CommandCancel="取消" CommandUpdate="保存" ConfirmDelete="您确定要删除这条记录吗？" />
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
        SelectMethod="GetAllPerson">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsProvider" runat="server" 
        TypeName="ESB.UddiService" 
        DataObjectTypeName="ESB.BusinessEntity" 
        SelectMethod="GetAllBusinessEntity">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsServiceVersionView" runat="server" 
        TypeName="ESB.ContractSerivce"
        DataObjectTypeName="ESB.EsbView_ServiceVersion"
        SelectMethod="GetServiceVersionViewByConfirmPerson"
        OnSelecting="OdsServiceVersionView_Selecting">
    </asp:ObjectDataSource>
</asp:Content>



