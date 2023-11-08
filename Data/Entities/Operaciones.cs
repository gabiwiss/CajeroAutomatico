using Data.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Operaciones
    {
        public int Id { get; set; }
        [ForeignKey("Usuarios")]
        public int IdUsuario { get; set; }
        public DateTime FechaHoraOperacion { get; set; }
        [ForeignKey("TipoOperacion")]
        public int IdTipoOperacion { get; set; }
        public decimal? MontoRetirado { get; set; }
        public decimal Balance { get; set; }

        public Usuarios? Usuarios { get; set; }
        public TipoOperacion? TipoOperacion { get; set; }

        public static implicit operator Operaciones(OperacionesDto v)
        {
            Operaciones operacion = new Operaciones();
            
            operacion.IdUsuario = v.IdUsuario;
            operacion.FechaHoraOperacion = v.FechaHoraOperacion;
            operacion.IdTipoOperacion = v.IdTipoOperacion;
            operacion.MontoRetirado = v.MontoRetirado;
            operacion.Balance = v.Balance;
            return operacion;
        }
    }
}
