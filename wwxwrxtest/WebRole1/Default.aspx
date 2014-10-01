<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebRole1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Version <%=WebRole1.MessengerService.version%></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Direct Call" />
        <br />
        <a href="?">Refresh</a> <a href="?ResetTrace=yes">Reset</a>
        <br />
        <pre><%=Server.HtmlEncode(WebRole1.MessengerService.ReadLog())%></pre>
    </div>
    </form>
</body>
</html>
