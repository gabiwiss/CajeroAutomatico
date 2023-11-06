using Data.Managers;
using Data.Entities;

namespace API.Services
{
    public class UsuariosService
    {
        private readonly UsuariosManager _usuariosManager;
        public UsuariosService()
        {
            _usuariosManager = new UsuariosManager();
        }

        public async Task<bool> BuscarUsuario(Int64 nroCuenta)
        {
            string nroCuentaFormateado = Util.ConvertirIntAFormatoDeCuenta(nroCuenta);
            if (nroCuentaFormateado == "incorrecto")
            {
                return false;
            }
            else
            {
                return await _usuariosManager.BuscarUsuario(nroCuentaFormateado);
            }


        }

        public async Task<Usuarios> VerificarPin(int pin, string nroCuenta)
        {
            
            return await _usuariosManager.VerificarPin(pin, nroCuenta);
        }

        public async Task<bool> BloquearUsuario(Usuarios usuario)
        {
           
            return await _usuariosManager.BloquearUsuario(usuario.NroCuenta);
        }

        public async Task<bool> RetirarMonto(Usuarios usuario, decimal monto)
        {
            if (usuario.Balance >= monto)
            {
                usuario.Balance = usuario.Balance - monto;
                return await _usuariosManager.RetirarMonto(usuario);
            }
            else
            {
                return false;
            }

        }
    }
}
