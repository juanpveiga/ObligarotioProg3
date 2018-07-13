using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
using System.Configuration;


namespace AccesoDatos
{
    public  abstract class AbstractBase
    {
       
            #region Manejo de la conexión.
            //La cadena de conexión está configurada para el servidor de prueba 
            //que viene con Visual Studio
            //Cambiarla si se utiliza otro servicio de SQLServer.
            private static string cadenaConexion = @"SERVER=.\SQLEXPRESS;
                                                        DataBase=ObligatorioP31;
                                                        Trusted_Connection=true;";
            protected static SqlConnection CrearConexion()
            {
                return new SqlConnection(cadenaConexion);
            }
            protected static void AbrirConexion(SqlConnection cn)
            {

                try
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                }
                catch (Exception ex)
                {

                    Debug.Assert(false, ex.Message);

                }
            }
            protected static void CerrarConexion(SqlConnection cn)
            {

                try
                {
                    if (cn.State != ConnectionState.Closed)
                    {
                        cn.Close();
                        cn.Dispose();
                    }

                }
                catch (Exception ex)
                {

                    Debug.Assert(false, ex.Message);

                }
            }
            #endregion

            #region Métodos abstractos de Active Record
            public abstract bool insertar();
            public abstract bool actualizar();
            public abstract bool eliminar();
        

            #endregion

        }
    }



