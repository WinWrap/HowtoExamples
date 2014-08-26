<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogPage.aspx.cs" Inherits="ww2014b.LogPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="StyleSheet1.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" Width="100%" Height="79px" />
            <br />
            <pre class="mobile"><% Response.Write(Session["Error"].ToString()); %>
            </pre>
        </div>
    </form>
</body>
</html>
