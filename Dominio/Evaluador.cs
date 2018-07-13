using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AccesoDatos;

namespace Dominio
{
    public class Evaluador : Usuario
    {


        #region Propiedades
        public int IdEvaluador { get; set; }
        public List<Evaluacion> Evaluaciones { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }
        public int Calificacion { get; set; }




        #endregion

        #region Active Records
        /* public override bool insertar()
         {
             bool salida = false;
             if (!this.validarContrasena() && this.Email != "") return salida;
             int filas = 0;
             SqlConnection cn = CrearConexion();
             SqlCommand cmd = new SqlCommand();
             SqlTransaction trans = null;
             cmd.CommandText = @"INSERT INTO Usuarios
                                 VALUES(@email,@contrasena,@rol)";
             cmd.Parameters.AddWithValue("@email", this.Email);
             cmd.Parameters.AddWithValue("@contrasena", this.Contrasena);
             cmd.Parameters.AddWithValue("@rol", this.Rol);

             cmd.Connection = cn;
             try
             {

                 AbrirConexion(cn);
                 trans = cn.BeginTransaction();
                 cmd.Transaction = trans;

                 filas = cmd.ExecuteNonQuery();
                 cmd.Parameters.Clear();
                 filas = 0;
                 cmd.CommandText = @" INSERT  Evaluadores VALUES (@cedula,@email,@nombre,@telefono,@calificacion);";
                 cmd.Parameters.AddWithValue("@email", this.Email);
                 cmd.Parameters.AddWithValue("@calificacion", this.Calificacion);
                 cmd.Parameters.AddWithValue("@cedula", this.Cedula);
                 cmd.Parameters.AddWithValue("@telefono", this.Telefono);
                 cmd.Parameters.AddWithValue("@nombre", this.Nombre);
                 filas = cmd.ExecuteNonQuery();
                 if (filas != 0) {
                 trans.Commit();
                 salida = true; }


             }
             catch (SqlException ex)
             {
                 if (trans != null) trans.Rollback();
                 System.Diagnostics.Debug.Assert(false, ex.Message);

             }
             finally
             {
                 CerrarConexion(cn);
             }
             return salida;
         }*/


        public override bool insertar()
        {
            SqlConnection con = CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            bool ok = false;

            if (this.Email == "" || this.Contrasena.Length < 8 || this.Nombre == "" || this.Cedula == "" || this.Rol == "" 
                || this.Telefono == "" || this.Calificacion < 1 || this.Calificacion > 4 || Usuario.buscarPorEmail(this.Email) != null) return false;

            try
            {

                AbrirConexion(con);


                cmd.CommandText = "Evaluador_Insert";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par = new SqlParameter();
                par.ParameterName = "@Error";
                par.SqlDbType = SqlDbType.Int;
                par.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par);

                SqlParameter par2 = new SqlParameter();
                par2.ParameterName = "@IdEvaluador";
                par2.SqlDbType = SqlDbType.Int;
                par2.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par2);

                SqlParameter par3 = new SqlParameter();
                par3.ParameterName = "@IdUsuario";
                par3.SqlDbType = SqlDbType.Int;
                par3.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par3);

                cmd.Parameters.Add(new SqlParameter("@Email", this.Email));
                cmd.Parameters.Add(new SqlParameter("@Contrasena", this.Contrasena));
                cmd.Parameters.Add(new SqlParameter("@Rol", this.Rol));

                cmd.Parameters.Add(new SqlParameter("@Cedula", this.Cedula));
                cmd.Parameters.Add(new SqlParameter("@Nombre", this.Nombre));
                cmd.Parameters.Add(new SqlParameter("@Telefono", this.Telefono));
                cmd.Parameters.Add(new SqlParameter("@Calificacion", this.Calificacion));
                cmd.ExecuteNonQuery();

                IdEvaluador = (int)par2.Value;
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

