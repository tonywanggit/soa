<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="CallNumAnalyse.aspx.cs" Inherits="Analyse_CallNumAnalyse" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.XtraCharts.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.XtraCharts.v9.1.Web, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dxchartsui" %>  
<%@ Register assembly="DevExpress.XtraCharts.v9.1" namespace="DevExpress.XtraCharts" tagprefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phContent" Runat="Server">
    <dxm:ASPxMenu Width="100%" HorizontalAlign="Left"  SkinID="ChartDemoToolbar" ID="mnuToolbar" runat="server" ClientInstanceName="mnuToolbar" AutoSeparators="None" ApplyItemStyleToTemplates="True">
        <Items>
            <dxm:MenuItem Name="mnuPrint" Text="" ToolTip="Print the chart" >
                <Image Url="~/Images/Toolbar/BtnPrint.png" Width="16px" Height="16px"/>
            </dxm:MenuItem>
            <dxm:MenuItem Name="mnuSaveToDisk" Text="" ToolTip="导出图表到本地" BeginGroup="True">
                <Image Url="~/Images/Toolbar/BtnSave.png" Width="16px" Height="16px"/>
            </dxm:MenuItem>
            <dxm:MenuItem Name="mnuSaveToWindow" Text="" ToolTip="导出图表到新窗口">
                <Image Url="~/Images/Toolbar/BtnSaveWindow.png" Width="16px" Height="16px"/>
            </dxm:MenuItem>
            <dxm:MenuItem Name="mnuFormat"><Template>
                <dxe:ASPxComboBox runat="server" Width="60px" ValueType="System.String" ID="cbFormat" SelectedIndex="2" ClientInstanceName="cbFormat">
                    <Items>
                        <dxe:ListEditItem Value="pdf" Text="pdf" />
                        <dxe:ListEditItem Value="xls" Text="xls" />
                        <dxe:ListEditItem Value="png" Text="png" />
                        <dxe:ListEditItem Value="jpeg" Text="jpeg" />
                        <dxe:ListEditItem Value="bmp" Text="bmp" />
                        <dxe:ListEditItem Value="tiff" Text="tiff" />
                        <dxe:ListEditItem Value="gif" Text="gif" />
                    </Items>
                </dxe:ASPxComboBox>
            </Template></dxm:MenuItem>     
            <dxm:MenuItem Name="mnuLblType" BeginGroup="True">
                <Template><dxe:ASPxLabel ID="lblType" Cursor="auto" Text="&nbsp;统计类别:" runat="server" /></Template>
            </dxm:MenuItem>
            <dxm:MenuItem Name="mnuType"><Template>
                <dxe:ASPxComboBox runat="server" Width="100px" ValueType="System.String" ID="cbType" SelectedIndex="0" ClientInstanceName="cbType">
                    <Items>
                        <dxe:ListEditItem Value="all" Text="全部" />
                        <dxe:ListEditItem Value="xls" Text="按服务细分" />
                    </Items>
                    <ClientSideEvents SelectedIndexChanged="function(s, e) { chart.PerformCallback(&quot;Type&quot;); }" />
                </dxe:ASPxComboBox>
            </Template></dxm:MenuItem>                    
            <dxm:MenuItem Name="mnuLblSpace">
                <Template><dxe:ASPxLabel ID="lblSpace" Width="400px" Text="" runat="server"/></Template>
            </dxm:MenuItem>
        </Items>    
        <ClientSideEvents ItemClick="function(s, e) {
            if (e.item.name == 'mnuPrint')
	            chart.Print();
            if (e.item.name == 'mnuSaveToDisk')
                chart.SaveToDisk(cbFormat.GetText());
            if (e.item.name == 'mnuSaveToWindow')
                chart.SaveToWindow(cbFormat.GetText());
        }" />
    </dxm:ASPxMenu>
    <br />    
    <dxchartsui:WebChartControl ID="chart" runat="server" Height="600px" Width="900px" ClientInstanceName="chart" OnCustomCallback="chart_CustomCallback" >
        <BorderOptions Visible="False" />
        <Legend AlignmentVertical="Top"></Legend>
    </dxchartsui:WebChartControl>    
</asp:Content>

