using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ObtenerEmprendimientos" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ObtenerEmprendimientos.svc o ObtenerEmprendimientos.svc.cs en el Explorador de soluciones e inicie la depuración.


    public class ObtenerEmprendimientos : IObtenerEmprendimientos
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["Miconexion"].ConnectionString;
        public List<DTOEmprendimiento> mostrarEmprendimientos()
        {
            List<DTOEmprendimiento> emprendimientos = new List<DTOEmprendimiento>();
            SqlConnection con = new SqlConnection(cadenaConexion);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM Emprendimiento";
            cmd.Connection = con;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {


                    while (dr.Read())
                    {
                        DTOEmprendimiento emp = new DTOEmprendimiento();

                        emp.CodId = int.Parse(dr["CodId"].ToString());
                        emp.Titulo = dr["Titulo"].ToString();
                        DTOEvaluacion e = obtenerEvaluacion(emp.CodId);

                        string salida = "";
                        string nomIntegrantes = obtenerNomIntegrantes(emp.CodId);
                        if (nomIntegrantes != "")
                        {
                            salida += " INTEGRANTES: " + nomIntegrantes;
                        }
                        else
                        {
                            salida += " INTEGRANTES: ninguno";
                        }

                        if (e.Nombre.ToString() != "")
                        {
                            salida += " EVALUADORES: " + e.Nombre.ToString();
                            if(e.Justificacion.ToString() != "")
                            {
                                salida += " JUSTIFICACION: " + e.Justificacion.ToString();
                            }
                            else
                            {
                                salida += " JUSTIFICACION: pendiente";
                            }
                        }
                        else
                        {
                            salida += " EVALUADORES: ninguno" + " JUSTIFICACIONES: pendiente";
                        }

                        emp.Descripcion = dr["Descripcion"].ToString() + salida;
                        emp.Costo = double.Parse(dr["Costo"].ToString());
                        emp.TiempoEjecucion = int.Parse(dr["Tiempo_Ejecucion"].ToString());
                        
                        emp.Puntaje = e.PuntajeTotal;
                        emprendimientos.Add(emp);
                    }

                }


                dr.Close();


                return emprendimientos;
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }

        }

        public string obtenerNomIntegrantes(int CodEmp)
        {
            string salida = "";
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"Select * from Integrante where Emprendimiento = @CodEmp;";
            cmd.Parameters.AddWithValue("@CodEmp", CodEmp);

            cmd.Connection = con;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        salida += dr["Nombre"].ToString() + ", ";
                    }
                }
                return salida;
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }


        }

        public DTOEvaluacion obtenerEvaluacion(int CodEmp)
        {
            DTOEvaluacion e = null;
            string nombres = "";
            string justificaciones = "";
            int puntajeTotal = 0;
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"Select ev.Nombre,e.Justificacion,e.Puntaje from Evaluador ev ,Evaluacion e where e.IdEvaluador = ev.IdEvaluador and e.Emprendimiento = @CodEmp;";
            cmd.Parameters.AddWithValue("@CodEmp", CodEmp);
            cmd.Connection = con;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        nombres += dr["Nombre"].ToString() + ", ";
                        if (dr["Justificacion"].ToString() != "")
                        {
                            justificaciones += dr["Justificacion"].ToString() + ", ";
                        }
                        if (dr["Puntaje"].ToString() != "")
                        {
                            puntajeTotal += dr["Puntaje"].GetHashCode();
                        }
                    }
                }
                e = new DTOEvaluacion()
                {
                    Nombre = nombres,
                    Justificacion = justificaciones,
                    PuntajeTotal = puntajeTotal,

                };
                return e;
            }
            catch
            {
                return e;

                throw;
            }
            finally
            {
                con.Close();
            }


        }
    }
}