        public static List<Evaluador> buscarTodos()
        {
            SqlConnection con = CrearConexion();
            SqlDataReader reader = null;
            List<Evaluador> listaEvaluadores = null;

            try
            {
                SqlCommand com = new SqlCommand("TodosEvaluador", con);
                com.CommandType = CommandType.StoredProcedure;

                con.Open();
                reader = com.ExecuteReader();
                if (reader.HasRows)
                {

                    listaEvaluadores = new List<Evaluador>();
                    while (reader.Read())
                    {
                        Evaluador ev = new Evaluador();
                        ev.Email = reader["Email"].ToString();
                        ev.Cedula = reader["Cedula"].ToString();
                        ev.Nombre = reader["Nombre"].ToString();
                        ev.Telefono = reader["Telefono"].ToString();
                        ev.Calificacion = reader.GetInt32(reader.GetOrdinal("Calificacion"));

                        Usuario u = new Usuario();
                        u.Email = ev.Email;
                        if (u.buscar())
                        {
                            ev.Contrasena = u.Contrasena;
                            ev.Rol = u.Rol;
                            
                        }
                        listaEvaluadores.Add(ev);

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

            return listaEvaluadores;
        }

        public static bool verificarEvaluador(int idEvaluador, int codEmp)
        {
            bool ok = false;

            SqlConnection con = CrearConexion();
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = @"SELECT * FROM Evaluador ev, Emprendimiento e WHERE IdEvaluador=@IdEvaluador AND Emprentimiento = @CodEmp";
            com.Parameters.AddWithValue("@IdEvaluador", idEvaluador);
            com.Parameters.AddWithValue("@CodEmp", codEmp);

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

        public bool agregarEmprendimiento(int CodId)
        {
            bool retorno = false;
            Evaluacion eval = new Evaluacion();
            Emprendimiento e = new Emprendimiento();
            if (e.buscar(CodId))
            {
                eval.Emprendimiento = e;

                if (eval.insertarEvaluacion(this.IdEvaluador))
                {
                    retorno = true;
                }
            }

            return retorno;
        }

        public static List<Emprendimiento> buscarEmpDeEvaluador(int idEvaluador)
        {
            SqlConnection cn = CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM Evaluacion WHERE IdEvaluador=@IdEvaluador and Estado=@Estado";
            cmd.Parameters.AddWithValue("@IdEvaluador", idEvaluador);
            cmd.Parameters.AddWithValue("@Estado", false);


            cmd.Connection = cn;
            List<Emprendimiento> listaEmprendimientos = null;
            List<Evaluacion> misEvaluaciones = null;
            try
            {
                AbrirConexion(cn);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    misEvaluaciones = new List<Evaluacion>();

                    while (dr.Read())
                    {
                        Evaluacion e = new Evaluacion();

                        e.Emprendimiento = new Emprendimiento();
                        if (e.Emprendimiento != null)
                        {
                            e.Emprendimiento.CodId = dr.GetInt32(dr.GetOrdinal("Emprendimiento"));

                            misEvaluaciones.Add(e);
                        }

                    }


                }
                dr.Close();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = cn;


                listaEmprendimientos = new List<Emprendimiento>();

                if (misEvaluaciones != null)
                {
                    foreach (Evaluacion e in misEvaluaciones)
                    {




                        if (e != null)
                        {
                            int Id = e.Emprendimiento.CodId;

                            cmd1.CommandText = @"SELECT * FROM Emprendimiento WHERE CodId=@Id";
                            cmd1.Parameters.AddWithValue("@Id", Id);

                            SqlDataReader dr1 = cmd1.ExecuteReader();

                            if (dr1.HasRows)
                            {
                                while (dr1.Read())
                                {
                                    Emprendimiento emp = new Emprendimiento();
                                    // {

                                    emp.CodId = dr1.GetInt32(dr1.GetOrdinal("CodId"));
                                    emp.Titulo = dr1.GetString(dr1.GetOrdinal("Titulo"));
                                    emp.Descripcion = dr1.GetString(dr1.GetOrdinal("Descripcion"));
                                    emp.Costo = dr1.GetDouble(dr1.GetOrdinal("Costo"));
                                    emp.TiempoEjecucion = dr1.GetInt32(dr1.GetOrdinal("Tiempo_Ejecucion"));
                                    emp.Financiado = dr1.GetBoolean(dr1.GetOrdinal("Financiado"));
                                    // };


                                    listaEmprendimientos.Add(emp);



                                }

                            }
                            cmd1.Parameters.Clear();
                            dr1.Close();

                        }

                    }
                }

                return listaEmprendimientos;
            }
            catch (SqlException ex)
            {
                
                return listaEmprendimientos;
                throw;
            }
            finally
            {
                
                CerrarConexion(cn);
            }

        }

        public static int buscarIdEvaluador (string username)
        {
            int salida = -1;
            SqlConnection cn = CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT ev.IdEvaluador FROM Usuario u, Evaluador ev WHERE u.Email=@Email AND u.IdUsuario = ev.IdUsuario";
            cmd.Parameters.AddWithValue("@Email", username);
            cmd.Connection = cn;
           
            try
            {
                AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    salida = dr.GetInt32(dr.GetOrdinal("IdEvaluador"));
                }
                return salida;
            }
            catch (SqlException ex)
            {
                //
                System.Diagnostics.Debug.Assert(false, ex.Message);
                
            }
            finally
            {
                CerrarConexion(cn);
            }
            return salida;
        }
        #endregion

    }

}

