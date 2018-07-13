using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClienteWCF.ServicioObtenerEmprendimientos;
using System.IO;

namespace ClienteWCF
{
    public partial class MostrarEmprendimientos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            ObtenerEmprendimientosClient proxy = new ObtenerEmprendimientosClient();

            IEnumerable<DTOEmprendimiento> emprendimientos = proxy.mostrarEmprendimientos();

            grillaEmp.DataSource = emprendimientos;
            grillaEmp.DataBind();

            string ruta = HttpRuntime.AppDomainAppPath + "ServicioTexto//Emprendimientos.log";
            StreamWriter sw = new StreamWriter(ruta);

            foreach(DTOEmprendimiento emp in emprendimientos)
            {
                sw.WriteLine(emp.CodId + "#" + emp.Titulo + "#" + emp.Costo + "#" + emp.TiempoEjecucion + "#" + emp.Puntaje + "#" + emp.Descripcion);
            }

            sw.Close();
        }
    }
}