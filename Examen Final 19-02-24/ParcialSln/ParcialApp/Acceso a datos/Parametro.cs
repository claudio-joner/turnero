namespace ParcialApp.Acceso_a_datos.DAO
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