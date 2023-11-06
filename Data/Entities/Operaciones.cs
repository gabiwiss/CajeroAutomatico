﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Operaciones
    {
        public int Id { get; set; }
        public string NroCuenta { get; set; }
        public DateTime FechaHoraOperacion { get; set; }
        public int CodigoOperacion { get; set; }
        public decimal? MontoRetirado { get; set; }
    }
}
