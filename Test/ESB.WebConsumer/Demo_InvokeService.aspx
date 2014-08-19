<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo_InvokeService.aspx.cs" Inherits="ESB.CallCenter.Demo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ESB调用中心演示用例</title>
    <script src="Script/jquery-2.1.0.min.js" type="text/javascript"></script>
    <script src="http://192.168.56.1/CallCenter/Script/mb-esb-1.0.0.js" type="text/javascript"></script>
    <script type="text/javascript">
        function SendToESB() {
            var serviceName = $("#txtServiceName").val();
            var methodName = $("#txtMethodName").val();
            var message = $("#txtSendMessage").val();


            $.esb.invoke(serviceName, methodName, message, function (response) {
                $("#txtReceiveMessage").val(response);
            });
        }
    </script>

</head>
<body>
    
    <form id="form1" runat="server">
        服务名称：<asp:TextBox ID="txtServiceName" runat="server" Width="258px">WXSC_WeiXinServiceForApp</asp:TextBox>
        <br />
        <br />
        方法名称：<asp:TextBox ID="txtMethodName" runat="server" Width="258px">GET:XML:CollocationDetailFilter</asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="发送数据："></asp:Label>
        <br />
        <asp:TextBox ID="txtSendMessage" runat="server" Height="144px" Width="474px" TextMode="MultiLine">collocationId=11</asp:TextBox>
        <br />
        <br />
        <input type="button" id="btnSend" onclick="SendToESB()" value="发送" />
        <br />
        <br />
        接收数据：<br />
        <asp:TextBox ID="txtReceiveMessage" runat="server" Height="144px" Width="474px" TextMode="MultiLine"></asp:TextBox>
    </form>
    
</body>
</html>
