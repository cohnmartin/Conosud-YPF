<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PruebaConex.aspx.cs" Inherits="PruebaConex" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="ControlsAjax" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style>
        .Theader
        {
            background-position: 0 -7550px;
            border-top: 0;
            background-repeat: repeat;
            border-style: solid;
            border-width: 1px;
            border-color: #e98879 #cd6a3f #71250a #872b07;
            color: white;
            font-family: "segoe ui" ,arial,sans-serif;
            font-size: 12px;
            height: 23px;
            text-align: center;
            padding: 0px 0px 0px 0px;
            line-height: 1em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnConectar" runat="server" OnClick="btnConectar_Click" Text="Conectar"
        Width="87px" OnClientClick="CleanTable(); return false;" />
        <asp:Button ID="Button1" runat="server"  Text="Ver Cambios"
        Width="87px" OnClientClick="CleanTable(); return false;" 
        onclick="Button1_Click" />
    <asp:TextBox ID="lblCadena" runat="server" Height="52px" TextMode="MultiLine" Width="334px"></asp:TextBox>
    <br />
    <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>

    
    </form>
</body>

</html>
