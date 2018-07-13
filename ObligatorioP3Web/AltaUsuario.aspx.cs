using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace ObligatorioP3Web
{
    public partial class AltaUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Rol"] != null)
            {
                if (Session["Rol"].ToString() != "Administrador")
                {
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                plcEvaluador.Visible = false;
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string contrasena = txtContrasena.Text;
            string rol = "";
            string mensaje = "Error al dar de alta";

            if (rbtAdmin.Checked)
            {
                rol = "Administrador";
                if(email != "" && contrasena.Length >= 8 && rol != "")
                {
                    if(Usuario.buscarPorEmail(email) == null)
                    {
                        Usuario u = new Usuario
                        {
                            Email = email,
                            Contrasena = contrasena,
                            Rol = rol
                        };

                        if (u.insertar())
                        {
                            mensaje = "Se dio de alta correctamente";
                        }
                        
                    }
                    else
                    {
                        mensaje += "\n El usuario " + email + " ya existe.";
                    }
                }
                else
                {
                    mensaje += "\n Ingrese datos correctamente";
                }
            }
            else
            {
                if (rbtEvaluador.Checked)
                {
                    rol = "Evaluador";
                    string cedula = txtCedula.Text;
                    string nombre = txtNombre.Text;
                    string telefono = txtTelefono.Text;
                    int calificacion = -1;
                    if(txtCalificacion.Text != "")
                    {
                        calificacion = Convert.ToInt32(txtCalificacion.Text);
                    }

                    if(email != "" && contrasena.Length >= 8 && rol != "" && cedula != "" && nombre != "" && telefono != "" && calificacion >= 1 && calificacion <= 4)
                    {
                        if(Usuario.buscarPorEmail(email) == null)
                        {
                            Evaluador ev = new Evaluador
                            {
                                Email = email,
                                Contrasena = contrasena,
                                Rol = rol,
                                Cedula = cedula,
                                Nombre = nombre,
                                Telefono = telefono,
                                Calificacion = calificacion
                            };

                            if (ev.insertar())
                            {
                                mensaje = "Se dio de alta correctamente";
                            }
                        }
                        else
                        {
                            mensaje += "\n El usuairio " + email + " ya existe";
                        }
                    }
                    else
                    {
                        mensaje += "\n Ingrese datos correctamente";
                    }
                }
            }
            lblMensaje.Text = mensaje;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }

        protected void rbtAdmin_CheckedChanged(object sender, EventArgs e)
        {
            plcEvaluador.Visible = false;
        }

        protected void rbtEvaluador_CheckedChanged(object sender, EventArgs e)
        {
            plcEvaluador.Visible = true;
        }
    }
}