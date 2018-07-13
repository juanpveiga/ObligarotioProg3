<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="AgregarPostulante.aspx.cs" Inherits="ObligatorioP3Web.AgregarPostulante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Agregar Postulante</h1>

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
    <asp:Button ID="btnDarAlta" runat="server" Text="Dar Alta" OnClick="btnDarAlta_Click" />
    <br />
    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
    <br /><br />
    <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" />

</asp:Content>
