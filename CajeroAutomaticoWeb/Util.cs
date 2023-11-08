namespace Web
{
    public class Util
    {
        public static long ConvertirAFormatoDeNumero(string nroCuenta)
        {
            return Convert.ToInt64(nroCuenta.Replace("-", ""));
        }
    }
}
