namespace Web.Models
{
    public class UsuariosViewModel
    {

        public int Id { get; set; }
        public string NroCuenta { get; set; }
        public int Pin { get; set; }
        public decimal Balance { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Bloqueado { get; set; }

    }
}
