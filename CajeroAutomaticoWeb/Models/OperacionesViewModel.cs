using System.Runtime.CompilerServices;
using Data.Entities;
namespace Web.Models
{
    public class OperacionesViewModel
    {
        public int Id { get; set; }
        public string NroCuenta { get; set; }
        public DateTime FechaHoraOperacion { get; set; }
        public int CodigoOperacion { get; set; }
        public decimal? MontoRetirado { get; set; }
        public decimal Balance { get; set; }

        public static implicit operator OperacionesViewModel(Operaciones v)
        {
            OperacionesViewModel operacion= new OperacionesViewModel();
            operacion.Id=v.Id;
            operacion.NroCuenta=v.NroCuenta;
            operacion.FechaHoraOperacion=v.FechaHoraOperacion;
            operacion.CodigoOperacion=v.CodigoOperacion;
            operacion.MontoRetirado=v.MontoRetirado;
            operacion.Balance=v.Balance;
            return operacion;
        }
    }
}
