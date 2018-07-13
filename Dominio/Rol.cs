using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AccesoDatos;

namespace Dominio
{
    public class Rol
    {
        #region Atributos
        private string tipoRol;
        #endregion

        #region Propiedades
        public string TipoRol
        {
            get { return this.tipoRol; }
            set { this.tipoRol = value; }
        }
        #endregion

        #region Metodo
        public Rol buscar(string rol)
        {
            if()
        }
        #endregion
    }
}
