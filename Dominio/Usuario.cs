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
    public class Usuario : AbstractBase
    {





        #region Propiedades
        public int IdUsuario { get; set; }
        public string Email  { get; set; }
        public string Contrasena  { get; set; }
        public string Rol   { get; set; }


        #endregion

        

        #region validacion 


        public bool validarContrasena()
        {
            bool valido = false;

            if (this.Contrasena != String.Empty)
            {
                bool contNum = false;
                bool contString = false;
                int totalCaracteres = 0;
                foreach (char item in this.Contrasena)
                {
                    if (Char.IsNumber(item))
                    {
                        contNum = true;
                        totalCaracteres++;
                    }
                    else if (Char.IsLetter(item))
                    {
                        contString = true;
                        totalCaracteres++;
                    }
                }
                if (contString || contNum && totalCaracteres == this.Contrasena.Count<char>() && this.Contrasena.Count<char>() >= 8)
                {
                    valido = true;
                }
            }
            return valido;
        }
        #endregion

        #region Active Record

        public override bool insertar ()
        {
            bool salida = false;
            if (!this.validarContrasena() || this.Email=="") return salida;

            SqlConnection cn = CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"INSERT INTO Usuario
                                VALUES(@email,@contrasena,@rol
                                SELECT CAST(scope_identity() AS int))";
            cmd.Parameters.AddWithValue("@email", this.Email);
            cmd.Parameters.AddWithValue("@contrasena", this.Contrasena);
            cmd.Parameters.AddWithValue("@rol", this.Rol);

            cmd.Connection = cn;
            try
            {
                this.IdUsuario = -1;
                AbrirConexion(cn);
                this.IdUsuario = (int)cmd.ExecuteScalar();
                salida = this.IdUsuario!=1;

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
        public override bool eliminar()
        {

            SqlConnection cn = CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"DELETE Usuario WHERE Email=@email and Contrasena=@contrasena";
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@email", this.Email);
            cmd.Parameters.AddWithValue("@contrasena", this.Contrasena);

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

        public override bool actualizar()
        {
            if (!this.validarContrasena() || Email =="") return false;

            SqlConnection cn = CrearConexion();
            SqlCommand cmd = new SqlCommand();
                       cmd.CommandText = @"UPDATE Usuario SET  Rol=@rol, Contrasena=@contrasena
                                                    
                                WHERE Email=@email ";
            cmd.Parameters.AddWithValue("@rol", this.Rol);
            cmd.Parameters.AddWithValue("@email", this.Email);
            cmd.Parameters.AddWithValue("@contrasena", this.Contrasena);

            cmd.Connection = cn;
            try
            {

                AbrirConexion(cn);
                cmd.ExecuteNonQuery();

                return true;

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

        public bool buscar()
        {
            bool retorno = false;
            SqlConnection con = CrearConexion();
            SqlDataReader reader = null;

            try
            {
                SqlCommand com = new SqlCommand("UsuarioPorEmail", con);
                com.Parameters.AddWithValue("@Email", this.Email);
                com.CommandType = CommandType.StoredProcedure;

                con.Open();
                reader = com.ExecuteReader();

                if (reader.Read())
                {
                    this.Contrasena = reader["Contrasena"].ToString();
                    this.Rol = reader["Rol"].ToString();

                    retorno = true;
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

        public static Usuario buscarPorEmail(string email)
        {
            Usuario u = null;

            SqlConnection con = CrearConexion();

            try
            {
                SqlCommand com = new SqlCommand("UsuarioPorEmail", con);
                com.Parameters.AddWithValue("@Email", email);
                com.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = com.ExecuteReader();

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        u = new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"].ToString()),
                            Email = dr["Email"].ToString(),
                            Contrasena = dr["Contrasena"].ToString(),
                            Rol = dr["Rol"].ToString()
                        };

                    }
                }
                dr.Close();
                con.Close();

                return u;
                
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open) CerrarConexion(con);

            }


        }


        public static string verificarUsuario (string email,string contrasena)
        {
            SqlCommand cmd = new SqlCommand();
            
            cmd.CommandText = "Select  Rol, Email, Contrasena from Usuario where Email = @Email and Contrasena = @Contrasena";
            SqlConnection cn = CrearConexion();
            string retorno = "";
            SqlDataReader drResults;

            cmd.Connection = cn;
            cmd.Parameters.Add(new SqlParameter("@Email",email));
            cmd.Parameters.Add(new SqlParameter("@Contrasena", contrasena));

            cn.Open();
            drResults = cmd.ExecuteReader();

            if (drResults.Read())
            {

                retorno = drResults["rol"].ToString();
           
                }
            drResults.Close();
            cn.Close();
            return retorno;
        }
}

       
        #endregion

        #region Metodos
       
        #endregion

    }


