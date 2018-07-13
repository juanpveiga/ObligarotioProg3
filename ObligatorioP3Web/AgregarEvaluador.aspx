<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="AgregarEvaluador.aspx.cs" Inherits="ObligatorioP3Web.AgregarEvaluador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Agregar Evaluador</h1>
    <asp:Label ID="lblEmp" runat="server" Text="Seleccione un emprendimiento "></asp:Label>
    <asp:DropDownList ID="ddlEmp" runat="server"></asp:DropDownList>
    <br />
    <asp:Label ID="lblEv" runat="server" Text="Seleccione un evaluador "></asp:Label>
    <asp:DropDownList ID="ddlEv" runat="server"></asp:DropDownList>
    <br />
    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
    
    <br /><br />
    <asp:Button ID="btnSalir" runat="server" Text="Volver" OnClick="btnSalir_Click" />

    <br />

</asp:Content>
