using Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Operaciones
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaHoraOperacion { get; set; }
        public int CodigoOperacion { get; set; }
        public decimal? MontoRetirado { get; set; }
        public decimal Balance { get; set; }

        public static implicit operator Operaciones(OperacionesDto v)
        {
            Operaciones operacion = new Operaciones();
            
            operacion.IdUsuario = v.IdUsuario;
            operacion.FechaHoraOperacion = v.FechaHoraOperacion;
            operacion.CodigoOperacion = v.CodigoOperacion;
            operacion.MontoRetirado = v.MontoRetirado;
            operacion.Balance = v.Balance;
            return operacion;
        }
    }
}
