<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="AltaEmprendimiento.aspx.cs" Inherits="ObligatorioP3Web.AltaEmprendimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Alta Emprendimiento</h1>
    <h2>Ingrese datos del emprendimiento</h2>

    <asp:Label ID="lblTitulo" runat="server" Text="Ingrese titulo"></asp:Label>
    <asp:TextBox ID="txtTitulo" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lblDescripcion" runat="server" Text="Ingrese Descripcion"></asp:Label>
    <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lblCosto" runat="server" Text="Ingrese Costo"></asp:Label>
    <asp:TextBox ID="txtCosto" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lblTiempoEjec" runat="server" Text="Tiempo Ejecucion"></asp:Label>
    <asp:TextBox ID="txtTiempoEjec" runat="server"></asp:TextBox>
    <br />

    <h2>Ingrese datos del postulante</h2>

    <asp:Label ID="lblEmail" runat="server" Text="Ingrese email"></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lblContrasena" runat="server" Text="Ingrese contrasena"></asp:Label>
    <asp:TextBox ID="txtContrasena" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lblCedula" runat="server" Text="Ingrese cedula"></asp:Label>
    <asp:TextBox ID="txtCedula" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lblNombre" runat="server" Text="Ingrese nombre"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    <br />
    
    <br />
    <asp:Button ID="btnDarAlta" runat="server" Text="Dar Alta" OnClick="btnDarAlta_Click"/>
    <br /><br />
    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
    <br /><br />

    <asp:Button ID="btnAgregarPost" runat="server" Text="Agregar mas Postulantes " OnClick="btnAgregarPost_Click" /><br />
    <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" />

</asp:Content>
