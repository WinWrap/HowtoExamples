<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ww2014b.WebForm1" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="StyleSheet1.css" />
    <title></title>
    <!--<script>
        //alert("hi")
        function onPositionUpdate(position) {
            var lat = position.coords.latitude;
            var lng = position.coords.longitude;
            document.getElementById("demo").innerHTML += "<br />" + lat + " " + lng;
        }
        function myFunction() {
            if (navigator.geolocation)
                navigator.geolocation.getCurrentPosition(onPositionUpdate);
            else
                alert("navigator.geolocation is not available");
        }
    </script>-->
</head>
<body>
    <p class="mobile" id="demo"></p>
    <!--<button type="button" onclick="myFunction()" class="mobile">Get lat/lng.</button>
    <p>&nbsp;</p>-->
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Calculate" Font-Size="XX-Large"/>
        </div>
    </form>
    <pre class="mobile"><% Response.Write(Session["Error"].ToString()); %>
            </pre>
</body>
</html>
