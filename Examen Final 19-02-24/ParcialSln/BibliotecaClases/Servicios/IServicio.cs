using BibliotecaClases.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases.Servicios
{
    public interface IServicio
    {
        List<Medico> ListarMedicos();

        bool Crearturno(Turno turno);

        int ListarCantTurnos(DateTime fecha,DateTime hora,int nroMatricula);


    }
}
