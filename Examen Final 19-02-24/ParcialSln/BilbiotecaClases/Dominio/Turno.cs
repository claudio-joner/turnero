using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Dominio
{
    public class Turno
    {
        public int idTurno { get; set; }
        public string Paciente { get; set; }
        public bool Estado { get; set; }
        public List<Detalle> ListaDetalles { get; set; }

        public Turno(int id, string nomPaciente,List<Detalle> lDetalles)
        {
            idTurno = id;
            Paciente = nomPaciente;
            Estado = true;
            ListaDetalles = lDetalles;
        }

        public Turno()
        {
            idTurno = 0;
            Paciente = string.Empty;
            Estado = true;
            ListaDetalles = null;
        }
    }
}
