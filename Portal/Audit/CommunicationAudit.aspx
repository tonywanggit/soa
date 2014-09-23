<%@ Page Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="CommunicationAudit.aspx.cs" Inherits="Audit_CommunicationAudit" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dxlp" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>

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
                    <dxe:ASPxDateEdit ID="dateScopeBegin" runat="server" ToolTip="请选择开始时间" AutoPostBack="false"
                        Width="95px" EditFormat="Custom" EditFormatString="yyyy-MM-dd" />
                </td>
                <td class="buttonCell">
                    <dxe:ASPxDateEdit ID="dateScopeEnd" runat="server" ToolTip="请选择开始时间" AutoPostBack="false"
                         Width="95px" EditFormat="Custom" EditFormatString="yyyy-MM-dd" />
                </td>
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbExceptionType" runat="server" ToolTip="请选择异常的类型" AutoPostBack="false" Width="90px" >
                        <Items>
                            <dxe:ListEditItem Text="成功调用"　Value="Success" />
                            <dxe:ListEditItem Text="未处理异常"　Value="Exception" />
                            <dxe:ListEditItem Text="已重发异常"　Value="ExceptionReSend" />
                            <dxe:ListEditItem Text="已归档异常"　Value="ExceptionPigeonhole" />
                        </Items>
                    </dxe:ASPxComboBox>                
                </td>
                <td class="buttonCell">
                    <dxe:ASPxTextBox ID="txtHostName" runat="server" ToolTip="请选择主机名称" AutoPostBack="true" Width="95px"></dxe:ASPxTextBox>
                </td> 
                <td class="buttonCell">
                    <dxe:ASPxCheckBox ID="chkShowHeartBeat" runat="server" Checked="true" ToolTip="选择是否查看心跳检测记录" Text="查看心跳检测记录"></dxe:ASPxCheckBox>
                </td>           
                <td class="buttonCell">
                    
                </td>     
            </tr>
            <tr>
                <td class="buttonCell" colspan="2">
                    <dxe:ASPxComboBox ID="cbProvider" runat="server" ToolTip="请选择服务提供者" AutoPostBack="true" DataSourceID="OdsProvider" 
                            ValueField="BusinessID" TextField="Description" OnSelectedIndexChanged="cbProvider_SelectedIndexChanged" Width="198px" />
                </td>
                <td class="buttonCell" colspan="2">
                    <dxe:ASPxComboBox ID="cbService" runat="server" ToolTip="请选择具体服务" AutoPostBack="false" DataSourceID="OdsService"  Width="192px"
                        ValueField="ServiceID" TextField="Description" AutoResizeWithContainer="true" DropDownStyle="DropDownList" TextFormatString="{0}">
                        <Columns>
                            <dxe:ListBoxColumn Caption="服务名称" FieldName="ServiceName" ToolTip="ServiceName" Width="100px" />
                            <dxe:ListBoxColumn Caption="服务描述" FieldName="Description" ToolTip="Description" Width="200px" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td>
                <td class="buttonCell">                

                </td>
                <td class="buttonCell"><dxe:ASPxButton ID="cmdSearch" runat="server" ToolTip="点击查询" Text="查询" OnClick="cmdSearch_Click"/></td>
            </tr>
        </table>
        <br />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"
            DataSourceID="OdsAuditBusiness" KeyFieldName="OID" AutoGenerateColumns="False" Width="900" 
            DataSourceForceStandardPaging="True" SettingsPager-PageSize="15" OnHtmlEditFormCreated="grid_OnHtmlEditFormCreated" >
            <%-- BeginRegion Columns --%>
            <Columns>
               <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="50">
                    <EditButton Visible="True" Text="查看" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataColumn FieldName="BusinessName" Caption="调用系统" VisibleIndex="1" ReadOnly="true" >
                    <EditFormSettings VisibleIndex="7" Visible="true" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="ServiceName" VisibleIndex="2" Caption="调用服务" ReadOnly="true">
                    <EditFormSettings VisibleIndex="9" CaptionLocation="Near"  Visible="true" />
                    <EditFormCaptionStyle VerticalAlign="Top" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="MethodName" VisibleIndex="3" Caption="调用方法" ReadOnly="true">
                    <EditFormSettings VisibleIndex="10" Visible="true" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataDateColumn FieldName="ReqBeginTime" VisibleIndex="4" Caption="入站时间" Visible="false" ReadOnly="true">
                    <EditFormSettings VisibleIndex="1" Visible="True" />
                    <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn FieldName="ReqEndTime" VisibleIndex="4" Caption="出站时间" Visible="false" ReadOnly="true">
                    <EditFormSettings VisibleIndex="2" Visible="True"  />
                    <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss" AllowUserInput="False"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn FieldName="CallBeginTime" VisibleIndex="4" Caption="调用开始时间" ReadOnly="true">
                    <EditFormSettings VisibleIndex="3"  />
                    <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn FieldName="CallEndTime" VisibleIndex="4" Caption="调用结束时间" ReadOnly="true">
                    <EditFormSettings VisibleIndex="4"  />
                    <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn> 
                <dxwgv:GridViewDataDateColumn FieldName="ServiceBeginTime" VisibleIndex="4" Caption="服务开始时间" Visible="false" ReadOnly="true">
                    <EditFormSettings VisibleIndex="5" Visible="True" />
                    <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn FieldName="ServiceEndTime" VisibleIndex="4" Caption="服务结束时间" Visible="false" ReadOnly="true">
                    <EditFormSettings VisibleIndex="6" Visible="True"  />
                    <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>  
                <dxwgv:GridViewDataColumn FieldName="MessageID" VisibleIndex="5" Caption="消息编码" Visible="false" ReadOnly="true">
                    <EditFormSettings VisibleIndex="13" Visible="true" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="HostName" VisibleIndex="5" Caption="调用主机" Visible="false" ReadOnly="true">
                    <EditFormSettings VisibleIndex="8" Visible="true" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="BindingAddress" VisibleIndex="5" Caption="调用地址" Visible="false" ReadOnly="true">
                    <EditFormSettings VisibleIndex="11" Visible="true" ColumnSpan="2" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ConsumerIP" Caption="消费者IP" Visible="true" ReadOnly="true">
                    <EditFormSettings VisibleIndex="12" Visible="true"/>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="IsCache" Caption="命中缓存" ReadOnly="true" CellStyle-HorizontalAlign="Center">
                    <EditFormSettings Visible="false" />
                    <PropertiesComboBox>
                        <Items>
                            <dxe:ListEditItem Value="0" Text="" />
                            <dxe:ListEditItem Value="1" Text="*" />
                        </Items>
                    </PropertiesComboBox>
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
                    <dxtc:TabPage Text="请求数据"  Visible="true" >
                        <ContentCollection><dxw:ContentControl runat="server">
                         <dxe:ASPxMemo ID="txtMessageBody" ReadOnly="true" runat="server" Text='<%# Eval("OID").ToString()%>' Width="100%" Height="400px"></dxe:ASPxMemo>
                        </dxw:ContentControl></ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Text="响应数据"  Visible="true">
                        <ContentCollection><dxw:ContentControl runat="server">
                         <dxe:ASPxMemo ID="txtReturnMessageBody" ReadOnly="true" runat="server" Text='<%# Eval("OID").ToString()%>' Width="100%" Height="400px"></dxe:ASPxMemo>
                        </dxw:ContentControl></ContentCollection>
                    </dxtc:TabPage>
                    <dxtc:TabPage Text="依赖分析" Visible="true">
                        <ContentCollection>
                            <dxw:ContentControl runat="server">
                                <dxwtl:ASPxTreeList ID="treeList" runat="server" AutoGenerateColumns="False" OnHtmlRowPrepared="treeList_HtmlRowPrepared"
                                     Width="100%" DataSourceID="OdsAuditTrace" KeyFieldName="InvokeID" ParentFieldName="ParentInvokeID">
                                    <Columns>
                                        <dxwtl:TreeListDataColumn FieldName="ServiceName" Caption="服务名称" VisibleIndex="0" />            
                                        <dxwtl:TreeListDataColumn FieldName="MethodName" Caption="方法名称" VisibleIndex="1" />          
                                        <dxwtl:TreeListDataColumn FieldName="ReqBeginTime" Caption="请求开始" VisibleIndex="2" />          
                                        <dxwtl:TreeListDataColumn FieldName="ReqEndTime" Caption="请求结束" VisibleIndex="3" />            
                                        <dxwtl:TreeListDataColumn FieldName="InvokeTimeSpan"  Caption="耗时（ms）" VisibleIndex="4"  DisplayFormat="#,###.000" />  
                                        <dxwtl:TreeListDataColumn FieldName="OID"  Caption="耗时（ms）" VisibleIndex="4" Visible="false" />
                                    </Columns>
                                     <Styles>
                                         <AlternatingNode Enabled="true" />
                                     </Styles>
                                     <Settings GridLines="Both" />
                                     <SettingsBehavior ExpandCollapseAction="NodeDblClick" AutoExpandAllNodes="true" />
                                </dxwtl:ASPxTreeList>
                            </dxw:ContentControl>
                        </ContentCollection>
                    </dxtc:TabPage>
                </TabPages>
                </dxtc:ASPxPageControl>
                <table width="100%" runat="server" id="tblDownload">
                    <tr>
                        <td align="right">
                            <dxe:ASPxHyperLink ForeColor="ActiveCaption" ID="lnkReq" runat="server" Text="下载请求数据" NavigateUrl="DownloadAudit.aspx?Type=Req&ID=" Target="_blank" />
                            <dxe:ASPxHyperLink ForeColor="ActiveCaption" ID="lnkRes" runat="server" Text="下载响应数据" NavigateUrl="DownloadAudit.aspx?Type=Res&ID=" Target="_blank" />
                        </td>
                    </tr>
                </table>
                </div>
                </EditForm>
            </Templates>
            <%-- EndRegion --%>
            <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="600px" />
            <SettingsPager AlwaysShowPager="true" Mode="ShowPager" />
            <SettingsText EmptyDataRow="暂无数据！" CommandCancel="取消" CommandUpdate="保存" PopupEditFormCaption="查看详情" />
        </dxwgv:ASPxGridView>   
        <script type="text/javascript">
            //调用条件：页面包含ASPxLoadingPanel ClientInstanceName="LoadingPanel"
            InitCustomLoadingPanel(); 
        </script>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <%-- BeginRegion DataSource --%>
    <asp:ObjectDataSource ID="OdsAuditTrace" runat="server" 
        TypeName="ESB.AuditService" 
        DataObjectTypeName="ESB.AuditBusiness" OnSelecting="OdsAuditTrace_Selecting"
        SelectMethod="GetAuditListByTraceID">
        <SelectParameters>
            <asp:SessionParameter Name="traceID" SessionField="TraceID" Type="String" />
        </SelectParameters>
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
    <asp:ObjectDataSource ID="OdsAuditBusiness" runat="server" 
        TypeName="ESB.AuditService"
        DataObjectTypeName="ESB.AuditBusiness"
        SelectMethod="AuditBusinessSearch"
        OnSelecting="OdsAuditBusiness_Selecting"
        EnablePaging="True" 
        MaximumRowsParameterName="pageSize" 
        StartRowIndexParameterName="pageIndex" 
        SelectCountMethod="GetAuditBusinessCount">
    </asp:ObjectDataSource>
    <%-- EndRegion DataSource --%>
</asp:Content>