using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParcialApp.Acceso_a_datos.DAO
{
    public class Helper
    {
        //ESTA CLASE VA ENTERA

        private static Helper instancia;
        private SqlConnection conexion;

        public Helper()
        {
            conexion = new SqlConnection(Properties.Resources.cadenaConexion1);
        }


        public SqlConnection ObtenerConexion()
        {
            return conexion;   
        }


        //Singleton: crea una unica instancia, si es nula la crea y sino la devuelve
        public static Helper ObtenerInstanciaHelper()
        {
            if (instancia == null)
            {
                instancia = new Helper();

            }
            return instancia;
        }

        //ConsultaSql: te devuelve un dataTable
        public DataTable ConsultaSql(string nomSp,List<Parametro> lParametros)
        {
            DataTable dt = new DataTable();
            conexion.Open();
            SqlCommand comando = new SqlCommand(nomSp, conexion);
            comando.CommandType = CommandType.StoredProcedure;
            if (lParametros != null)
            {
                foreach (Parametro parametro in lParametros)
                {
                    comando.Parameters.AddWithValue(parametro.Clave, parametro.Valor);
                    // Clave se entiende por nombre, es el mismo que el del SP, @nombre

                }
            }
            dt.Load(comando.ExecuteReader());//Se usa para SELECTS   
            conexion.Close();

            return dt;
        }

        //ConsultarIdMaestro: te devuelve el ultimo id
        public int ConsultarIdMaestro(string nomSp,string idResultado)
        {
            int valorID;

            conexion.Open();
            SqlCommand comando = new SqlCommand( nomSp, conexion);
            comando.CommandType= CommandType.StoredProcedure;
            //Parametro Salida
            SqlParameter paramSalida = new SqlParameter();
            paramSalida.ParameterName = idResultado;
            paramSalida.DbType = DbType.Int32;
            paramSalida.Direction = ParameterDirection.Output;

            //Agrego y ejecuto el parametro de salida
            comando.Parameters.Add(paramSalida); 
            comando.ExecuteNonQuery();

            conexion.Close() ;

            valorID = Convert.ToInt32(paramSalida.Value);

            return valorID;
        }

        //EjecutarSqlParamIn: son todos parametros de entrada,hacer lo mismo que ConsultaSql, deevuelve la cantidad de lineas afectadas
        public int EjecutarSqlParamIn(string nombreSp,List<Parametro> lParametros)
        {
            int lineasAfectadas = 0;
            SqlTransaction transaction = null;

            try
            {
                //SqlCommand command = new SqlCommand(nombreSp,conexion,transaction) Ahorro la linea 94 ,98 - 104 menos 102
                SqlCommand comando = new SqlCommand();

                conexion.Open();

                transaction = conexion.BeginTransaction();

                comando.Connection = conexion;
                comando.CommandText = nombreSp; //POSIBLE ERROR
                comando.CommandType = CommandType.StoredProcedure;

                comando.Transaction = transaction;

                if (lParametros != null)
                {
                    foreach(Parametro p in lParametros)
                    {
                        comando.Parameters.AddWithValue(p.Clave, p.Valor);
                        //Otra forma es mas larga es: 
                        /*
                        SqlParameter sqlParametro = new SqlParameter();
                        sqlParametro.ParameterName = p.Clave;
                        sqlParametro.Value = p.Valor;
                        comando.Parameters.Add(sqlParametro.ParameterName,sqlParametro.Value);
                        */
                    }
                }

                lineasAfectadas = comando.ExecuteNonQuery(); //Devuelve cantidad de linea afectadas

                transaction.Commit();


            }
            catch (Exception ex ) //SqlException
            {
                if(ex != null)
                {
                    transaction.Rollback();
                }
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();  
                }
            }

            return lineasAfectadas;
        }

        //EjecutarSqlParamInOut: son todos parametros de entrada,hacer lo mismo que ConsultaSql, deevuelve la cantidad de lineas afectadas
        public int EjecutarSqlParamInOut(string nombreSp, List<Parametro> lParametros,Parametro pSalida)
        {
            int lineasAfectadas = 0;
            SqlTransaction transaction = null;

            try
            {
                //SqlCommand command = new SqlCommand(nombreSp,conexion,transaction) Ahorro la linea 94 ,98 - 104 menos 102
                SqlCommand comando = new SqlCommand();

                conexion.Open();

                transaction = conexion.BeginTransaction();

                comando.Connection = conexion;
                comando.CommandText = nombreSp; //POSIBLE ERROR
                comando.CommandType = CommandType.StoredProcedure;

                comando.Transaction = transaction;

                if (lParametros != null)
                {
                    foreach (Parametro p in lParametros)
                    {
                        comando.Parameters.AddWithValue(p.Clave, p.Valor);
                        //Otra forma es mas larga es: 
                        /*
                        SqlParameter sqlParametro = new SqlParameter();
                        sqlParametro.ParameterName = p.Clave;
                        sqlParametro.Value = p.Valor;
                        comando.Parameters.Add(sqlParametro.ParameterName,sqlParametro.Value);
                        */
                    }
                }

                SqlParameter paramSalida = new SqlParameter();
                paramSalida.ParameterName = pSalida.Clave;
                paramSalida.DbType = DbType.Int32;
                paramSalida.Direction = ParameterDirection.Output;

                //Agrego y ejecuto el parametro de salida
                comando.Parameters.Add(paramSalida);

                lineasAfectadas = comando.ExecuteNonQuery(); //Devuelve cantidad de linea afectadas

                transaction.Commit();


            }
            catch (Exception ex) //SqlException
            {
                if (ex != null)
                {
                    transaction.Rollback();
                }
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return lineasAfectadas;
        }
    }
}
