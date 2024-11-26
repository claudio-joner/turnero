using BibliotecaClases.Acceso_a_datos;
using BibliotecaClases.Acceso_a_datos.imp;
using BibliotecaClases.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Servicios.impl
{
    public class Servicio : IServicio
    {
        //Usa un IDAO

        private IDao dao;

        public Servicio()
        {
            dao = new Dao();
        }


        public bool Crearturno(Turno turno)
        {
            return dao.Save(turno);
        }

        public int ListarCantTurnos(DateTime fecha, DateTime hora ,int nroMatricula)
        {
            return dao.ListarCantidadTurnos(fecha,hora,nroMatricula);
        }

        public List<Medico> ListarMedicos()
        {
            return dao.ListarMedicos();
        }
    }
}
