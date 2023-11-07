using Data.Managers;
using Data.Entities;
using Data.Dto;

namespace API.Services
{
    public class UsuariosService
    {
        private readonly UsuariosManager _usuariosManager;
        public UsuariosService()
        {
            _usuariosManager = new UsuariosManager();
        }

        public async Task<bool> BuscarUsuario(string nroCuenta)
        {
            return await _usuariosManager.BuscarUsuario(nroCuenta);
        }
        public async Task<Usuarios> RecuperarUsuario(string nroCuenta)
        {
            return await _usuariosManager.RecuperarUsuario(nroCuenta);
        }
        public async Task<Usuarios> VerificarPin(UsuariosDto usuarioDto)
        {
            Usuarios usuario = usuarioDto;
            return await _usuariosManager.VerificarPin(usuario.Pin, usuario.NroCuenta);
        }

        public async Task<bool> BloquearUsuario(UsuariosDto usuarioDto)
        {
            Usuarios usuario = usuarioDto;
            return await _usuariosManager.BloquearUsuario(usuario.NroCuenta);
        }

        public async Task<bool> RetirarMonto(UsuariosDto usuarioDto)
        {
            Usuarios usuario = usuarioDto;
            return await _usuariosManager.RetirarMonto(usuario,usuarioDto.Retiro);
        }
    }
}
