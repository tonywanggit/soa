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
        var url; // 服务绑定地址
        var esbHostIp1 = "localhost"; // ESB主机1
        var esbHostIp2 = "localhost"; // ESB主机2
    
        // 响应表格中的自定义按钮事件
        function OnCustomButtonClick(s, e) {
            grid.GetRowValues(e.visibleIndex, "访问地址;状态"
                , function(values) {
                    url = values[0];
                    var status = values[1];

                    if (status == 0) { // 启用
                        popClearCache.Show();
                    } else {
                        alert("此地址已经被停用，无法清除其缓存！");
                    }
                }
            ); 
        
        }

        // 响应清除按钮单击事件
        function OnClearCache(s, e) {
            var serviceName = cbService.GetText();
            var cmdName = s.name;
            
            if (/1$/.test(cmdName))
                ClearAssemblyCache(esbHostIp1, url, serviceName, s);
            else
                ClearAssemblyCache(esbHostIp2, url, serviceName, s); 
        }

        // 一键清除
        function OnClearAllCache(s, e) {
            var serviceName = cbService.GetText();

            ClearAssemblyCache(esbHostIp1, url, serviceName, cmdClearCache1);
            ClearAssemblyCache(esbHostIp2, url, serviceName, cmdClearCache2); 
        }

        // 清除程序集缓存
        function ClearAssemblyCache(hostName, url, serviceName, cmd) {
            cmd.SetEnabled(false);
            $.getJSON("http://" + hostName + "/EsbCore/ClearAssemblyCache.aspx?Action=RemoveCache&callback=?",
                { Url: url, ServiceName: serviceName },
                function(data) {
                    var esbHostName = /1$/.test(cmd.name) ? "ESB主机一：" : "ESB主机二：";
                    alert(esbHostName + data.result);
                    
                    cmd.SetEnabled(true);
                }
            );
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="phContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <dxlp:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="False" />
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
    <ContentTemplate>
        <dxpc:ASPxPopupControl ID="popClearCache" Modal="false" runat="server" Width="300" Height="150" HeaderText="清除代理程序集缓存"
            ClientInstanceName="popClearCache" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentStyle Paddings-PaddingRight="0"></ContentStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <table cellpadding="3" cellspacing="0">
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel7" Text="ESB主机一: 10.30.4.103" runat="server" /></td>
                            <td style="padding-top: 3px">
                                <dxe:ASPxButton Text="清除缓存" ID="cmdClearCache1" ClientInstanceName="cmdClearCache1" runat="server" AutoPostBack="false">
                                    <ClientSideEvents Click="OnClearCache" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel9" Text="ESB主机二: 10.30.4.104" runat="server" /></td>
                            <td style="padding-top: 3px">
                                <dxe:ASPxButton Text="清除缓存" ID="cmdClearCache2" ClientInstanceName="cmdClearCache2" runat="server" AutoPostBack="false">
                                    <ClientSideEvents Click="OnClearCache" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                        <tr><td colspan="3" style="height:10px"></td></tr>
                        <tr>
                            <td class="buttonCell" colspan="2" >
                                <dxe:ASPxButton Text="一键清除" ID="cmdClearAllCache" runat="server" AutoPostBack="false">
                                    <ClientSideEvents Click="OnClearAllCache" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl> 
    
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbProvider" runat="server" ToolTip="请选择服务提供者" AutoPostBack="true" DataSourceID="OdsProvider" 
                        ValueField="业务编码" TextField="描述" OnSelectedIndexChanged="cbProvider_SelectedIndexChanged" />
                </td> 
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbService" ClientInstanceName="cbService" runat="server" ToolTip="请选择具体服务" AutoPostBack="true" DataSourceID="OdsService" 
                        ValueField="服务编码" TextField="描述" AutoResizeWithContainer="true" DropDownStyle="DropDownList" OnSelectedIndexChanged="cbService_SelectedIndexChanged" TextFormatString="{0}">
                        <Columns>
                            <dxe:ListBoxColumn Caption="服务名称" FieldName="服务名称" ToolTip="服务名称" Width="100px" />
                            <dxe:ListBoxColumn Caption="服务描述" FieldName="描述" ToolTip="服务描述" Width="200px" />
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
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsBinding" KeyFieldName="服务地址编码" AutoGenerateColumns="False" Width="900">
            <%-- BeginRegion Columns --%>
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="70">
                    <EditButton Visible="True" Text="编辑" />
                    <DeleteButton Visible="true" Text="删除" />
                </dxwgv:GridViewCommandColumn>               
                <dxwgv:GridViewDataHyperLinkColumn FieldName="访问地址" Caption="访问地址" VisibleIndex="1" >
                    <PropertiesHyperLinkEdit TextField="访问地址" TextFormatString="{0}" NavigateUrlFormatString="{0}" Target="_blank" >
                    </PropertiesHyperLinkEdit>
                    <EditFormSettings ColumnSpan="2" VisibleIndex="3" />
                </dxwgv:GridViewDataHyperLinkColumn>
                                
                <dxwgv:GridViewDataMemoColumn FieldName="描述" VisibleIndex="2" Caption="绑定描述" >
                    <EditFormSettings ColumnSpan="2" VisibleIndex="4" CaptionLocation="Near" />
                    <EditFormCaptionStyle VerticalAlign="Top" />
                    <PropertiesMemoEdit Height="80px" />
                </dxwgv:GridViewDataMemoColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="绑定类型" VisibleIndex="3" Caption="绑定类型">
                    <PropertiesComboBox TextField="Text" ValueField="Value" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="xdsBindingType">
                    </PropertiesComboBox>
                    <EditFormSettings VisibleIndex="1" />
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="状态" VisibleIndex="4" Caption="状态">
                    <PropertiesComboBox TextField="Text" ValueField="Value" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="xdsBindingStatus">
                    </PropertiesComboBox>
                    <EditFormSettings VisibleIndex="2" />
                </dxwgv:GridViewDataComboBoxColumn>    
            </Columns>
            <%-- EndRegion --%>
            <SettingsDetail ShowDetailRow="true"/>
            <SettingsEditing Mode="EditFormAndDisplayRow"/>
            <SettingsPager AlwaysShowPager="true" />
            <SettingsText EmptyDataRow="暂无数据！" CommandCancel="取消" CommandUpdate="保存" ConfirmDelete="您确定要删除这条记录吗？" />
            <SettingsBehavior ConfirmDelete="true" />
            <ClientSideEvents CustomButtonClick="OnCustomButtonClick" />
            <Templates>
                <DetailRow>
                <div style="padding:3px 3px 2px 3px">
                    <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true">
                    <TabPages>
                        <dxtc:TabPage Text="绑定约束" Visible="true">
                            <ContentCollection><dxw:ContentControl runat="server">
                                <dxwgv:ASPxGridView  ClientInstanceName="gridTmodel" ID="gridTmodel" runat="server" DataSourceID="OdsTModel" 
                                    KeyFieldName="约束编码" Width="100%" OnBeforePerformDataSelect="gridTmodel_DataSelect" >
                                    <%-- BeginRegion Grid Columns --%>
                                    <Columns>                
                                        <dxwgv:GridViewCommandColumn VisibleIndex="0" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="90px">
                                            <EditButton Visible="True" Text="编辑" />
                                            <DeleteButton Visible="true" Text="删除" />
                                            <NewButton Visible="true" Text="新增"/>
                                        </dxwgv:GridViewCommandColumn>
                                        <dxwgv:GridViewDataColumn FieldName="描述" VisibleIndex="0" Caption="方法名称">
                                            <EditFormSettings ColumnSpan="2" VisibleIndex="0" />
                                        </dxwgv:GridViewDataColumn>
                                        <dxwgv:GridViewDataMemoColumn FieldName="示例" VisibleIndex="1" Caption="约束示例">
                                            <EditFormSettings ColumnSpan="2" VisibleIndex="1" CaptionLocation="Near" />
                                            <EditFormCaptionStyle VerticalAlign="Top" />
                                            <PropertiesMemoEdit Height="80px" />
                                        </dxwgv:GridViewDataMemoColumn>
                                    </Columns>
                                    <%-- EndRegion --%>
                                    <SettingsText EmptyDataRow=" " CommandCancel="取消" CommandUpdate="保存" />
                                </dxwgv:ASPxGridView>
                            </dxw:ContentControl></ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Text="基本信息"  Visible="true">
                            <ContentCollection><dxw:ContentControl runat="server">
                                <Table cellpadding="2" cellspacing="1" style="border-collapse: collapse; width:100%">
                                    <tr>
                                        <td style="font-weight:bold;width:70px;height:30px">访问地址:</td>
                                        <td align="left"><%# Eval("访问地址")%></td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight:bold;" valign="top">绑定描述:</td>
                                        <td align="left"><%# Eval("描述")%></td>
                                    </tr>
                                </Table>
                            </dxw:ContentControl></ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                </dxtc:ASPxPageControl>
                </div>
                </DetailRow>
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
        SelectMethod="获得所有服务提供者">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsService" runat="server" 
        TypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.注册服务目录服务"
        SelectMethod="获得具体服务_服务提供者" OnSelecting="OdsService_Selecting">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsBinding" runat="server" 
        TypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.注册服务目录服务"
        DataObjectTypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.服务地址"
        SelectMethod="获得绑定信息_具体服务"  UpdateMethod="修改绑定方法" InsertMethod="新增服务地址" DeleteMethod="删除绑定方法"
        OnSelecting="OdsBinding_Selecting" OnUpdating="OdsBinding_Updating" OnInserting="OdsBinding_Inserting">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsTModel" runat="server" 
        TypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.注册服务目录服务"
        DataObjectTypeName="JN.Esb.Portal.ServiceMgt.服务目录服务.服务约束"
        SelectMethod="获得服务约束＿服务地址"  UpdateMethod="修改服务约束" InsertMethod="新增服务约束" DeleteMethod="删除服务约束"
        OnSelecting="OdsTModel_Selecting" OnUpdating="OdsTModel_Updating" OnInserting="OdsTModel_Inserting">
    </asp:ObjectDataSource>
    <asp:XmlDataSource DataFile="~/App_Data/BindingStatusEnum.xml" XPath="//Binding" ID="xdsBindingStatus" runat="server" />
    <asp:XmlDataSource DataFile="~/App_Data/BindingTypeEnum.xml" XPath="//BindingType" ID="xdsBindingType" runat="server" />
    <%-- EndRegion DataSource --%>
</asp:Content>

