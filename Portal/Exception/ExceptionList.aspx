﻿<%@ Page Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="ExceptionList.aspx.cs" Inherits="Exception_ExceptionList" %>
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
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsException" KeyFieldName="ExceptionID" 
             AutoGenerateColumns="False" Width="900" SettingsPager-PageSize="20"
             OnCustomButtonCallback="grid_OnCustomButtonCallback" OnCustomColumnDisplayText="grid_OnCustomColumnDisplayText" 
             OnHtmlEditFormCreated="grid_OnHtmlEditFormCreated"
            >
            <%-- BeginRegion Columns --%>
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="70">
                    <EditButton Visible="True" Text="查看" />
                    <CustomButtons>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnPigeonhole" Visibility="BrowsableRow" Text="归档" />
                    </CustomButtons>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="BindingTemplateID" Caption="调用系统" VisibleIndex="1" >
                    <EditFormSettings VisibleIndex="1" Visible="False" />
                    <PropertiesComboBox TextField="BusinessName" ValueField="BusinessID" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsProvider" />
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataColumn FieldName="BindingTemplateID" VisibleIndex="2" Caption="调用服务" ReadOnly="true">
                    <EditFormSettings VisibleIndex="12" CaptionLocation="Near"  Visible="False" />
                    <EditFormCaptionStyle VerticalAlign="Top" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="MethodName" VisibleIndex="3" Caption="调用方法" ReadOnly="true">
                    <EditFormSettings VisibleIndex="13" Visible="False" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="ExceptionCode" VisibleIndex="3" Caption="异常代码" ReadOnly="true">
                    <EditFormSettings VisibleIndex="4" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataDateColumn FieldName="ExceptionTime" VisibleIndex="4" Caption="异常时间" ReadOnly="true">
                    <EditFormSettings VisibleIndex="5"  />
                    <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>    
                <dxwgv:GridViewDataColumn FieldName="MessageID" VisibleIndex="5" Caption="消息编码" Visible="false" ReadOnly="true">
                    <EditFormSettings VisibleIndex="5" Visible="true" ColumnSpan="2" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="HostName" VisibleIndex="5" Caption="调用主机" Visible="false" ReadOnly="true">
                    <EditFormSettings VisibleIndex="5" Visible="true" ColumnSpan="2" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataMemoColumn FieldName="Description" VisibleIndex="5" Caption="异常描述" Visible="false" ReadOnly="true">
                    <EditFormSettings VisibleIndex="5" Visible="true" ColumnSpan="2" />
                    <PropertiesMemoEdit Height="60px" />
                </dxwgv:GridViewDataMemoColumn>
                <dxwgv:GridViewDataMemoColumn FieldName="ExceptionInfo" VisibleIndex="5" Caption="详细信息" Visible="false" ReadOnly="true">
                    <EditFormSettings VisibleIndex="5" Visible="true" ColumnSpan="2" />
                    <PropertiesMemoEdit Height="180px" />
                </dxwgv:GridViewDataMemoColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="BindingType" VisibleIndex="3" Caption="通讯协议" ReadOnly="true" Visible="False">
                    <EditFormSettings VisibleIndex="2" Visible="true"/>                    
                    <PropertiesComboBox TextField="Text" ValueField="Value" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="xdsBindingType">
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="RequestType" VisibleIndex="3" Caption="通讯模式" ReadOnly="true" Visible="False">
                    <EditFormSettings VisibleIndex="3" Visible="true" />
                    <PropertiesComboBox TextField="Text" ValueField="Value" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="xdsRequestType">
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                
            </Columns>
            <%-- EndRegion --%>
            <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="600px" />
            <SettingsPager AlwaysShowPager="true"  />
            <SettingsText EmptyDataRow="暂无数据！" CommandCancel="取消" CommandUpdate="重发" PopupEditFormCaption="查看异常" />
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
                    <dxtc:TabPage Text="请求数据"  Visible="true">
                        <ContentCollection><dxw:ContentControl runat="server">
                         <dxe:ASPxMemo runat="server" ID="notesEditor" Text='<%# GetBindingReqBody(Eval("ExceptionID").ToString())%>' Width="100%" Height="400px"></dxe:ASPxMemo>
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
    <asp:ObjectDataSource ID="OdsException" runat="server"
        TypeName="ESB.ExceptionService"
        DataObjectTypeName="ESB.ExceptionCoreTb" 
        SelectMethod="GetAllExceptionByBusinessID" UpdateMethod="UpdateException"
        OnSelecting="OdsException_Selecting" OnUpdating="OdsException_OnUpdating">
    </asp:ObjectDataSource>    
    <asp:XmlDataSource DataFile="~/App_Data/RequestTypeEnum.xml" XPath="//RequestType" ID="xdsRequestType" runat="server" />
    <asp:XmlDataSource DataFile="~/App_Data/BindingTypeEnum.xml" XPath="//BindingType" ID="xdsBindingType" runat="server" />
    <%-- EndRegion DataSource --%>
</asp:Content>


