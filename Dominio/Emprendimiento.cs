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
    public class Emprendimiento : AbstractBase
    {
       
        #region Propiedades autoimplementadas
        public int CodId { get; set; }
        public string Titulo { get; set; }
       
        public string Descripcion { get; set; }
       
        public double Costo { get; set; }
        
        public int TiempoEjecucion { get; set; }
        
        public List<Integrante> Integrantes { get; set; }
        
        public bool Financiado { get; set; }
        
        #endregion

        #region Metodos
        public bool agregarIntegrante(Integrante inte)
        {
            bool retorno = false;
            int cantIntegrantes = this.Integrantes.Count;
            this.Integrantes.Add(inte);

            if (Integrantes.Count > cantIntegrantes)
            {
                retorno = true;
            }

            return retorno;
        }
        #endregion

        #region Active Record
        public override bool insertar()
        {
            this.CodId = -1;
            SqlConnection con = CrearConexion();
            SqlTransaction trn = null;
            bool retorno = false;
            int afectadas = -1;

            if (this.Titulo == "" || this.Descripcion == "" || this.Costo < 0 || this.TiempoEjecucion <= 0 || !Emprendimiento.verificarEmprendimiento(this.Titulo)) return false; 

            try
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "Emprendimiento_Insert";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Titulo", this.Titulo));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", this.Descripcion));
                    cmd.Parameters.Add(new SqlParameter("@Costo", this.Costo));
                    cmd.Parameters.Add(new SqlParameter("@TiempoEjecucion", this.TiempoEjecucion));
                    cmd.Parameters.Add(new SqlParameter("@Financiado", this.Financiado));

                    AbrirConexion(con);

                    trn = con.BeginTransaction();

                    cmd.Transaction = trn;

                    this.CodId = (int)cmd.ExecuteScalar();

                    if (this.CodId == -1) return false;

                    cmd.CommandText = "Integrante_Insert";

                    SqlParameter par = new SqlParameter();
                    par.ParameterName = "@Error";
                    par.SqlDbType = SqlDbType.Int;
                    par.Direction = ParameterDirection.Output;

                    SqlParameter par2 = new SqlParameter();
                    par2.ParameterName = "@IdIntegrante";
                    par2.SqlDbType = SqlDbType.Int;
                    par2.Direction = ParameterDirection.Output;

                    SqlParameter par3 = new SqlParameter();
                    par3.ParameterName = "@IdUsuario";
                    par3.SqlDbType = SqlDbType.Int;
                    par3.Direction = ParameterDirection.Output;

                    foreach (Integrante inte in this.Integrantes)
                    {
                        cmd.Parameters.Clear();


                        cmd.Parameters.Add(par);
                        cmd.Parameters.Add(par2);
                        cmd.Parameters.Add(par3);
                        cmd.Parameters.Add(new SqlParameter("@Email", inte.Email));
                        cmd.Parameters.Add(new SqlParameter("@Contrasena", inte.Contrasena));
                        cmd.Parameters.Add(new SqlParameter("@Rol", inte.Rol));

                        cmd.Parameters.Add(new SqlParameter("@Cedula", inte.Cedula));
                        cmd.Parameters.Add(new SqlParameter("@Nombre", inte.Nombre));
                        cmd.Parameters.Add(new SqlParameter("@Emprendimiento", this.CodId));
                        afectadas = cmd.ExecuteNonQuery();
                        inte.IdIntengrante = (int)par2.Value;
                        inte.IdUsuario = (int)par3.Value;

                    }
                    retorno = (int)par.Value == 0;
                    trn.Commit();
                }
            }
            catch (SqlException ex)
            {

                trn.Rollback();
            }
            finally
            {
                CerrarConexion(con);
            }
            return retorno;
        }

        //Falta terminarlo
        public static List<Emprendimiento> buscarTodos()
        {

            SqlConnection cn = CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM Emprendimiento";

            cmd.Connection = cn;
            List<Emprendimiento> listaEmprendimientos = null;
            try
            {
                AbrirConexion(cn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    listaEmprendimientos = new List<Emprendimiento>();
                    while (dr.Read())
                    {
                        Emprendimiento e = new Emprendimiento()
                        {
                            CodId = dr.GetInt32(dr.GetOrdinal("CodId")),
                            Titulo = dr.GetString(dr.GetOrdinal("Titulo")),
                            Descripcion = dr.GetString(dr.GetOrdinal("Descripcion")),
                            Costo = dr.GetFloat(dr.GetOrdinal("Costo")),
                            TiempoEjecucion = dr.GetInt32(dr.GetOrdinal("Tiempo_Ejecucion")),
                            Financiado = dr.GetBoolean(dr.GetOrdinal("Financiado")),
                        };
                    }
                }
                dr.Close();
                CerrarConexion(cn);
                
                foreach(Emprendimiento e in listaEmprendimientos)
                {
                    e.Integrantes = new List<Integrante>();
                    e.Integrantes.AddRange(e.buscarIntegrantesDeEmp());
                }
                return listaEmprendimientos;
            }
            catch (SqlException ex)
            {
                //
                System.Diagnostics.Debug.Assert(false, ex.Message);
                return null;
            }
            finally
            {
                CerrarConexion(cn);
            }
        }

        //Falta terminarlo
        public List<Integrante> buscarIntegrantesDeEmp()
        {
            List<Integrante> listaInte = new List<Integrante>();
            SqlConnection con = CrearConexion();
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = @"SELECT * FROM Integrante";

            return listaInte;
        }

        public static List<Emprendimiento> busarEmpAagregarEval()
        {

            SqlConnection cn = CrearConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select e.Titulo, e.CodId from Emprendimiento e  where e.CodId not in 
                             (select  e.CodId from  Emprendimiento e,Evaluacion ev where e.CodId= ev.Emprendimiento 
                              Group by e.CodId having count (ev.Emprendimiento)= 3);";

            cmd.Connection = cn;
            List<Emprendimiento> listaEmprendimientos = null;
            try
            {
                AbrirConexion(cn);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    listaEmprendimientos = new List<Emprendimiento>();
                    while (dr.Read())
                    {
                        Emprendimiento e = new Emprendimiento()
                        {
                            CodId = dr.GetInt32(dr.GetOrdinal("CodId")),
                            Titulo = dr.GetString(dr.GetOrdinal("Titulo")),

                        };

                        listaEmprendimientos.Add(e);
                    }
                }

                dr.Close();
                return listaEmprendimientos;
            }
            catch (SqlException ex)
            {
                //
                System.Diagnostics.Debug.Assert(false, ex.Message);
                return null;
            }
            finally
            {
                CerrarConexion(cn);
            }
        }

        public bool buscar(int id)
        {
            bool retorno = false;
            this.CodId = id;
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"Select  * from Emprendimiento where  CodId= @Id ";
            SqlConnection cn = CrearConexion();
            SqlDataReader drResults;

            cmd.Connection = cn;
            cmd.Parameters.Add(new SqlParameter("@Id", id));

            cn.Open();
            drResults = cmd.ExecuteReader();

            if (drResults.Read())
            {
                this.Titulo = drResults["Titulo"].ToString();
                this.Descripcion = drResults["Descripcion"].ToString();
                this.Costo = double.Parse(drResults["Costo"].ToString());
                this.Financiado = bool.Parse(drResults["Financiado"].ToString());
                this.TiempoEjecucion = int.Parse(drResults["Tiempo_Ejecucion"].ToString());
                retorno = true;
            }

            drResults.Close();
            cn.Close();


            return retorno;
        }

        public static bool verificarEmprendimiento(string titulo)
        {
            bool ok = false;
            SqlConnection con = CrearConexion();
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandText = @"SELECT * FROM Emprendimiento WHERE Titulo=@Titulo";
            com.Parameters.AddWithValue("@Titulo", titulo);

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

        public static List<Emprendimiento> mostrarEmprendimiento(Emprendimiento emp)
        {
            List<Emprendimiento> listaEmp = new List<Emprendimiento>();

            listaEmp.Add(emp);
            return listaEmp;
        }



        public override bool actualizar()
        {
            throw new NotImplementedException();
        }

        public override bool eliminar()
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
