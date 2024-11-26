using ParcialApp.Dominio;
using ParcialApp.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Acceso_a_datos
{
    interface IDao
    {

        //Contiene CRUD 
        //Metodos que usan los de abajo
        int ProximoIdMaestro();

        //Metodos requeridos 
        bool Save(Turno oTurno);
        List<Medico> ListarMedicos();
        int ListarCantidadTurnos(DateTime fecha,DateTime hora, int nroMatricula);
    }
}
