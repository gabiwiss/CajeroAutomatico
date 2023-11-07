using Data.Entities;
using Data.Dto;
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
        public async Task<bool> BuscarUsuario( string nroCuenta)
        {
            return await _usuariosServices.BuscarUsuario(nroCuenta);
        }
        [HttpGet]
        [Route("RecuperarUsuario")]
        public async Task<Usuarios> RecuperarUsuario(string nroCuenta)
        {
            return await _usuariosServices.RecuperarUsuario(nroCuenta);
        }

        [HttpPost]
        [Route("VerificarPin")]
        public async Task<Usuarios> VerificarPin(UsuariosDto usuarioDto)
        {
            
            return await _usuariosServices.VerificarPin(usuarioDto);
        }
        [HttpPost]
        [Route("BloquearUsuario")]
        public async Task<bool> BloquearUsuario(UsuariosDto usuarioDto)
        {  
            return await _usuariosServices.BloquearUsuario(usuarioDto);
        }

        [HttpPost]
        [Route("RetirarMonto")]
        public async Task<bool> RetirarMonto(UsuariosDto usuarioDto)
        {
            return await _usuariosServices.RetirarMonto(usuarioDto);
        }
    }
}
