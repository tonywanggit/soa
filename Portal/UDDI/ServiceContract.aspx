<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="ServiceContract.aspx.cs" Inherits="UDDI_ServiceContract" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dxlp" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dxhe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="localCssPlaceholder" Runat="Server">
    <script type="text/javascript">
        var validateConfirmPerson = true;

        function cancelValidate(s, e) {
            validateConfirmPerson = false;
            e.processOnServer = true;
        }

        function onConfirmPersonValidation(s, e) {
            if (!validateConfirmPerson) {
                e.isValid = true;
                e.errorText = null;

                validateConfirmPerson = true;
            } else {
                if (!e.value) {
                    e.isValid = false;
                    e.errorText = "请选择审批人！";
                }
            }
        }

    </script>
    <style type="text/css">
        body, body.dxheDesignViewArea_BlackGlass /* or body.dxhedesignviewarea_{themepostfix} for a themed control  */
        {
            font-family: Arial;
            font-size: 10pt;
        }
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
                    <dxe:ASPxComboBox ID="cbProvider" runat="server" ToolTip="请选择服务提供者" AutoPostBack="true" DataSourceID="OdsProvider" 
                        ValueField="BusinessID" TextField="Description" OnSelectedIndexChanged="cbProvider_SelectedIndexChanged" Width="165" />
                </td> 
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbService" ClientInstanceName="cbService" runat="server" ToolTip="请选择具体服务" AutoPostBack="true" DataSourceID="OdsService" Width="150"
                        ValueField="ServiceID" TextField="Description" AutoResizeWithContainer="true" DropDownStyle="DropDownList" OnSelectedIndexChanged="cbService_SelectedIndexChanged" TextFormatString="{0}">
                        <Columns>
                            <dxe:ListBoxColumn Caption="服务名称" FieldName="ServiceName" ToolTip="服务名称" Width="100px" />
                            <dxe:ListBoxColumn Caption="服务描述" FieldName="Description" ToolTip="服务描述" Width="200px" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td> 
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbServiceVersion" ClientInstanceName="cbService" runat="server" ToolTip="请选择服务版本" AutoPostBack="true" DataSourceID="OdsServiceVersion" Width="120" 
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
                    <dxe:ASPxButton ID="btnAdd" runat="server" Text="新增契约" UseSubmitBehavior="False" AutoPostBack="false">
                        <ClientSideEvents GotFocus="cancelValidate" Click="function(){ 
                            grid.AddNewRow(); 
                        }" />
                    </dxe:ASPxButton>
                </td>    
                <td class="buttonCell">
                    <dxe:ASPxButton ID="btnCommit" runat="server" Text="提交评审" AutoPostBack="true" OnClick="btnCommit_Click"></dxe:ASPxButton>
                </td> 
                <td class="buttonCell">
                    <dxe:ASPxButton ID="btnRevise" runat="server" Text="修订版本" AutoPostBack="true" OnClick="btnRevise_Click">
                    </dxe:ASPxButton>
                </td>
                <td class="buttonCell">
                    <dxe:ASPxButton ID="btnUpdate" runat="server" Text="升级版本" AutoPostBack="true" OnClick="btnUpdate_Click">
                        <ClientSideEvents GotFocus="cancelValidate" />
                    </dxe:ASPxButton>
                </td>
                <td class="buttonCell">
                    <dxe:ASPxButton ID="btnDelete" runat="server" Text="删除" AutoPostBack="true" OnClick="btnDelete_Click">
                        <ClientSideEvents GotFocus="cancelValidate" />
                    </dxe:ASPxButton>
                </td>
                <td class="buttonCell">
                    <dxe:ASPxButton ID="btnObsolete" runat="server" Text="废弃" AutoPostBack="true" OnClick="btnObsolete_Click"></dxe:ASPxButton>
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
                            <dxe:ASPxComboBox ID="cbCreatePerson" runat="server" ReadOnly="true" Width="130" DataSourceID="OdsUser" ValueField="PersonalID" TextField="PersonalName"></dxe:ASPxComboBox>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxLabel ID="ASPxLabel2" Text="创建时间:" runat="server"></dxe:ASPxLabel>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxDateEdit ID="deCreateDateTime" runat="server" ReadOnly="true" Width="130" EditFormat="DateTime"></dxe:ASPxDateEdit>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxLabel ID="ASPxLabel3" Text="审批人:" runat="server" ></dxe:ASPxLabel>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxComboBox ID="cbConfirmPerson" ClientInstanceName="cbConfirmPerson" runat="server" Width="130" DataSourceID="OdsUser" ValueField="PersonalID" TextField="PersonalName">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="false" ErrorText="请选择审批人！" />
                                </ValidationSettings>
                                <ClientSideEvents Validation="onConfirmPersonValidation" />
                            </dxe:ASPxComboBox>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxLabel ID="ASPxLabel4" Text="审批时间:" runat="server"></dxe:ASPxLabel>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxDateEdit ID="deConfirmDateTime" runat="server" ReadOnly="true" Width="130" EditFormat="DateTime"></dxe:ASPxDateEdit>
                        </td>
                        <td class="buttonCell">
                            <dxe:ASPxButton ID="btnSaveVersion" runat="server" Text="保存信息" OnClick="btnSaveVersion_Click"></dxe:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="buttonCell" style="height:25px;vertical-align:top">
                            <dxe:ASPxLabel ID="ASPxLabel5" Text="版本描述:" runat="server"></dxe:ASPxLabel>
                        </td>
                        <td class="buttonCell" colspan="8">
                            <dxe:ASPxMemo ID="mmVersionDesc" Width="100%" runat="server" Height="80px"></dxe:ASPxMemo>
                        </td>
                    </tr>
                    <tr style="<%=m_OpinionTrStyle%>">
                        <td class="buttonCell" style="height:25px;">
                            <dxe:ASPxLabel ID="ASPxLabel6" Text="评审意见:" runat="server"></dxe:ASPxLabel>
                        </td>
                        <td class="buttonCell" colspan="8">
                            <dxe:ASPxTextBox ID="txtOpinion" Width="100%" runat="server" ></dxe:ASPxTextBox>
                        </td>
                    </tr>
                </table>            
            </dxp:PanelContent></PanelCollection>
        </dxrp:ASPxRoundPanel>
        <br />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsServiceContract" KeyFieldName="OID" PreviewFieldName="MethodContract" 
            OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" 
            AutoGenerateColumns="False" Width="900">
            <%-- BeginRegion Columns --%>
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="70">
                    <EditButton Visible="True" Text="编辑" />
                    <DeleteButton Visible="true" Text="删除" />
                </dxwgv:GridViewCommandColumn>   
                <dxwgv:GridViewDataTextColumn FieldName="MethodName" VisibleIndex="1" Caption="方法名称" >
                    <EditFormSettings ColumnSpan="2" VisibleIndex="1" CaptionLocation="Near" />
                    <EditFormCaptionStyle VerticalAlign="Top" />
                </dxwgv:GridViewDataTextColumn>      
                <dxwgv:GridViewDataMemoColumn FieldName="MethodContract" VisibleIndex="2" Visible="false" Caption="契约描述" >
                    <EditFormSettings ColumnSpan="2" VisibleIndex="2" CaptionLocation="Near" Visible="false" />
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
                                <td>契约描述：</td>
                                <td style="border:none;padding-left:10px;"><%# Container.Text %></td>
                            </tr>
                        </tbody>
                    </table>
                </PreviewRow>
                <EditForm>
                    <div style="padding:4px 4px 3px 4px">
                        <dxwgv:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors" runat="server">
                        </dxwgv:ASPxGridViewTemplateReplacement>
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td style="padding: 2px 4px 2px 10px;vertical-align:top;white-space: nowrap;">契约描述</td>
                                <td style="padding: 2px 0px 2px 4px;">
                                    <dxhe:ASPxHtmlEditor id="HtmlEditor" runat="server" OnInit="HtmlEditor_Init" height="300px" width="100%" Html='<%# Eval("MethodContract")%>'>
                                        <Toolbars>
                                            <dxhe:StandardToolbar2>
                                                <Items>
                                                    <dxhe:ToolbarParagraphFormattingEdit Width="120px">
                                                        <Items>
                                                            <dxhe:ToolbarListEditItem Text="Normal" Value="p"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="Heading  1" Value="h1"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="Heading  2" Value="h2"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="Heading  3" Value="h3"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="Heading  4" Value="h4"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="Heading  5" Value="h5"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="Heading  6" Value="h6"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="Address" Value="address"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="Normal (DIV)" Value="div"></dxhe:ToolbarListEditItem>
                                                        </Items>
                                                    </dxhe:ToolbarParagraphFormattingEdit>
                                                    <dxhe:ToolbarFontNameEdit DefaultCaption="Courier">
                                                        <Items>
                                                            <dxhe:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="Tahoma" Value="Tahoma"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="Verdana" Value="Verdana"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="Arial" Value="Arial"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="Courier" Value="Courier"></dxhe:ToolbarListEditItem>
                                                        </Items>
                                                    </dxhe:ToolbarFontNameEdit>
                                                    <dxhe:ToolbarFontSizeEdit>
                                                        <Items>
                                                            <dxhe:ToolbarListEditItem Text="1 (8pt)" Value="1"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="2 (10pt)" Value="2"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="3 (12pt)" Value="3"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="4 (14pt)" Value="4"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="5 (18pt)" Value="5"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="6 (24pt)" Value="6"></dxhe:ToolbarListEditItem>
                                                            <dxhe:ToolbarListEditItem Text="7 (36pt)" Value="7"></dxhe:ToolbarListEditItem>
                                                        </Items>
                                                    </dxhe:ToolbarFontSizeEdit>
                                                    <dxhe:ToolbarBoldButton BeginGroup="True" ></dxhe:ToolbarBoldButton>
                                                    <dxhe:ToolbarItalicButton></dxhe:ToolbarItalicButton>
                                                    <dxhe:ToolbarUnderlineButton></dxhe:ToolbarUnderlineButton>
                                                    <dxhe:ToolbarStrikethroughButton></dxhe:ToolbarStrikethroughButton>
                                                    <dxhe:ToolbarJustifyLeftButton BeginGroup="True"></dxhe:ToolbarJustifyLeftButton>
                                                    <dxhe:ToolbarJustifyCenterButton></dxhe:ToolbarJustifyCenterButton>
                                                    <dxhe:ToolbarJustifyRightButton></dxhe:ToolbarJustifyRightButton>
                                                    <dxhe:ToolbarJustifyFullButton></dxhe:ToolbarJustifyFullButton>
                                                    <dxhe:ToolbarBackColorButton BeginGroup="True"></dxhe:ToolbarBackColorButton>
                                                    <dxhe:ToolbarFontColorButton></dxhe:ToolbarFontColorButton>
                                                </Items>
                                            </dxhe:StandardToolbar2>
                                        </Toolbars>
                                        <Settings AllowHtmlView="false" AllowContextMenu="False" />
                                        <SettingsText DesignViewTab="撰写" PreviewTab="预览" />
                                    </dxhe:ASPxHtmlEditor>
                                </td>
                            </tr>
                        </table>
                        
                    </div>
                    <div style="text-align:right; padding:2px 10px 2px 2px">
                        <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                        <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                    </div>
                </EditForm>
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

