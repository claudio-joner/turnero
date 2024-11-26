using BibliotecaClases.Acceso_a_datos;
using BibliotecaClases.Dominio;
using BibliotecaClases.Servicios;
using BibliotecaClases.Servicios.impl;
using System;
using System.Collections.Generic;
using System.Data;

using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Acceso_a_datos.imp
{
    public class Dao : IDao
    {

        //Contiene CRUD , implementa IDao
        //Este es el que tiene toda la logica de CRUD 
        public int ListarCantidadTurnos(DateTime fecha,DateTime hora, int nroMatricula)
        {
            int cantTurnos = 0;
            List<Parametro> lParametros = new List<Parametro>();    
            
            Parametro parametroFecha = new Parametro("@fecha", fecha.ToString("dd/mm/yyyy"));
            lParametros.Add(parametroFecha);
            Parametro parametroHora = new Parametro("@hora", hora.ToString("HH:mm"));
            lParametros.Add(parametroHora);
            Parametro parametroMatricula = new Parametro("@matricula", nroMatricula);
            lParametros.Add(parametroMatricula );

            Parametro paramSalidaCantTurnos = new Parametro("@ctd_turnos", null);
            

            string sp = "SP_CONTAR_TURNOS";
            cantTurnos = Helper.ObtenerInstanciaHelper().EjecutarSqlParamInOut(sp, lParametros, paramSalidaCantTurnos);

            return cantTurnos;  
        }

        public List<Medico> ListarMedicos()
        {
            List<Medico> lMedicosResultado = new List<Medico>();

            string sp = "SP_CONSULTAR_MEDICOS";
            DataTable dt = Helper.ObtenerInstanciaHelper().ConsultaSql(sp, null);

            foreach (DataRow row in dt.Rows)
            {
                //Mapear Con los datos de la row un objeto de la lista, Ej: Medigo 
                int matricula = int.Parse(row[0].ToString());//Se puede indica el titulo
                string nom = row["nombre"].ToString();//Se puede indicar la columna
                string ape = row[1].ToString();
                string esp = row[2].ToString(); 

                Medico medicoResultado = new Medico(matricula, nom, ape, esp);

                lMedicosResultado.Add(medicoResultado);
            }

            return lMedicosResultado;
        }

        public int ProximoIdMaestro()
        {
            string sp = "OBTENER_ULTIMO_ID";
            string parametroSalida = "@ultimoId";
            return Helper.ObtenerInstanciaHelper().ConsultarIdMaestro(sp,parametroSalida);
        }

        public bool Save(Turno oTurno)
        {
            bool estado = true;
            SqlConnection cnn = Helper.ObtenerInstanciaHelper().ObtenerConexion();
            SqlTransaction t = null;
            SqlCommand cmd = new SqlCommand();
            try
            {

                cnn.Open();
                t = cnn.BeginTransaction();
                cmd.Connection = cnn;
                cmd.Transaction = t;
                cmd.CommandText = "SP_INSERTAR_MAESTRO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@paciente", oTurno.Paciente);

                //parámetro de salida:
                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@id";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pOut);
                cmd.ExecuteNonQuery();

                int presupuestoNro = (int)pOut.Value;

                SqlCommand cmdDetalle;
                int detalleNro = 1;
                foreach (Detalle detalleTurno in oTurno.ListaDetalles)
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@id_turno", presupuestoNro);
                    cmdDetalle.Parameters.AddWithValue("@matricula", detalleTurno.Medico.Matricula);
                    cmdDetalle.Parameters.AddWithValue("@motivo", detalleTurno.MotivoConsulta);
                    cmdDetalle.Parameters.AddWithValue("@fecha", detalleTurno.Fecha);
                    cmdDetalle.Parameters.AddWithValue("@fecha", detalleTurno.Hora);
                    cmdDetalle.ExecuteNonQuery();

                    detalleNro++; //Este caso es para cuando la pk de detale no es identity, 
                }
                t.Commit();
            }

            catch (Exception)
            {
                if (t != null)
                    t.Rollback();
                estado = false;
            }

            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            return estado;
        }
    }
}
