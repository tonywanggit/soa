<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="ServiceSummary.aspx.cs" Inherits="UDDI_ServiceSummary" %>
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
                </td>                
            </tr>
        </table>
        <br />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsService" KeyFieldName="服务编码" AutoGenerateColumns="False" Width="100%">
            <%-- BeginRegion Columns --%>
            <Columns>
                <dxwgv:GridViewDataComboBoxColumn FieldName="业务编码" Caption="服务提供者" VisibleIndex="1" ReadOnly="true">
                    <PropertiesComboBox TextField="业务名称" ValueField="业务编码" TextFormatString="{0}({1})" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsProvider" >
                        <Columns>
                            <dxe:ListBoxColumn Caption="系统代码" FieldName="业务名称" ToolTip="系统代码" Width="100px" />
                            <dxe:ListBoxColumn Caption="系统名称" FieldName="描述" ToolTip="系统名称" Width="200px" />
                        </Columns>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataHyperLinkColumn FieldName="服务编码" Caption="服务名称" VisibleIndex="2" ReadOnly="true" >
                    <PropertiesHyperLinkEdit TextField="服务名称" TextFormatString="{0}" NavigateUrlFormatString="ServiceBinding.aspx?SID={0}" >
                    </PropertiesHyperLinkEdit>
                </dxwgv:GridViewDataHyperLinkColumn>
                <dxwgv:GridViewDataColumn FieldName="描述" VisibleIndex="3" Caption="服务描述" ReadOnly="true">
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="个人编码" VisibleIndex="4" Caption="服务管理员">
                    <PropertiesComboBox TextField="姓名" ValueField="个人编码" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsUser">
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>    
            </Columns>
            <%-- EndRegion --%>
            <Settings ShowGroupPanel="true" ShowFooter="true" />
            <SettingsPager AlwaysShowPager="true" PageSize="15" />
            <SettingsText EmptyDataRow="暂无数据！" CommandCancel="取消" CommandUpdate="保存" GroupPanel="可将字段拖到这里进行分组" />
            <GroupSummary>
                <dxwgv:ASPxSummaryItem FieldName="业务编码" SummaryType="Count" />
            </GroupSummary>
            <TotalSummary>
                <dxwgv:ASPxSummaryItem FieldName="业务编码" SummaryType="Count" />
            </TotalSummary>
        </dxwgv:ASPxGridView>           
        <script type="text/javascript">
            //调用条件：页面包含ASPxLoadingPanel ClientInstanceName="LoadingPanel"
            InitCustomLoadingPanel(); 
        </script>
    </ContentTemplate>
    </asp:UpdatePanel>
    <%-- BeginRegion DataSource --%>
    <asp:ObjectDataSource ID="OdsUser" runat="server" 
        TypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.注册服务目录服务" 
        DataObjectTypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.个人" 
        SelectMethod="获得所有服务管理员" 
        />
    <asp:ObjectDataSource ID="OdsProvider" runat="server" 
        TypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.注册服务目录服务" 
        DataObjectTypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.业务实体" 
        SelectMethod="获得所有服务提供者"
        />
    <asp:ObjectDataSource ID="OdsService" runat="server" 
        TypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.注册服务目录服务" 
        DataObjectTypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.服务" 
        SelectMethod="获得具体服务_服务提供者"  UpdateMethod="修改具体服务"
        OnSelecting="OdsService_Selecting">
    </asp:ObjectDataSource>
</asp:Content>


