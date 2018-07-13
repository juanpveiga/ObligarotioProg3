using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace ObligatorioP3Web
{
    public partial class AgregarPostulante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Rol"] != null)
            {
                if (Session["Rol"].ToString() != "Postulante")
                {
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnDarAlta_Click(object sender, EventArgs e)
        {
            int codEm = -1;
            codEm = Convert.ToInt32(Session["CodEm"].ToString());


            string email = txtEmail.Text;
            string contrasena = txtContrasena.Text;
            string rol = "Postulante";
            string cedula = txtCedula.Text;
            string nombre = txtNombre.Text;

            if (email != "" && contrasena.Length >= 8 && rol != "" && cedula != "" && nombre != "")
            {
                if (Integrante.verificarIntegrante(cedula))
                {
                    Integrante inte = new Integrante()
                    {
                        Email = email,
                        Contrasena = contrasena,
                        Rol = rol,
                        Cedula = cedula,
                        Nombre = nombre,
                    };


                    if (inte.altaIntegrante(codEm, email, contrasena, nombre, cedula, rol))
                    {
                        lblMensaje.Text = "Se agrego el postulante correctamente";
                    }
                    else
                    {
                        lblMensaje.Text = "No se pudo agregar el postulate al emprendimiento";
                    }
                }
                else
                {
                    lblMensaje.Text = "El postulante ya existe en un emprendimiento";
                }
            }
            else
            {
                lblMensaje.Text = "Ingrese datos del postulante correctamente";
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }
    }
}