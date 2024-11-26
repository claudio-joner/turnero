namespace BibliotecaClases.Dominio
{
    public class Detalle
    {
        public Medico Medico { get; set; }
        public string MotivoConsulta { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }


        public Detalle(Medico medico, string motivoConsulta, string fecha, string hora) 
        { 
            Medico = medico; 
            MotivoConsulta = motivoConsulta; 
            Fecha = fecha; 
            Hora = hora; 
        }
        public override string ToString() 
        { 
            return "Medico: " + Medico.Nombre + " " + Medico.Apellido + " | " + MotivoConsulta + " | " + Fecha + ":" + Hora;
        }

    }
}