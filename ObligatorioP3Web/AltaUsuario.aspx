<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="AltaUsuario.aspx.cs" Inherits="ObligatorioP3Web.AltaUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Alta Usuario</h1>

    <asp:Label ID="lblEmail" runat="server" Text="Ingrese email"></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lblContrasena" runat="server" Text="Ingrese contrasena"></asp:Label>
    <asp:TextBox ID="txtContrasena" runat="server"></asp:TextBox>
    
    <br /><br />

    <asp:Label ID="lblTipoUsuario" runat="server" Text="Seleccione un tipo de usuario"></asp:Label>
    <br />
    <asp:RadioButton ID="rbtAdmin" runat="server" GroupName="tipoUsuario" AutoPostBack="True" Text="Admin" Checked="True" OnCheckedChanged="rbtAdmin_CheckedChanged"/>
    <asp:RadioButton ID="rbtEvaluador" runat="server" GroupName="tipoUsuario" AutoPostBack="True" Text="Evaluador" OnCheckedChanged="rbtEvaluador_CheckedChanged"/>

    <br />
    
    <asp:PlaceHolder ID="plcEvaluador" runat="server">
        <asp:Label ID="lblCedula" runat="server" Text="Ingrese cedula"></asp:Label>
        <asp:TextBox ID="txtCedula" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblNombre" runat="server" Text="Ingrese nombre"></asp:Label>
        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblTelefono" runat="server" Text="Ingrese telefono"></asp:Label>
        <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblCalificacion" runat="server" Text="Ingrese calificacion"></asp:Label>
        <asp:TextBox ID="txtCalificacion" runat="server"></asp:TextBox>
    </asp:PlaceHolder>

    <br /><br />

    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" />
    <br />
    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
    <br />
    <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" />
    <br />
    
</asp:Content>
