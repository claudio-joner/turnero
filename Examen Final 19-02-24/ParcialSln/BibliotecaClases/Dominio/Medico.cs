namespace BibliotecaClases.Dominio
{
    public class Medico
    {
        public int Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }

        public Medico(int matricula, string nombre, string apellido, string especialidad) 
        {
            Matricula = matricula;
            Nombre = nombre; 
            Apellido = apellido; 
            Especialidad = especialidad; 
        }

        public override string ToString()
        {
            return "Matricula" + Matricula.ToString() +"| Doctor: "+ Nombre + " " + Apellido + " |Especialidad: " + Especialidad;
        }
    }
}