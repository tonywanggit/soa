<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="ServiceContractReview.aspx.cs" Inherits="UDDI_ServiceContractReview" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dxlp" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="localCssPlaceholder" Runat="Server">
    <style type="text/css">
        td.buttonCell {
            padding-right: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phContent" Runat="Server">
       <asp:ScriptManager ID="ScriptManager" runat="server" />
    <dxlp:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="False" />
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbProvider" runat="server" ReadOnly="true" ToolTip="请选择服务提供者" AutoPostBack="true" DataSourceID="OdsProvider" 
                        ValueField="BusinessID" TextField="Description" OnSelectedIndexChanged="cbProvider_SelectedIndexChanged" />
                </td> 
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbService" ClientInstanceName="cbService" runat="server" ReadOnly="true" ToolTip="请选择具体服务" AutoPostBack="true" DataSourceID="OdsService" Width="150"
                        ValueField="ServiceID" TextField="Description" AutoResizeWithContainer="true" DropDownStyle="DropDownList" OnSelectedIndexChanged="cbService_SelectedIndexChanged" TextFormatString="{0}">
                        <Columns>
                            <dxe:ListBoxColumn Caption="服务名称" FieldName="ServiceName" ToolTip="服务名称" Width="100px" />
                            <dxe:ListBoxColumn Caption="服务描述" FieldName="Description" ToolTip="服务描述" Width="200px" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td> 
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbServiceVersion" ClientInstanceName="cbService" runat="server" ReadOnly="false" ToolTip="请选择服务版本" AutoPostBack="true" DataSourceID="OdsServiceVersion" Width="120" 
                        ValueField="OID" TextField="Description" AutoResizeWithContainer="true" DropDownStyle="DropDownList" TextFormatString="{0}.{1}（{2}）"
                        OnSelectedIndexChanged="cbServiceVersion_SelectedIndexChanged" OnDataBound="cbServiceVersion_DataBound">
                        <Columns>
                            <dxe:ListBoxColumn Caption="版本号" FieldName="BigVer" ToolTip="版本号" Width="50px" />
                            <dxe:ListBoxColumn Caption="修订号" FieldName="SmallVer" ToolTip="修订号" Width="50px" />
                            <dxe:ListBoxColumn Caption="状态" FieldName="StatusDesc" ToolTip="状态" Width="80px" />
                            <dxe:ListBoxColumn Caption="版本描述" FieldName="Description" ToolTip="版本描述" Width="200px" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td>           
                <td class="buttonCell">
                    <dxe:ASPxTextBox ID="txtOpinion" runat="server" Width="280" ToolTip="请填写审批意见" NullText="请填写审批意见" ValidationSettings-ErrorDisplayMode="ImageWithTooltip">
                        <ValidationSettings>
                            <RequiredField IsRequired="true" ErrorText="请填写审批意见" />
                        </ValidationSettings>
                    </dxe:ASPxTextBox>
                </td>
                <td class="buttonCell">
                    <dxe:ASPxButton ID="btnRefuse" runat="server" Text="拒绝" AutoPostBack="true" OnClick="btnRefuse_Click"></dxe:ASPxButton>
                </td>    
                <td class="buttonCell">
                    <dxe:ASPxButton ID="btnPass" runat="server" Text="通过" AutoPostBack="true" OnClick="btnPass_Click"></dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <br />
        <dxrp:ASPxRoundPanel ID="rpServiceVersion" SkinID="RoundPanelGroupBox" runat="server" Width="100%" GroupBoxCaptionOffsetY="-20px" HeaderText="服务版本">
            <ContentPaddings PaddingTop="10px" PaddingLeft="10px" PaddingBottom="10px" PaddingRight="2px" />
            <PanelCollection><dxp:PanelContent>
                <table cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="buttonCell" style="height:30px">
                            <dxe:ASPxLabel ID="ASPxLabel1" Text="创建人:" runat="server"></dxe:ASPxLabel>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxComboBox ID="cbCreatePerson" runat="server" ReadOnly="true" Width="100" DataSourceID="OdsUser" ValueField="PersonalID" TextField="PersonalName"></dxe:ASPxComboBox>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxLabel ID="ASPxLabel2" Text="创建时间:" runat="server"></dxe:ASPxLabel>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxDateEdit ID="deCreateDateTime" runat="server" ReadOnly="true" Width="120" EditFormat="DateTime"></dxe:ASPxDateEdit>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxLabel ID="ASPxLabel6" Text="提交时间:" runat="server"></dxe:ASPxLabel>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxDateEdit ID="deCommitDateTime" runat="server" ReadOnly="true" Width="120" EditFormat="DateTime"></dxe:ASPxDateEdit>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxLabel ID="ASPxLabel3" Text="审批人:" runat="server" ></dxe:ASPxLabel>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxComboBox ID="cbConfirmPerson" runat="server" Width="100" ReadOnly="true"  DataSourceID="OdsUser" ValueField="PersonalID" TextField="PersonalName">
                            </dxe:ASPxComboBox>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxLabel ID="ASPxLabel4" Text="审批时间:" runat="server"></dxe:ASPxLabel>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxDateEdit ID="deConfirmDateTime" runat="server" ReadOnly="true" Width="120" EditFormat="DateTime"></dxe:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="buttonCell" style="height:25px;vertical-align:top">
                            <dxe:ASPxLabel ID="ASPxLabel5" Text="版本描述:" runat="server"></dxe:ASPxLabel>
                        </td>
                        <td class="buttonCell" colspan="9">
                            <dxe:ASPxMemo ID="mmVersionDesc" ReadOnly="true" Width="100%" runat="server" Height="80px"></dxe:ASPxMemo>
                        </td>
                    </tr>
                </table>            
            </dxp:PanelContent></PanelCollection>
        </dxrp:ASPxRoundPanel>
        <br />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsServiceContract" KeyFieldName="OID" PreviewFieldName="MethodContract" 
            AutoGenerateColumns="False" Width="900" OnDataBound="grid_DataBound">
            <%-- BeginRegion Columns --%>
            <Columns> 
                <dxwgv:GridViewDataTextColumn FieldName="MethodName" VisibleIndex="1" Caption="方法名称" >
                    <EditFormSettings ColumnSpan="2" VisibleIndex="1" CaptionLocation="Near" />
                    <EditFormCaptionStyle VerticalAlign="Top" />
                </dxwgv:GridViewDataTextColumn>      
                <dxwgv:GridViewDataMemoColumn FieldName="MethodContract" VisibleIndex="2" Visible="false" Caption="契约描述" >
                    <EditFormSettings ColumnSpan="2" VisibleIndex="2" CaptionLocation="Near" Visible="True" />
                    <EditFormCaptionStyle VerticalAlign="Top" />
                    <PropertiesMemoEdit Height="200px" />
                </dxwgv:GridViewDataMemoColumn>    
                <dxwgv:GridViewDataComboBoxColumn FieldName="CreatePersonID" VisibleIndex="3" Caption="撰写人" Width="80px">
                    <EditFormSettings Visible="False" />
                    <PropertiesComboBox TextField="PersonalName" ValueField="PersonalID" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsUser">
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>  
                <dxwgv:GridViewDataDateColumn FieldName="CreateDateTime" VisibleIndex="4" Caption="撰写时间" Width="120px">
                    <EditFormSettings Visible="False" />
                    <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
            </Columns>
            <Templates>
                <PreviewRow>
                    <table style="border:none">
                        <tbody>
                            <tr>
                                <td style="border:none;"><%# Container.Text %></td>
                            </tr>
                        </tbody>
                    </table>
                </PreviewRow>
            </Templates>
            <%-- EndRegion --%>
            <Settings ShowPreview="true" />
            <SettingsDetail ShowDetailRow="false"/>
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
    <asp:ObjectDataSource ID="OdsService" runat="server" 
        TypeName="ESB.UddiService"
        DataObjectTypeName="ESB.BusinessService"
        SelectMethod="GetBusinessServiceByBussinessID"
        OnSelecting="OdsService_Selecting">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsServiceVersion" runat="server" 
        TypeName="ESB.ContractSerivce"
        DataObjectTypeName="ESB.BusinessServiceVersion"
        SelectMethod="GetServiceVersionByServiceID"
        OnSelecting="OdsServiceVersion_Selecting">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsServiceContract" runat="server" 
        TypeName="ESB.ContractSerivce"
        DataObjectTypeName="ESB.ServiceContract"
        SelectMethod="SelectServiceContract"  UpdateMethod="UpdateServiceContract" InsertMethod="InsertServiceContract" DeleteMethod="DeleteServiceContract"
        OnSelecting="OdsServiceContract_Selecting" OnUpdating="OdsServiceContract_Updating" OnInserting="OdsServiceContract_Inserting">
    </asp:ObjectDataSource>
    <asp:XmlDataSource DataFile="~/App_Data/BindingStatusEnum.xml" XPath="//Binding" ID="xdsBindingStatus" runat="server" />
    <asp:XmlDataSource DataFile="~/App_Data/BindingTypeEnum.xml" XPath="//BindingType" ID="xdsBindingType" runat="server" />
    <%-- EndRegion DataSource --%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="phOnceContent" Runat="Server">
</asp:Content>

