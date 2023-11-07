using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto
{
    public class OperacionesDto
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaHoraOperacion { get; set; }
        public int CodigoOperacion { get; set; }
        public decimal? MontoRetirado { get; set; }
        public decimal Balance { get; set; }
    }
}
