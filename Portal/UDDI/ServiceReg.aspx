<%@ Page Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="ServiceReg.aspx.cs" Inherits="UDDI_ServiceReg" %>
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
    <script type="text/javascript">
        function OnServiceConfig() {
            //pcServiceConfig.Show();
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="phContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <dxpc:ASPxPopupControl ClientInstanceName="pcAlert" Width="260px" ID="pcAlert" 
        HeaderText="友情提示" runat="server" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" ShowOnPageLoad="false" >
        <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <asp:Label ID="Label1" runat="server" >该服务下已经有发布版本，无法删除！</asp:Label>
                </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>

    <dxlp:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="False" />
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbProvider" runat="server" ToolTip="请选择服务提供者" AutoPostBack="true" DataSourceID="OdsProvider" 
                        ValueField="BusinessID" TextField="Description" OnSelectedIndexChanged="cbProvider_SelectedIndexChanged" />
                </td>           
                <td class="buttonCell">
                    <dxe:ASPxButton ID="btnAdd" runat="server" Text="新增服务" UseSubmitBehavior="false" AutoPostBack="false">
                        <ClientSideEvents Click="function(){ grid.AddNewRow(); }" />
                    </dxe:ASPxButton>
                </td>   
                <td class="buttonCell"> 
                    <asp:Label runat="server" ID="lblError" Text="" ></asp:Label>
                </td>   
            </tr>
        </table>
        <br />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsService" KeyFieldName="ServiceID" 
            OnRowDeleting="grid_RowDeleting" OnRowValidating="grid_RowValidating" OnInitNewRow="grid_InitNewRow"
            AutoGenerateColumns="False" Width="900px" EnableCallBacks="false">
            <%-- BeginRegion Columns --%>
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="80">
                    <EditButton Visible="True" Text="编辑" />
                    <DeleteButton Visible="true" Text="删除" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="BusinessID" Caption="服务提供者" VisibleIndex="1" Width="150" ReadOnly="true">
                    <PropertiesComboBox TextField="Description" ValueField="BusinessID" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsProvider">
                        <ValidationSettings>
                            <RequiredField IsRequired="true" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                    <EditFormSettings Visible="True" VisibleIndex="1" />
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataHyperLinkColumn FieldName="ServiceID" Caption="服务名称" VisibleIndex="2" ReadOnly="true" Width="150" >
                    <PropertiesHyperLinkEdit TextField="ServiceName" NavigateUrlFormatString="ServiceBinding.aspx?SID={0}" >
                    </PropertiesHyperLinkEdit>
                    <EditFormSettings Visible="False" />
                </dxwgv:GridViewDataHyperLinkColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ServiceName" VisibleIndex="3" Caption="服务名称" Visible="false">
                    <PropertiesTextEdit>
                        <ValidationSettings>
                            <RequiredField IsRequired="true" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                    <EditFormSettings Visible="true" VisibleIndex="3" ColumnSpan="2" />
                </dxwgv:GridViewDataTextColumn>                
                <dxwgv:GridViewDataMemoColumn FieldName="Description" VisibleIndex="3" Caption="服务描述" >
                    <EditFormSettings Visible="true" VisibleIndex="4" ColumnSpan="2" />
                    <PropertiesMemoEdit Height="80px" >
                        <ValidationSettings>
                            <RequiredField IsRequired="true" />
                        </ValidationSettings>
                    </PropertiesMemoEdit>
                </dxwgv:GridViewDataMemoColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="PersonalID" VisibleIndex="4" Caption="服务管理员" Width="80">
                    <PropertiesComboBox TextField="PersonalName" ValueField="PersonalID" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsUser">
                        <ValidationSettings>
                            <RequiredField IsRequired="true" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                    <EditFormSettings Visible="true" VisibleIndex="2" />
                </dxwgv:GridViewDataComboBoxColumn>    
            </Columns>
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
                <DetailRow>
                    <dxtc:ASPxPageControl runat="server" ID="pageControlDetail" Width="100%" Height="100px">
                    <TabPages>
                        <dxtc:TabPage Text="服务配置">
                            <ContentCollection>
                                <dxw:ContentControl ID="ccServiceConfig" runat="server">
                                <dxwgv:ASPxGridView ID="grdServiceConfig" runat="server" DataSourceID="OdsServiceConfig" KeyFieldName="OID" Width="100%" 
                                    OnRowValidating="grdServiceConfig_RowValidating"
                                    OnInitNewRow="grdServiceConfig_InitNewRow" OnRowInserting="grdServiceConfig_RowInserting" OnBeforePerformDataSelect="detailGrid_DataSelect">
                                    <Columns>
                                        <dxwgv:GridViewCommandColumn Caption="操作" HeaderStyle-HorizontalAlign="Center">
                                            <EditButton Visible="True" Text="编辑" />
                                            <DeleteButton Visible="true" Text="删除" />
                                            <NewButton Visible="true" Text="新增"></NewButton>
                                        </dxwgv:GridViewCommandColumn>   
                                        <dxwgv:GridViewDataTextColumn FieldName="MethodName" Caption="方法名" >
                                            <PropertiesTextEdit>
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="true" ErrorText="请填写方法名！" />
                                                </ValidationSettings>
                                            </PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataComboBoxColumn FieldName="IsAudit" Caption="是否开启审计" >
                                            <PropertiesComboBox TextField="Text" ValueField="Value" DataSourceID="xdsAuditStatus">
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="true" ErrorText="请选择是否开启审计！" />
                                                </ValidationSettings>
                                            </PropertiesComboBox>
                                        </dxwgv:GridViewDataComboBoxColumn>
                                        <dxwgv:GridViewDataSpinEditColumn FieldName="Timeout" Caption="超时（ms）">
                                            <PropertiesSpinEdit NumberType="Integer" MinValue="0" MaxValue="1000000">
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="true" ErrorText="请填写超时！" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                        </dxwgv:GridViewDataSpinEditColumn>
                                        <dxwgv:GridViewDataSpinEditColumn FieldName="CacheDuration" Caption="缓存时间（s）">
                                            <PropertiesSpinEdit NumberType="Integer" MinValue="0">
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="true" ErrorText="请缓存时间！" />
                                                </ValidationSettings>
                                            </PropertiesSpinEdit>
                                        </dxwgv:GridViewDataSpinEditColumn>
                                        <dxwgv:GridViewDataComboBoxColumn FieldName="QueueCenter" Caption="队列处理服务" >
                                            <PropertiesComboBox TextField="UriPort" ValueField="OID" DataSourceID="OdsQueueCenter" Width="305">
                                            </PropertiesComboBox>
                                        </dxwgv:GridViewDataComboBoxColumn>
                                        <dxwgv:GridViewDataComboBoxColumn FieldName="HBPolicy" Caption="负载均衡策略">
                                            <PropertiesComboBox TextField="Text" ValueField="Value" DataSourceID="xdsHBPolicy">
                                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                                    <RequiredField IsRequired="true" ErrorText="请选择负载均衡策略！" />
                                                </ValidationSettings>
                                            </PropertiesComboBox>
                                        </dxwgv:GridViewDataComboBoxColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="WhiteList" Caption="白名单">
                                            <PropertiesTextEdit Width="305"></PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn FieldName="BlackList" Caption="黑名单">
                                            <PropertiesTextEdit Width="290"></PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataMemoColumn FieldName="MockObject" Caption="服务降级Mock">
                                            <PropertiesMemoEdit Height="60"></PropertiesMemoEdit>
                                            <EditFormSettings ColumnSpan="2" />
                                        </dxwgv:GridViewDataMemoColumn>
                                    </Columns>
                                    <SettingsDetail IsDetailGrid="true"/>
                                    <SettingsText EmptyDataRow=" " CommandCancel="取消" CommandUpdate="保存" />
                                 </dxwgv:ASPxGridView>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Text="版本信息">
                            <ContentCollection><dxw:ContentControl ID="ccVersion" runat="server">
                                <dxwgv:ASPxGridView ID="detailGrid" runat="server" DataSourceID="OdsServiceVersion" KeyFieldName="OID" Width="100%" 
                                     OnBeforePerformDataSelect="detailGrid_DataSelect" >
                                    <Columns>
                                        <dxwgv:GridViewDataColumn FieldName="BigVer" VisibleIndex="0" Caption="版本号" Width="40">
                                            <EditFormSettings ColumnSpan="1" VisibleIndex="0" />
                                        </dxwgv:GridViewDataColumn>
                                        <dxwgv:GridViewDataColumn FieldName="SmallVer" VisibleIndex="0" Caption="修订号" Width="40">
                                            <EditFormSettings ColumnSpan="1" VisibleIndex="0" />
                                        </dxwgv:GridViewDataColumn>
                                        <dxwgv:GridViewDataColumn FieldName="StatusDesc" VisibleIndex="0" Caption="版本状态" Width="60">
                                            <EditFormSettings ColumnSpan="1" VisibleIndex="0" />
                                        </dxwgv:GridViewDataColumn>
                                        <dxwgv:GridViewDataDateColumn FieldName="CreateDateTime" Caption="创建时间" Width="100">
                                            <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm"></PropertiesDateEdit>
                                        </dxwgv:GridViewDataDateColumn>
                                        <dxwgv:GridViewDataDateColumn FieldName="ConfirmDateTime" Caption="审批时间" Width="100">
                                            <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm"></PropertiesDateEdit>
                                        </dxwgv:GridViewDataDateColumn>
                                        <dxwgv:GridViewDataDateColumn FieldName="ObsoleteDateTime" Caption="废弃时间" Width="100">
                                            <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm"></PropertiesDateEdit>
                                        </dxwgv:GridViewDataDateColumn>
                                        <dxwgv:GridViewDataComboBoxColumn FieldName="CreatePersionID" VisibleIndex="4" Caption="创建人" Width="70">
                                            <PropertiesComboBox TextField="PersonalName" ValueField="PersonalID" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsUser">
                                            </PropertiesComboBox>
                                            <EditFormSettings Visible="true" VisibleIndex="2" />
                                        </dxwgv:GridViewDataComboBoxColumn>    
                                        <dxwgv:GridViewDataMemoColumn FieldName="Description" VisibleIndex="1" Caption="版本描述">
                                            <EditFormSettings ColumnSpan="2" VisibleIndex="1" CaptionLocation="Near" />
                                            <EditFormCaptionStyle VerticalAlign="Top" />
                                            <PropertiesMemoEdit Height="80px" />
                                        </dxwgv:GridViewDataMemoColumn>
                                    </Columns>
                                    <SettingsDetail IsDetailGrid="true"/>
                                    <SettingsText EmptyDataRow=" " CommandCancel="取消" CommandUpdate="保存" />
                                 </dxwgv:ASPxGridView>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                    </dxtc:ASPxPageControl>
                </DetailRow>
            </Templates>
            <%-- EndRegion --%>
            <SettingsDetail ShowDetailRow="true" />
            <SettingsEditing Mode="EditFormAndDisplayRow"/>
            <SettingsPager AlwaysShowPager="true" />
            <SettingsBehavior ConfirmDelete="true"  />
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
    <asp:ObjectDataSource ID="OdsService" runat="server" 
        TypeName="ESB.UddiService"
        DataObjectTypeName="ESB.BusinessService"
        SelectMethod="GetBusinessServiceByBussinessID"  UpdateMethod="UpdateBusinessService" InsertMethod="InsertBusinessService" DeleteMethod="DeleteBusinessService"
        OnSelecting="OdsService_Selecting" OnInserting="OdsService_Inserting" OnUpdating="OdsService_Updating" OnDeleting="OdsService_Deleting">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsServiceVersion" runat="server" 
        TypeName="ESB.ContractSerivce"
        DataObjectTypeName="ESB.BusinessServiceVersion"
        SelectMethod="GetServiceVersionByServiceID"  UpdateMethod="UpdateServiceVersion" InsertMethod="InsertServiceVersion" DeleteMethod="DeleteServiceVersion"
        OnInserting="OdsServiceVersion_Inserting">
        <SelectParameters>
            <asp:SessionParameter Name="serviceID" SessionField="ServiceReg_ServiceID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsQueueCenter" runat="server"
        TypeName="ESB.SystemSettingService"
        DataObjectTypeName="ESB.SettingUri"
        SelectMethod="GetQueueCenter">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsServiceConfig" runat="server"
        TypeName="ESB.ContractSerivce"
        DataObjectTypeName="ESB.ServiceConfig"
        SelectMethod="GetServiceConfig" UpdateMethod="UpdateServiceConfig" InsertMethod="InsertServiceConfig" DeleteMethod="DeleteServiceConfig">
        <SelectParameters>
            <asp:SessionParameter Name="serviceID" SessionField="ServiceReg_ServiceID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:XmlDataSource DataFile="~/App_Data/CommonAuditStatus.xml" XPath="//AuditStatus" ID="xdsAuditStatus" runat="server" />
    <asp:XmlDataSource DataFile="~/App_Data/HBPolicy.xml" XPath="//Item" ID="xdsHBPolicy" runat="server" />
</asp:Content>

