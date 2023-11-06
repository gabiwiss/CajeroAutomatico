using Data.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly UsuariosService _usuariosServices;

        public UsuariosController()
        {
            _usuariosServices = new UsuariosService();
        }

        [HttpGet]
        [Route("BuscarUsuario")]
        public async Task<bool> BuscarUsuario( long nroCuenta)
        {
            return await _usuariosServices.BuscarUsuario(nroCuenta);
        }

        [HttpPost]
        [Route("VerificarPin")]
        public async Task<Usuarios> VerificarPin(Usuarios usuario)
        {
            
            return await _usuariosServices.VerificarPin(usuario.Pin,usuario.NroCuenta);
        }
        [HttpPost]
        [Route("BloquearUsuario")]
        public async Task<bool> BloquearUsuario(Usuarios usuario)
        {
            
            return await _usuariosServices.BloquearUsuario(usuario);
        }

        [HttpPost]
        [Route("RetirarMonto")]
        public async Task<bool> RetirarMonto(Usuarios usuario, decimal monto)
        {
            return await _usuariosServices.RetirarMonto(usuario, monto);
        }
    }
}
