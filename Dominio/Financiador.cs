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
    class Financiador:Usuario
    {
        #region Atributos
        private string nomOrg;
        private double montoMax;
        #endregion

        #region propiedades
        public string NomOrg
        {
            get { return this.nomOrg; }
            set { this.nomOrg = value; }

        } public double MontoMax
        {
            get { return this.montoMax; }
            set { this.montoMax = value; }
        }
        #endregion

    }
}
