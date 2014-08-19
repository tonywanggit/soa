<%@ Page Language="C#" AutoEventWireup="true" Inherits="LoginPage" EnableEventValidation="false" CodeFile="Login.aspx.cs" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dxlp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>企业服务总线 - 安全校验</title>
	<link rel="Stylesheet" href="CSS/LoginPage.css" type="text/css" />
	<script type="text/javascript" src="Scripts/Utilities.js"></script>
</head>
<body class="Dialog" style="background: none white">
   <div id="PageContent" class="PageContent DialogPageContent">
	<form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager" runat="server" />
       <dxlp:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="True" />
        <div class="Header">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td class="ViewImage" style="width:90px"><img src="Images/ImageResource.gif" /></td>
                    <td class="ViewCaption">
                        <h1><asp:Literal ID="ViewCaptionLabel" runat="server" Text="安全校验"/></h1>
                    </td>
                </tr>
            </table>
        </div>

        <table class="DialogContent Content" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="height:50px;">
                </td>
            </tr>            
            <tr>
                <td class="LogonContentCell" align="center">
                    <div id="LoginContent">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="height:150px;width:320px"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="height:180px;"></td>
                                <td valign="top">      
                                <asp:UpdatePanel ID="UpdatePanel" runat="server"> 
                                <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="height:20px;width:50px"></td>
                                            <td align="left" valign="bottom"><asp:Label ID="errorLabel" runat="server" ForeColor="#ff3300"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="height:30px;width:50px">域名:</td>
                                            <td align="left"><asp:TextBox ID="txtDomain" runat="server" Text="mb.com" Enabled="false" Width="150px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="height:30px;width:50px">用户:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtUsername" runat="server" Width="150px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                    ControlToValidate="txtUsername" ErrorMessage="*"></asp:RequiredFieldValidator>                                       
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height:30px;width:50px">密码:</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                    ControlToValidate="txtPassword" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="left" style="padding-left:7px"><asp:CheckBox ID="chkPersist" runat="server" Checked="true" Text="记住我的登陆名" /></td>
                                        </tr>
                                        <tr>
                                            <td style="height:30px;width:50px"></td>
                                            <td><asp:Button ID="btnLogin" runat="server" CssClass="login_btn" OnClick="btnLogin_Click" /></td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="height:45px;"></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td  align="left" style="color:White;">
                                    &nbsp;&nbsp;[调用中心: <a href="http://localhost/EsbCore/Core_Service_Bus_MainBus_ReceiveSendPort.asmx" target="_blank" style="color:White;">双向</a>
                                    <a href="http://localhost/EsbCore/Core_Service_Bus_OnewayMainBus_OneWayReceive.asmx" target="_blank" style="color:White;">单向</a>]
                                </td>
                                <td align="right" style="color:White;padding-right: 10px;">版权所有 © 美特斯邦威服饰股份有限公司</td>
                            </tr>
                        </table>
                    </div>
                </td>
          </table>
	</form>
    <script type="text/javascript">
    <!--
        //调用条件：页面包含ASPxLoadingPanel ClientInstanceName="LoadingPanel"
        InitCustomLoadingPanel(function(){
            LoadingPanel.Show();
        });
        
        document.getElementById("txtUsername").focus();
    //-->	    
    </script>
    </div>
</body>

</html>