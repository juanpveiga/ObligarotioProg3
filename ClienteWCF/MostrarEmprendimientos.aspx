<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MostrarEmprendimientos.aspx.cs" Inherits="ClienteWCF.MostrarEmprendimientos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Mostrar Emprendimientos</h1>
        <asp:Button ID="btnMostrar" runat="server" Text="Mostrar" OnClick="btnMostrar_Click" />
        <br />
        <asp:GridView ID="grillaEmp" runat="server"></asp:GridView>
        
    </div>
    </form>
</body>
</html>
