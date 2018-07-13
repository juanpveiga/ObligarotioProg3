using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace ObligatorioP3Web
{
    public partial class AltaEmprendimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Rol"] != null)
            {
                Response.Redirect("Inicio.aspx");
            }
            

            btnAgregarPost.Visible = false;
        }

        protected void btnDarAlta_Click(object sender, EventArgs e)
        {
            string mensaje = "No se pudo dar de alta.";
            string email = txtEmail.Text;
            string contrasena = txtContrasena.Text;
            string rol = "Postulante";
            string cedula = txtCedula.Text;
            string nombre = txtNombre.Text;

            if (email != "" && contrasena.Length >= 8 && cedula != "" && nombre != "")
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


                    string titulo = txtTitulo.Text;
                    string descripcion = txtDescripcion.Text;
                    double costo = -1;
                    if (txtCosto.Text != "")
                    {
                        costo = Convert.ToDouble(txtCosto.Text);
                    }
                    int tiempoEjec = -1;
                    if (txtTiempoEjec.Text != "")
                    {
                        tiempoEjec = Convert.ToInt32(txtTiempoEjec.Text);
                    }

                    if (titulo != "" && descripcion != "" && costo >= 0 && tiempoEjec > 0)
                    {
                        if (Emprendimiento.verificarEmprendimiento(titulo))
                        {
                            Emprendimiento em = new Emprendimiento()
                            {
                                Titulo = titulo,
                                Descripcion = descripcion,
                                Costo = costo,
                                TiempoEjecucion = tiempoEjec,
                                Integrantes = new List<Integrante>(),
                                Financiado = false,

                            };

                            if (em != null && inte != null)
                            {
                                if (em.agregarIntegrante(inte))
                                {
                                    if (em.insertar())
                                    {
                                        Session["email"] = email;
                                        Session["rol"] = rol;
                                        Session["CodEm"] = Integrante.buscarCodEm(email);
                                        mensaje = "Se dio de alta correctamente.";
                                        btnDarAlta.Visible = false;
                                        btnAgregarPost.Visible = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            mensaje += "\n El emprendimiento ya existe";
                        }
                        
                    }
                    else
                    {
                        mensaje += "\n Ingrese datos del emprendimiento correctamente.";
                    }

                }
                else
                {
                    mensaje += "\n El integrante ya existe en un emprendimiento."; ;
                }

            }
            else
            {
                mensaje += "\n Ingrese datos del postulante correctametne.";
            }


            lblMensaje.Text = mensaje;
        }
    

        protected void btnAgregarPost_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaPostulante.aspx");
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }
    }
}