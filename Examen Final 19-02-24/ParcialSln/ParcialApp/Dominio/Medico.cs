namespace BibliotecaClases.Dominio
{
    public class Medico
    {
        public int Matricula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }

        public Medico(int nroMatricula,string nom,string ape,string especialidad)
        {
            Matricula = nroMatricula;
            Nombre = nom;
            Apellido = ape;
            Especialidad = especialidad;
        }

        public override string ToString()
        {
            return "Matricula" + Matricula.ToString() +"| Doctor: "+ Nombre + " " + Apellido + " |Especialidad: " + Especialidad;
        }
    }
}