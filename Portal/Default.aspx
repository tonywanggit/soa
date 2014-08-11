<%@ Page Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_DefaultPage" %>
<%@ Register assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxDataView" TagPrefix="dxdv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phOnceContent" Runat="Server">
    <div class="RootPage">
        <div class="Slogan">集成应用<br /><span class="MiniSlogan">连接IT孤岛，串起一颗颗信息化珍珠！</span></div>
        <dxe:ASPxImage ID="Image1" SkinID="MainBanner" CssClass="MainBanner" ImageAlign="Right" runat="server" EnableViewState="False" Width="453px" Height="235px" ImageUrl="~/Images/Main.png" />
        <br class="Clear" />
        <asp:Literal ID="lGeneralTerms" EnableViewState="False" runat="server" />
        <br /><br /><br />
        <dxdv:ASPxDataView EnableDefaultAppearance="False" ID="ASPxDataView1" runat="server" DataSourceID="XmlDataSource1" Width="100%" CssClass="Features" ColumnCount="2" ItemSpacing="43px" SkinID="None" RowPerPage="100">
            <ItemTemplate>
                <table cellpadding="0" cellspacing="0" border="0" width="100%"><tr>
                    <td valign="top" runat="server" id="td1" EnableViewState="False" Visible='<%# IsImageVisible(Eval("Visible")) %>'><dxe:ASPxImage ID="Image2" runat="server" EnableViewState="False" Width="73px" Height="82px" ImageUrl='<%# Eval("ImageUrl") %>' /></td>
                    <td runat="server" id="td2" EnableViewState="False" Visible='<%# IsImageVisible(Eval("Visible")) %>'><div class="Spacer" style="width: 8px;"></div></td>
                    <td valign="top" style="width: 100%;"><b><dxe:ASPxLabel EnableDefaultAppearance="False" EnableViewState="False" ID="Label1" runat="server" Text='<%# Eval("Title") %>' /></b><dxe:ASPxLabel EnableDefaultAppearance="False" EnableViewState="False" Font-Size="13px" ID="Label2" runat="server" Text='<%# Eval("Text") %>' /></td>
                </tr></table>
            </ItemTemplate>
            <PagerSettings Visible="False">
            </PagerSettings>
            <ItemStyle Width="50%" />
        </dxdv:ASPxDataView>       
        <%-- BeginRegion DataSource --%>
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/MainFeatures.xml" XPath="//Item" />
        <%-- EndRegion --%>
        <br /><br />
        <asp:Panel ID="pDescription" EnableViewState="False" runat="server" />
    </div>
</asp:Content>
