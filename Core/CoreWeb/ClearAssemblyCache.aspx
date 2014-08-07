<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClearAssemblyCache.aspx.cs" Inherits="ESB.CallCenter.ClearAssemblyCache" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblConsole" runat="server" Text="当前程序集缓存数："></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="txtServiceName" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" Text="清空缓存" />
        &nbsp;&nbsp;
        <asp:Button ID="btnShowCache" runat="server" onclick="btnShowCache_Click" Text="查看缓存" />
        &nbsp;
        <asp:Button ID="btnCompiler" runat="server" onclick="btnCompiler_Click" Text="并发编译" Visible="False" />
        &nbsp;
        <asp:Button ID="btnShowAssemblyList" runat="server"  Text="查看程序集端口缓存列表" onclick="btnShowAssemblyList_Click" />
        <br />
        <br />
        <asp:Label ID="lblAssemblyList" runat="server" Text="当前程序集端口缓存列表："></asp:Label>
        
    </div>
    </form>
</body>
</html>
