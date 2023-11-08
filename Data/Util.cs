namespace API
{
    public class Util
    {
        public static string ConvertirIntAFormatoDeCuenta(Int64 dato)
        {
            string nroCuenta = dato.ToString();
            if (nroCuenta.Length == 16)
            {
                nroCuenta = nroCuenta.Insert(4, "-");
                nroCuenta = nroCuenta.Insert(9, "-");
                nroCuenta = nroCuenta.Insert(14, "-");

                return nroCuenta;
            }
            return "incorrecto";
        }
    }
}
