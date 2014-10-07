<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ww_ws_objmod.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.8.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Top1").val($("#Button1").offset().top);
            $("#Width1").val($(window).width());
            $("#Height1").val($(window).height());
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Width="468px" Font-Size="XX-Large" Height="84px" /><br />
            <asp:Image ID="Image2" runat="server" /><br />
            <asp:TextBox ID="TextBox1" runat="server" Height="310px" TextMode="MultiLine" Width="100%" Visible="False"></asp:TextBox><br />
            <asp:HiddenField ID="Width1" runat="server" />
            <asp:HiddenField ID="Top1" runat="server" />
            <asp:HiddenField ID="Height1" runat="server" />
        </div>
    </form>
</body>
</html>
