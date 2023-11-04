using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string NroCuenta { get; set; }
        public int Pin { get; set; }
        public decimal Balance { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Bloqueado { get; set; }
    }
}
