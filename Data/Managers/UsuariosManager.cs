using Data.Base;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Managers
{
    public class UsuariosManager : BaseManager<Usuarios>
    {
        public Task<Usuarios> BuscarUsuario(string nroCuenta)
        {
            return contextoSingleton.Usuarios.Where(a => a.NroCuenta == nroCuenta && a.Bloqueado == false).SingleAsync();
        }

        public Task<Usuarios> VerificarPin(Usuarios usuario) 
        {
            return contextoSingleton.Usuarios.Where(a => a.NroCuenta == usuario.NroCuenta && a.Bloqueado == false).SingleAsync();
        }

        public async Task<bool> RetirarMonto(Usuarios usuario,double monto)
        {
            contextoSingleton.Update(usuario).State = EntityState.Modified;
            var resultado= await contextoSingleton.SaveChangesAsync() > 0;
            return resultado;
        }
    }
}
