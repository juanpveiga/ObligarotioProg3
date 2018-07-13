using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace ObligatorioP3Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string username = Login1.UserName;
            string password = Login1.Password;
            string rol = Usuario.verificarUsuario(username, password);

            if (rol != "")
            {
                Session["Rol"] = rol;
                Session["Email"] = username;
                if (rol == "Postulante")
                {
                    Session["CodEm"] = Integrante.buscarCodEm(username);
                }
                if (rol == "Evaluador")
                {
                    Session["IdEvaluador"] = Evaluador.buscarIdEvaluador(username);
                }

                Response.Redirect("Inicio.aspx");

            }
            else { lblMensaje.Text = "Los datos son incorrectos"; }
        }
    }
}