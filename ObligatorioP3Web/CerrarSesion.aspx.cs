using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ObligatorioP3Web
{
    public partial class CerrarSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Rol"] = null;
            Session["Email"] = null;
            Session["CodEm"] = null;
            Session["Cedula"] = null;
            Response.Redirect("Inicio.aspx");
        }
    }
}