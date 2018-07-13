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
    public class Evaluacion : AbstractBase
    {


        #region Propiedades
        public int Puntaje { get; set; }

        public string Justicacion { get; set; }

        public DateTime Fecha { get; set; }

        public Emprendimiento Emprendimiento  { get; set; }

        public bool Estado { get; set; }
        #endregion

        public override bool insertar()
        {
            throw new NotImplementedException();
        }

        public override bool actualizar()
        {
            throw new NotImplementedException();
        }

        public override bool eliminar()
        {
            throw new NotImplementedException();
        }

        public bool eliminar(int idEv)
        {
            SqlConnection cn = CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"DELETE Evaluacion  WHERE Evaluador = @Evaluador AND Emprendimiento = @Emprendimiento;";
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@Evaluador", idEv);
            cmd.Parameters.AddWithValue("@contrasena", this.Emprendimiento.CodId);

            try
            {
                cn.Open();
                int filas = cmd.ExecuteNonQuery();

                return filas == 1;
            }
            catch (SqlException ex)
            {
                //
                System.Diagnostics.Debug.Assert(false, ex.Message);
                return false;
            }
            finally
            {
                CerrarConexion(cn);
            }
        }

        public bool insertarEvaluacion(int idEv)
        {
            int filas = -1;
            bool salida = false;
            SqlConnection cn = CrearConexion();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Evaluacion_Insert1";
            cmd.CommandType = CommandType.StoredProcedure;
             
            cmd.Parameters.AddWithValue("@Evaluador", idEv);
            cmd.Parameters.AddWithValue("@Emprendimiento", this.Emprendimiento.CodId);
            cmd.Parameters.AddWithValue("@Estado", false);

            cmd.Connection = cn;
           
            try
            {
                
                AbrirConexion(cn);
                filas = cmd.ExecuteNonQuery();
                salida = filas == 1;
                
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
                salida = false;

            }
            finally
            {
                CerrarConexion(cn);
                
            }
            return salida;
        }

        public bool actualizar(int idEvaluador)
        {
            bool salida = false;
            SqlConnection cn = CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"UPDATE Evaluacion
                                SET Puntaje = @Puntaje,
                                    Justificacion = @Justificacion,
                                    Fecha = @Fecha,
                                    Estado= @Estado
                                WHERE IdEvaluador = @IdEvaluador AND Emprendimiento = @Emprendimiento;";
            cmd.Parameters.AddWithValue("@IdEvaluador", idEvaluador);
            cmd.Parameters.AddWithValue("@Emprendimiento", this.Emprendimiento.CodId);
            cmd.Parameters.AddWithValue("@Puntaje", this.Puntaje);
            cmd.Parameters.AddWithValue("@Justificacion", this.Justicacion);
            cmd.Parameters.AddWithValue("@Fecha", this.Fecha);
            cmd.Parameters.AddWithValue("@Estado", true);


            cmd.Connection = cn;
            try
            {
                int filas = 0;
                AbrirConexion(cn);
                filas = cmd.ExecuteNonQuery();
                salida = filas == 1;
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
                

            }
            finally
            {
                CerrarConexion(cn);

            }
            return salida;
        }
    }
}
