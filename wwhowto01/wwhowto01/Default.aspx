<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ww_classobjs.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            

            <asp:Table ID="Table1" runat="server" Height="69px" Width="100%">
                <asp:TableRow runat="server">
                    <asp:TableCell RowSpan="2" runat="server">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                            <asp:ListItem Value="SSS" Selected="True">SSS</asp:ListItem>
                            <asp:ListItem Value="SAS">SAS</asp:ListItem>
                            <asp:ListItem Value="SSA">SSA</asp:ListItem>
                            <asp:ListItem Value="AAA">AAA</asp:ListItem>
                            <asp:ListItem Value="ASA">ASA</asp:ListItem>
                            <asp:ListItem Value="AAS">AAS</asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                    <asp:TableCell runat="server">image</asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Solve" Width="301px" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <asp:Label ID="LabelSides" runat="server" Text="Sides"></asp:Label><br />
                        a&nbsp;<asp:TextBox ID="TextBoxSideA" runat="server" Text="3"></asp:TextBox><br />
                        b&nbsp;<asp:TextBox ID="TextBoxSideB" runat="server" Text="4"></asp:TextBox><br />
                        c&nbsp;<asp:TextBox ID="TextBoxSideC" runat="server" Text="5"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:Label ID="LabelAngles" runat="server" Text="Angles"></asp:Label><br />
                        A&nbsp;<asp:TextBox ID="TextBoxAngleA" runat="server" Text="" Enabled="False"></asp:TextBox><br />
                        B&nbsp;<asp:TextBox ID="TextBoxAngleB" runat="server" Text="" Enabled="False"></asp:TextBox><br />
                        C&nbsp;<asp:TextBox ID="TextBoxAngleC" runat="server" Text="" Enabled="False"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server"></asp:TableCell>
                    <asp:TableCell runat="server"></asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            
            <br />
            <asp:TextBox ID="TextBox1" runat="server" Height="310px" TextMode="MultiLine" Width="100%" Visible="False"></asp:TextBox>
        </div>
    </form>
</body>
</html>
