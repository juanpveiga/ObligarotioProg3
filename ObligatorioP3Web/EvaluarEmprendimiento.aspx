<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="EvaluarEmprendimiento.aspx.cs" Inherits="ObligatorioP3Web.EvaluarEmprendimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Evaluar Emprendimiento</h1>
     <asp:PlaceHolder ID="plhSelEmp" runat="server">

    <asp:Label ID="lblEmp" runat="server" Text="Seleccione un emprendimiento"></asp:Label>
    <asp:DropDownList ID="ddlEmp" runat="server"></asp:DropDownList><br />
    <asp:Label ID="lblMensajeDrop" runat="server" Text=""></asp:Label>
    <br />
    <asp:Button ID="btnSeleccionar" runat="server" Text="Seleccionar" OnClick="btnSeleccionar_Click" />
    <br />
    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label><br />
    </asp:PlaceHolder>
    
    <asp:PlaceHolder ID="plcEvaluar" runat="server">
        <asp:GridView ID="grillaEmp" runat="server"></asp:GridView>
        <br />
        <asp:Label ID="lblPuntaje" runat="server" Text="Ingrese un puntaje"></asp:Label>
        <asp:TextBox ID="txtPuntaje" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblJust" runat="server" Text="Ingrese justificacion"></asp:Label>
        <asp:TextBox ID="txtJust" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnEvaluar" runat="server" Text="Evaluar" OnClick="btnEvaluar_Click" />
        <br />

    </asp:PlaceHolder>
    
    <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" />


</asp:Content>
