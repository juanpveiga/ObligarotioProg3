using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace ObligatorioP3Web
{
    public partial class AgregarEvaluador : System.Web.UI.Page
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
                ddlEmp.DataSource = Emprendimiento.busarEmpAagregarEval();
                ddlEmp.DataTextField = "Titulo";
                ddlEmp.DataValueField = "CodId";
                ddlEmp.DataBind();

                ddlEv.DataSource = Evaluador.buscarTodos();
                ddlEv.DataTextField = "Nombre";
                ddlEv.DataValueField = "IdEvaluador";
                ddlEv.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string mensaje = "No se pudo agregar";
            int CodIdEmp = Convert.ToInt32(ddlEmp.SelectedValue);
            int idEvaluador = Convert.ToInt32(ddlEv.SelectedValue);

            if(Evaluador.verificarEvaluador(idEvaluador, CodIdEmp))
            {
                Evaluador ev = new Evaluador();
                ev.IdEvaluador = idEvaluador;

                if (ev.agregarEmprendimiento(CodIdEmp))
                {
                    mensaje = "Se agrego correctamente";
                }
            }
            else
            {
                mensaje += "\n El evaluador ya se encuentra en este emprendimiento";
            }
            

            lblMensaje.Text = mensaje;
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }
    }
}