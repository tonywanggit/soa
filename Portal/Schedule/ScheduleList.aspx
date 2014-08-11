<%@ Page Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="ScheduleList.aspx.cs" Inherits="Schedule_ScheduleList" %>
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
    <script type="text/javascript">
    function ChangeService(control) {
        if(control.GetValue() != "") {
            cbService.ClearItems();
            cbService.PerformCallback(control.GetValue());
            cbService.SetFocus();
        }
    }
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
                    <dxe:ASPxComboBox ID="cbProvider" runat="server" ToolTip="请选择调度系统" AutoPostBack="true" DataSourceID="OdsProvider" 
                        ValueField="业务编码" TextField="描述" TextFormatString="{0}({1})" Width="205px" AutoResizeWithContainer="true" DropDownStyle="DropDownList"
                        OnSelectedIndexChanged="cbProvider_SelectedIndexChanged" ValueType="System.String"
                        >
                        <Columns>
                            <dxe:ListBoxColumn Caption="系统代码" FieldName="业务名称" ToolTip="系统代码" Width="50px" />
                            <dxe:ListBoxColumn Caption="系统名称" FieldName="描述" ToolTip="系统名称" Width="155px" />     
                        </Columns>               
                    </dxe:ASPxComboBox>    
                </td>
                <td class="buttonCell">
                    <dxe:ASPxButton ID="btnAdd" runat="server" Text="新增调度" UseSubmitBehavior="False" AutoPostBack="false">
                        <ClientSideEvents Click="function(){ 
                            grid.AddNewRow(); 
                        }" />
                    </dxe:ASPxButton>
                </td>     
                </td>
            </tr>
        </table>
        <br />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsHostScheduler" KeyFieldName="SCHD_ID" AutoGenerateColumns="False" Width="900"
            OnCustomButtonInitialize="grid_OnCustomButtonInitialize" OnCustomButtonCallback="grid_OnCustomButtonCallback"
            OnCustomColumnDisplayText="grid_OnCustomColumnDisplayText" OnInitNewRow="grid_InitNewRow" OnRowValidating="grid_RowValidating"
            OnCellEditorInitialize="grid_CellEditorInitialize" 
        >
            <%-- BeginRegion Columns --%>
            <Columns>
               <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="85">
                    <EditButton Visible="True" Text="编辑" />
                    <DeleteButton Visible="true" Text="删除" />
                    <CustomButtons>
                        <dxwgv:GridViewCommandColumnCustomButton ID="btnPauseOrStart" Visibility="AllDataRows" Text="停用" />
                    </CustomButtons>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataColumn FieldName="SCHD_NAME" Caption="调度名称" VisibleIndex="1">
                    <EditFormSettings VisibleIndex="1" Visible="true" ColumnSpan="2" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="SCHD_FREQ" VisibleIndex="2" Caption="调度频率" >
                    <PropertiesComboBox TextField="Text" ValueField="Value" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="xdsScheduleFrequency">
                    </PropertiesComboBox>
                    <EditFormSettings VisibleIndex="4" CaptionLocation="Near"  Visible="true" />
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="TRIG_GROUP" VisibleIndex="3" Caption="调度类别" >
                    <PropertiesComboBox TextField="Text" ValueField="Value" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="xdsScheduleType">     
                    </PropertiesComboBox>
                    <EditFormSettings VisibleIndex="6" CaptionLocation="Near"  Visible="true" />
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataColumn FieldName="TRIGGER_STATE" VisibleIndex="4" Caption="当前状态" ReadOnly="true">
                    <EditFormSettings VisibleIndex="4" CaptionLocation="Near"  Visible="false" />
                    <EditFormCaptionStyle VerticalAlign="Top" />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataDateColumn FieldName="PREV_FIRE_TIME" VisibleIndex="5" Caption="上次调用时间" ReadOnly="true">
                    <EditFormSettings VisibleIndex="5" Visible="False" />
                    <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn FieldName="NEXT_FIRE_TIME" VisibleIndex="6" Caption="下次调用时间" ReadOnly="true">
                    <EditFormSettings VisibleIndex="6" Visible="False" />
                    <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataColumn FieldName="SCHD_USER" VisibleIndex="7" Caption="编写者" ReadOnly="true">
                    <EditFormSettings VisibleIndex="9" Visible="False"  />
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataMemoColumn FieldName="SCHD_DESC" Caption="调度描述" Visible="false" VisibleIndex="1" >
                    <PropertiesMemoEdit Height="60px" />
                    <EditFormCaptionStyle VerticalAlign="Top" />
                    <EditFormSettings VisibleIndex="2" Visible="true" ColumnSpan="2"  />
                </dxwgv:GridViewDataMemoColumn> 
                <dxwgv:GridViewDataComboBoxColumn FieldName="SCHD_HOST" Caption="调度系统" Visible="false" VisibleIndex="1" >
                    <PropertiesComboBox TextField="业务名称"  ValueField="业务编码" DataSourceID="OdsProvider"
                        TextFormatString="{0}({1})" >
                        <Columns>
                            <dxe:ListBoxColumn Caption="系统代码" FieldName="业务名称" ToolTip="系统代码" Width="50px" />
                            <dxe:ListBoxColumn Caption="系统名称" FieldName="描述" ToolTip="系统名称" Width="100px" />     
                        </Columns>  
                    </PropertiesComboBox>
                    <EditFormSettings VisibleIndex="3" Visible="true" CaptionLocation="Near"  />
                </dxwgv:GridViewDataComboBoxColumn>  
                <dxwgv:GridViewDataColumn FieldName="SCHD_CRON" Caption="调度计划" Visible="false" VisibleIndex="1" >
                    <EditFormSettings VisibleIndex="5" Visible="true"  />
                </dxwgv:GridViewDataColumn>   
                <dxwgv:GridViewDataColumn FieldName="START_TIME" Caption="开始时间" VisibleIndex="1" Visible="false">
                    <EditFormSettings VisibleIndex="7" Visible="true" />
                </dxwgv:GridViewDataColumn>     
                <dxwgv:GridViewDataColumn FieldName="END_TIME" Caption="结束时间" Visible="false" VisibleIndex="1" >
                    <EditFormSettings VisibleIndex="8" Visible="true" />
                </dxwgv:GridViewDataColumn>                
            </Columns>
            <%-- EndRegion --%>
            <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="600px" />
            <SettingsPager AlwaysShowPager="true" />
            <SettingsText EmptyDataRow="暂无数据！" CommandCancel="取消" CommandUpdate="保存" ConfirmDelete="您确定要删除这条纪录？" PopupEditFormCaption="调度任务" />
            <SettingsBehavior ConfirmDelete="true" />
            <Templates>
                <EditForm>
                <div style="padding:3px 3px 2px 3px">
                    <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true">
                    <TabPages>
                        <dxtc:TabPage Text="调度信息" Visible="true">
                            <ContentCollection><dxw:ContentControl runat="server">
                            <dxwgv:ASPxGridViewTemplateReplacement ID="Editors" ReplacementType="EditFormEditors" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                            </dxw:ContentControl></ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Text="任务信息"  Visible="true">
                            <ContentCollection><dxw:ContentControl runat="server">
                                <Table cellpadding="2" cellspacing="1" style="border-collapse: collapse; width:100%; ">
                                    <tr>
                                        <td style="width:70px;height:30px">调用实体:</td>
                                        <td align="left">
                                            <dxe:ASPxComboBox ID="cbEntity" runat="server" ToolTip="请选择调度系统" AutoPostBack="false" DataSourceID="OdsProvider" 
                                                ValueField="业务编码" TextField="描述" TextFormatString="{0}({1})" Width="215px" AutoResizeWithContainer="true" DropDownStyle="DropDownList"
                                                >
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { ChangeService(s); }">
                                                </ClientSideEvents>
                                                <Columns>
                                                    <dxe:ListBoxColumn Caption="系统代码" FieldName="业务名称" ToolTip="系统代码" Width="50px" />
                                                    <dxe:ListBoxColumn Caption="系统名称" FieldName="描述" ToolTip="系统名称" Width="100px" />     
                                                </Columns>   
                                            </dxe:ASPxComboBox>                                       
                                        </td>
                                        <td style="width:70px;height:30px" align="center">调用服务:</td>                                        
                                        <td align="left">
                                            <dxe:ASPxComboBox ID="cbService" ClientInstanceName="cbService" runat="server" ToolTip="请选择调度服务" AutoPostBack="false" DataSourceID="OdsService" 
                                                ValueField="服务编码" TextField="服务名称" TextFormatString="{0}" Width="210px" AutoResizeWithContainer="true" DropDownStyle="DropDownList"
                                                OnCallback="cbService_Callback"
                                                >
                                                <Columns>
                                                    <dxe:ListBoxColumn Caption="服务名称" FieldName="服务名称" ToolTip="服务名称" Width="70px" />
                                                    <dxe:ListBoxColumn Caption="服务描述" FieldName="描述" ToolTip="服务描述" Width="80px" />
                                                </Columns>
                                            </dxe:ASPxComboBox>                                          
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:70px;height:30px">调用方法:</td>
                                        <td align="left">
                                            <dxe:ASPxTextBox ID="txtMethodName" runat="server" ToolTip="请输入调用方法" Width="215px"  />
                                        </td>
                                        <td style="width:70px;height:30px" align="center">调用密钥:</td>                                        
                                        <td align="left">
                                            <dxe:ASPxTextBox ID="txtPassWord" runat="server" ToolTip="请输入调用密钥" Width="210px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height:130px" valign="top">调用参数:</td>
                                        <td colspan="3" valign="top">
                                            <dxe:ASPxMemo ID="txtParam" runat="server" ToolTip="请输入调用参数" Height="120px" Width="504px" />
                                        </td>
                                    </tr>
                                </Table>
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
        TypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.注册服务目录服务" 
        DataObjectTypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.业务实体" 
        SelectMethod="获得所有服务提供者"
        >
    </asp:ObjectDataSource>
   
    <asp:ObjectDataSource ID="OdsHostScheduler" runat="server" 
        TypeName="JN.Esb.Portal.ServiceMgt.调度服务.SchedulerService"
        SelectMethod="GetSchedulerByHost" InsertMethod="AddEsbWebServcieScheduler" UpdateMethod="UpdateEsbWebServcieScheduler" DeleteMethod="DeleteEsbWebServcieScheduler"
        OnSelecting="OdsHostScheduler_Selecting" OnInserting="OdsHostScheduler_Inserting" OnUpdating="OdsHostScheduler_OnUpdating" OnDeleting="OdsHostScheduler_OnDeleting"
        OnUpdated="OdsHostScheduler_OnUpdated"
        >
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsService" runat="server" 
        TypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.注册服务目录服务"
        SelectMethod="获得具体服务_服务提供者" OnSelecting="OdsService_Selecting">
    </asp:ObjectDataSource>    
    <asp:XmlDataSource DataFile="~/App_Data/ScheduleTypeEnum.xml" XPath="//ScheduleType" ID="xdsScheduleType" runat="server" />
    <asp:XmlDataSource DataFile="~/App_Data/ScheduleFrequencyTypeEnum.xml" XPath="//ScheduleFrequency" ID="xdsScheduleFrequency" runat="server" />
    
    <%-- EndRegion DataSource --%>
</asp:Content>