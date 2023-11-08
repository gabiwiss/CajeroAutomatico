using Data.Base;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Data.Managers
{
    public class UsuariosManager : BaseManager<Usuarios>
    {
        public async Task<bool> BuscarUsuario(string nroCuenta)
        {
            return await contextoSingleton.Usuarios.Where(a => a.NroCuenta == nroCuenta && a.Bloqueado == false).CountAsync() > 0;
        }
        public async Task<Usuarios> RecuperarUsuario(string nroCuenta)
        {
            return await contextoSingleton.Usuarios.Where(a => a.NroCuenta == nroCuenta).SingleOrDefaultAsync();
        }
        public async Task<Usuarios> VerificarPin(int pin, string nroCuenta) 
        {
            return await contextoSingleton.Usuarios.Where(a => a.NroCuenta == nroCuenta && a.Pin == pin).SingleOrDefaultAsync();
        }

        public async Task<bool> RetirarMonto(Usuarios usuario, decimal cantRetiro)
        {
            Usuarios usuarioRecuperado = contextoSingleton.Usuarios.Where(a => a.NroCuenta == usuario.NroCuenta && a.Pin == usuario.Pin).SingleOrDefault();
            if (cantRetiro <= usuarioRecuperado.Balance)
            {
                usuarioRecuperado.Balance -= cantRetiro;

                contextoSingleton.Update(usuarioRecuperado).State = EntityState.Modified;
                var resultado = await contextoSingleton.SaveChangesAsync() > 0;
                contextoSingleton.Update(usuarioRecuperado).State = EntityState.Detached;
                return resultado;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> BloquearUsuario(string nroCuenta)
        {
            Usuarios usuario = contextoSingleton.Usuarios.Where(a=>a.NroCuenta == nroCuenta).SingleOrDefault();
            usuario.Bloqueado = true;
            contextoSingleton.Update(usuario).State = EntityState.Modified;
            var resultado = await contextoSingleton.SaveChangesAsync() > 0;
            contextoSingleton.Update(usuario).State = EntityState.Detached;
            return resultado;
        }
    }
}
