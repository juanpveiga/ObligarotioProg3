using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace ObligatorioP3Web
{
    public partial class EvaluarEmprendimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Rol"] != null)
            {
                if (Session["Rol"].ToString() != "Evaluador")
                {
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            plcEvaluar.Visible = false;
            if (!IsPostBack)
            {
                if (Session["IdEvaluador"] != null)
                {
                    int idEvaluador = Convert.ToInt32(Session["IdEvaluador"].ToString());
                    List<Emprendimiento> empAEvaluar = Evaluador.buscarEmpDeEvaluador(idEvaluador);
                    if (empAEvaluar.Count == 0)
                    {
                        lblMensajeDrop.Text = " No tiene evaluaciones pendinetes";
                    }
                    else
                    {
                        ddlEmp.DataSource = empAEvaluar;
                        ddlEmp.DataTextField = "Titulo";
                        ddlEmp.DataValueField = "CodId";
                        ddlEmp.DataBind();

                        plhSelEmp.Visible = true;
                    }
                }
            }
        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            int CodId = Convert.ToInt32(ddlEmp.SelectedValue);
            Emprendimiento emp = new Emprendimiento();
            if (emp.buscar(CodId))
            {
                grillaEmp.DataSource = Emprendimiento.mostrarEmprendimiento(emp);
                grillaEmp.DataBind();
                plcEvaluar.Visible = true;

            }
        }

        protected void btnEvaluar_Click(object sender, EventArgs e)
        {
            int CodId = Convert.ToInt32(ddlEmp.SelectedValue);
            int idEvaluador = Convert.ToInt32(Session["IdEvaluador"].ToString());
            int puntaje = Convert.ToInt32(txtPuntaje.Text);
            string justificacion = txtJust.Text;
            DateTime fecha = DateTime.Today;
            Emprendimiento emp = new Emprendimiento();
            if (CodId > 0 && idEvaluador > 0 && puntaje >= 0 && puntaje <= 4 && justificacion.Length >= 100 && justificacion.Length <= 500 && emp.buscar(CodId))
            {
                Evaluacion ev = new Evaluacion()
                {
                    Emprendimiento = emp,
                    Puntaje = puntaje,
                    Justicacion = justificacion,
                    Fecha = fecha,
                    Estado = true,


                };

                if (ev.actualizar(idEvaluador))
                {
                    lblMensaje.Text = "Se Ingreso la evaluacion correctamente";
                    btnEvaluar.Visible = false;
                }
            }
            else
            {
                lblMensaje.Text = "Ingresar datos de evaluacion correctamente";
            }
            
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }
    }
}