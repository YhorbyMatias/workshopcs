<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Webforms_SQL_Query.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox runat="server" ID="TxtNom"></asp:TextBox>
            <asp:TextBox runat="server" ID="TxtMon"></asp:TextBox>
        </div>

        <div>
            <asp:Label runat="server" ID="LblMensage1"></asp:Label>
            <asp:Label runat="server" ID="LblMensage2"></asp:Label>
        </div>
    </form>
</body>
</html>
