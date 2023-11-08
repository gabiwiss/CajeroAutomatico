using Data.Dto;

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

        public int Intentos { get; set; }
        public string mensaje { get; set; }

        public static implicit operator UsuariosViewModel(UsuariosDto v)
        {
            UsuariosViewModel modelo = new UsuariosViewModel();

            modelo.Id = v.Id;
            modelo.NroCuenta = v.NroCuenta;
            modelo.Pin = v.Pin;
            modelo.Balance = v.Balance;
            modelo.FechaVencimiento = v.FechaVencimiento;
            modelo.Bloqueado = v.Bloqueado;
            return modelo;
        }
    }
}
