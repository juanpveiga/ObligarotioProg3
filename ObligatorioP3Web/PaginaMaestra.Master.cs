using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ObligatorioP3Web
{
    public partial class PaginaMaestra : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Rol"] != null)
            {
                if(Session["Rol"].ToString() == "Administrador")
                {
                    MenuAdministrador.Visible = true;
                    MenuEvaluador.Visible = false;
                    MenuPostulante.Visible = false;
                    MenuInicio.Visible = false;
                }
                if(Session["Rol"].ToString() == "Evaluador")
                {
                    MenuAdministrador.Visible = false;
                    MenuEvaluador.Visible = true;
                    MenuPostulante.Visible = false;
                    MenuInicio.Visible = false;
                }
                if (Session["Rol"].ToString() == "Postulante")
                {
                    MenuAdministrador.Visible = false;
                    MenuEvaluador.Visible = false;
                    MenuPostulante.Visible = true;
                    MenuInicio.Visible = false;
                }
            }
            else
            {
                MenuAdministrador.Visible = false;
                MenuEvaluador.Visible = false;
                MenuPostulante.Visible = false;
                MenuInicio.Visible = true;
            }
        }
    }
}