﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ww_classobjs.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Run Triangle Solver" Width="301px" /><br />

            <asp:TextBox ID="TextBox1" runat="server" Height="310px" TextMode="MultiLine" Width="100%" Visible="False"></asp:TextBox>
        </div>
    </form>
</body>
</html>