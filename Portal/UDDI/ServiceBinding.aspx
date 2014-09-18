<%@ Page Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="ServiceBinding.aspx.cs" Inherits="UDDI_ServiceBinding" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dxlp" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="localCssPlaceholder" runat="server">
    <style type="text/css">
        td.buttonCell {
            padding-right: 6px;
        }
    </style>
    <script src="../Scripts/jquery.1.2.1.js" type="text/javascript"></script>
    <script type="text/javascript">
    </script>
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
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbService" ClientInstanceName="cbService" runat="server" ToolTip="请选择具体服务" AutoPostBack="true" DataSourceID="OdsService" Width="250"
                        ValueField="ServiceID" TextField="Description" AutoResizeWithContainer="true" DropDownStyle="DropDownList" OnSelectedIndexChanged="cbService_SelectedIndexChanged" TextFormatString="{0}">
                        <Columns>
                            <dxe:ListBoxColumn Caption="服务名称" FieldName="ServiceName" ToolTip="服务名称" Width="100px" />
                            <dxe:ListBoxColumn Caption="服务描述" FieldName="Description" ToolTip="服务描述" Width="200px" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td> 
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbServiceVersion" ClientInstanceName="cbServiceVersion" runat="server" ToolTip="请选择服务版本" AutoPostBack="true" DataSourceID="OdsServiceVersion" Width="100" 
                        ValueField="BigVer" TextField="Description" AutoResizeWithContainer="true" DropDownStyle="DropDownList" TextFormatString="{0}"
                        OnSelectedIndexChanged="cbServiceVersion_SelectedIndexChanged" OnDataBound="cbServiceVersion_DataBound" >
                        <Columns>
                            <dxe:ListBoxColumn Caption="版本号" FieldName="BigVer" ToolTip="版本号" Width="100px" />
                            <dxe:ListBoxColumn Caption="版本描述" FieldName="Description" ToolTip="版本描述" Width="200px" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td>           
                <td class="buttonCell">
                    <dxe:ASPxButton ID="btnAdd" runat="server" Text="新增绑定" UseSubmitBehavior="False" AutoPostBack="false">
                        <ClientSideEvents Click="function(){ 
                            grid.AddNewRow(); 
                        }" />
                    </dxe:ASPxButton>
                </td>     
            </tr>
        </table>
        <br />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsBinding" KeyFieldName="TemplateID" AutoGenerateColumns="False" Width="900">
            <%-- BeginRegion Columns --%>
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="70">
                    <EditButton Visible="True" Text="编辑" />
                    <DeleteButton Visible="true" Text="删除" />
                </dxwgv:GridViewCommandColumn>               
                <dxwgv:GridViewDataHyperLinkColumn FieldName="Address" Caption="访问地址" VisibleIndex="1" >
                    <PropertiesHyperLinkEdit TextField="Address" TextFormatString="{0}" NavigateUrlFormatString="{0}" Target="_blank" >
                    </PropertiesHyperLinkEdit>
                    <EditFormSettings ColumnSpan="2" VisibleIndex="3" />
                </dxwgv:GridViewDataHyperLinkColumn>
                <dxwgv:GridViewDataMemoColumn FieldName="Description" VisibleIndex="2" Caption="绑定描述" >
                    <EditFormSettings ColumnSpan="2" VisibleIndex="4" CaptionLocation="Near" />
                    <EditFormCaptionStyle VerticalAlign="Top" />
                    <PropertiesMemoEdit Height="80px" />
                </dxwgv:GridViewDataMemoColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="BindingType" VisibleIndex="3" Caption="绑定类型">
                    <PropertiesComboBox TextField="Text" ValueField="Value" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="xdsBindingType">
                    </PropertiesComboBox>
                    <EditFormSettings VisibleIndex="1" />
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="BindingStatus" VisibleIndex="4" Caption="状态">
                    <PropertiesComboBox TextField="Text" ValueField="Value" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="xdsBindingStatus">
                    </PropertiesComboBox>
                    <EditFormSettings VisibleIndex="2" />
                </dxwgv:GridViewDataComboBoxColumn>    
            </Columns>
            <%-- EndRegion --%>
            <SettingsEditing Mode="EditFormAndDisplayRow"/>
            <SettingsPager AlwaysShowPager="true" />
            <SettingsText EmptyDataRow="暂无数据！" CommandCancel="取消" CommandUpdate="保存" ConfirmDelete="您确定要删除这条记录吗？" />
            <SettingsBehavior ConfirmDelete="true" />
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
    <asp:ObjectDataSource ID="OdsService" runat="server" 
        TypeName="ESB.UddiService"
        DataObjectTypeName="ESB.BusinessService"
        SelectMethod="GetBusinessServiceByBussinessID"
        OnSelecting="OdsService_Selecting">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsServiceVersion" runat="server" 
        TypeName="ESB.ContractSerivce"
        DataObjectTypeName="ESB.BusinessServiceVersion"
        SelectMethod="GetServiceBigVersionByServiceID"
        OnSelecting="OdsServiceVersion_Selecting">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsBinding" runat="server" 
        TypeName="ESB.UddiService"
        DataObjectTypeName="ESB.BindingTemplate"
        SelectMethod="GetBindingByServiceIDAndVersion"  UpdateMethod="UpdateBindingTemplate" InsertMethod="InsertBindingTemplate" DeleteMethod="DeleteBindingTemplate"
        OnSelecting="OdsBinding_Selecting" OnUpdating="OdsBinding_Updating" OnInserting="OdsBinding_Inserting">
    </asp:ObjectDataSource>
    <asp:XmlDataSource DataFile="~/App_Data/BindingStatusEnum.xml" XPath="//Binding" ID="xdsBindingStatus" runat="server" />
    <asp:XmlDataSource DataFile="~/App_Data/BindingTypeEnum.xml" XPath="//BindingType" ID="xdsBindingType" runat="server" />
    <%-- EndRegion DataSource --%>
</asp:Content>

