<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebRole1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Version 10</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Width="269px" />
        <a href="?">Refresh</a> <a href="?ResetTrace=yes">Reset</a>
        <br />
        <pre><%=Server.HtmlEncode(ReadLog())%></pre>
    </div>
    </form>
</body>
</html>
