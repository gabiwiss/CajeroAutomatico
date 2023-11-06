using Data.Base;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Data.Managers
{
    public class OperacionesManager : BaseManager<Operaciones>
    {
        public async Task<bool> GuardarOperacion(Operaciones operacion)
        {
            contextoSingleton.Entry(operacion).State = EntityState.Added;
            var respuesta =await contextoSingleton.SaveChangesAsync() > 0;
            contextoSingleton.Entry(operacion).State = EntityState.Detached;
            return respuesta;
        }
    }
}
