using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace BibliotecaClases.Dominio
{
    public class Detalle
    {
        public Medico Medico { get; set; }
        public string MotivoConsulta { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }

        public Detalle(Medico medico,string motivo,DateTime fechaTurno)
        {
            Medico = medico;
            MotivoConsulta = motivo;
            Fecha = fechaTurno.Date.ToString("dd/MM/yyyy");
            Hora = fechaTurno.Date.ToString("HH:mm");
        }

        public override string ToString()
        {
            return "Medico: " + Medico.Nombre + " " + Medico.Apellido +" | " + MotivoConsulta + " |" + Fecha + ":" + Hora; 
        }

    }
}