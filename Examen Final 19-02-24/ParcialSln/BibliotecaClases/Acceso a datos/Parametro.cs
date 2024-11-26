namespace BibliotecaClases.Acceso_a_datos
{
    public class Parametro
    {
        public string Clave { get; set; }
        public object Valor { get; set; }

        public Parametro(string nomProp,object value)
        {
            Clave = nomProp;
            Valor = value;
        }
    }
}