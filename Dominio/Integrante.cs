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
    public class Integrante : Usuario
    {
        
        #region propiedades autoimplementadas
        public int IdIntengrante { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        
        #endregion

        #region Metodos
        public static int buscarCodEm(string username)
        {
            SqlConnection cn = CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Buscar_Cod_Emp";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Email", username);
            cmd.Connection = cn;
            int codEm = -1;
            try
            {
                AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    codEm = dr.GetInt32(dr.GetOrdinal("Emprendimiento"));
                }
                return codEm;
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
                return -1;
            }
            finally
            {
                CerrarConexion(cn);
            }
        }

        public static bool verificarIntegrante(string cedula)
        {
            bool ok = false;

            SqlConnection con = CrearConexion();
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = @"SELECT * FROM Integrante WHERE Cedula=@Cedula";
            com.Parameters.AddWithValue("@Cedula", cedula);

            try
            {
                AbrirConexion(con);

                SqlDataReader dr = com.ExecuteReader();

                if (!dr.HasRows)
                {
                    ok = true;
                }

                dr.Close();
                return ok;
            }
            catch
            {
                throw;
            }
            finally
            {
                CerrarConexion(con);
            }

        }
        public bool altaIntegrante(int idEmp, string email, string contrasena, string nombre, string cedula, string rol)
        {
            SqlConnection con = CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            bool ok = false;
            Integrante i = new Integrante();

            
            i.Email = email;
            i.Contrasena = contrasena;
            i.Nombre = nombre;
            i.Cedula = cedula;
            i.Rol = rol;

            if (this.Email == "" || this.Contrasena.Length < 8 || this.Nombre == "" || this.Cedula == "" 
                || this.Rol == "" || Usuario.buscarPorEmail(this.Email) != null) return false;

            try
            {

                AbrirConexion(con);


                cmd.CommandText = "Integrante_Insert";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par = new SqlParameter();
                par.ParameterName = "@Error";
                par.SqlDbType = SqlDbType.Int;
                par.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par);

                SqlParameter par2 = new SqlParameter();
                par2.ParameterName = "@IdIntegrante";
                par2.SqlDbType = SqlDbType.Int;
                par2.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par2);

                SqlParameter par3 = new SqlParameter();
                par3.ParameterName = "@IdUsuario";
                par3.SqlDbType = SqlDbType.Int;
                par3.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par3);

                cmd.Parameters.Add(new SqlParameter("@Email", Email));
                cmd.Parameters.Add(new SqlParameter("@Contrasena", Contrasena));
                cmd.Parameters.Add(new SqlParameter("@Rol", Rol));

                cmd.Parameters.Add(new SqlParameter("@Cedula", Cedula));
                cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                cmd.Parameters.Add(new SqlParameter("@Emprendimiento",idEmp ));
                cmd.ExecuteNonQuery();

                IdIntengrante = (int)par2.Value;
                IdUsuario = (int)par3.Value;

                ok = (int)par.Value == 0;
                
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
            }
            finally
            {
                CerrarConexion(con);
                
            }
            return ok;
        }

        public bool buscarIntegrantePorEmail()
        {
            bool retorno = false;
            SqlConnection con = CrearConexion();
            SqlDataReader reader = null;

            try
            {
                SqlCommand com = new SqlCommand("IntegrantePorEmail", con);
                com.Parameters.AddWithValue("@Email", this.Email);
                com.CommandType = CommandType.StoredProcedure;

                con.Open();
                reader = com.ExecuteReader();

                if (reader.Read())
                {
                    this.Cedula = reader["Cedula"].ToString();
                    this.Nombre = reader["Nombre"].ToString();

                    if (reader.NextResult())
                    {
                        this.Contrasena = reader["Contrasena"].ToString();
                        this.Rol = reader["Rol"].ToString();
                        retorno = true;
                    }

                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (reader != null) reader.Close();
                if (con != null && con.State == ConnectionState.Open) con.Close();

            }

            return retorno;
        }

        #endregion
    }
}


    
    