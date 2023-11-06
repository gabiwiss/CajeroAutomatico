using Microsoft.AspNetCore.Mvc;
using Data.Base;
using Web.Models;
using Microsoft.Data.SqlClient;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly BaseApi baseApi;

        public UsuariosController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            baseApi = new BaseApi(_httpClient);
        }

        [HttpGet]
        public IActionResult Index(string mensaje)
        {
            ViewBag.mensaje = mensaje;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(long nroCuenta)
        {
            
            var response = await baseApi.BuscarUsuario("Usuarios/BuscarUsuario", "?nroCuenta=" + nroCuenta);
            var resultado = response as OkObjectResult;
            if (resultado != null && resultado.StatusCode == StatusCodes.Status200OK)
            {
                return RedirectToAction("VerificarPin",new UsuariosViewModel { NroCuenta = nroCuenta, Intentos = 4});
            }
            else
            {
                return RedirectToAction("Index", new { mensaje = "No se ha encontrado cuenta activa" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> VerificarPin(UsuariosViewModel usuario)
        {
            if (usuario.Intentos <= 0)
            {
                await baseApi.BloquearUsuario("Usuarios/BloquearUsuario", usuario.NroCuenta);
                return RedirectToAction("Index", new { mensaje = "Su cuenta ha sido Bloqueada" });
            }
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> VerificarPin(long nroCuenta, int pin, int intentos)
        {
            
            var response = await baseApi.VerificarPin("Usuarios/VerificarPin", pin, nroCuenta);
            var resultado = response as OkObjectResult;
            if (resultado != null && resultado.StatusCode == StatusCodes.Status200OK)
            {
                return RedirectToAction("Operaciones", new UsuariosViewModel { NroCuenta=nroCuenta, Pin=pin});
            }
            else
            {
                return RedirectToAction("VerificarPin", new UsuariosViewModel { NroCuenta = nroCuenta, Intentos = --intentos });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Operaciones(UsuariosViewModel usuario)
        {
            return View(usuario);
        }

        //[HttpGet]
        //public async Task<IActionResult> VerificarPin(long nroCuenta, int intentos = 4, string mensaje=null)
        //{
        //    var baseApi = new BaseApi(_httpClient);
        //    var response = await baseApi.BuscarUsuario("Usuarios/BuscarUsuario","?nroCuenta="+nroCuenta);
        //    var resultado = response as OkObjectResult;

        //    if (resultado != null && resultado.StatusCode == StatusCodes.Status200OK && intentos>0)
        //    {
        //        ViewBag.mensaje = mensaje;
        //        ViewBag.intentos = intentos;
        //        ViewBag.nroCuenta = nroCuenta;
        //        return View();
        //    }
        //    else if (intentos==0)
        //    {
        //        await baseApi.BloquearUsuario("Usuarios/BloquearUsuario", nroCuenta);
        //        return RedirectToAction("Index", "Usuarios", new { mensaje = "Su cuenta ha sido bloqueada" });
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Usuarios", new { mensaje = "No se ha encontrado la cuenta"});
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> Operaciones(long nroCuenta, int pin, int intentos)
        //{
        //    var baseApi = new BaseApi(_httpClient);
        //    var response = await baseApi.VerificarPin("Usuarios/VerificarPin", pin, nroCuenta);
        //    var resultado = response as OkObjectResult;
        //    if (resultado != null && resultado.StatusCode == StatusCodes.Status200OK)
        //    {
        //        ViewBag.nroCuenta = nroCuenta;
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("VerificarPin", "Usuarios", new { nroCuenta=nroCuenta,intentos=--intentos, mensaje = "PIN INCORRECTO, TIENE "+intentos+" intentos" });
        //    }
        //}

        [HttpGet]
        public IActionResult Retiro(long nroCuenta, int pin)
        {
            
            return View(new UsuariosViewModel { NroCuenta= nroCuenta, Pin= pin});
        }

        [HttpPost]
        public async Task<IActionResult> Retiro(long nroCuenta, int pin, string balance)
        {
            var baseApi = new BaseApi(_httpClient);
            var response = await baseApi.RetirarMonto("Usuarios/RetirarMonto", nroCuenta,pin, Convert.ToDecimal(balance));
            var resultado = response as OkObjectResult;
            if (resultado != null && resultado.StatusCode == StatusCodes.Status200OK)
            {
                return RedirectToAction("ReporteOperacion", "Usuarios",new {nroCuenta=nroCuenta,balance=balance });
            }
            else
            {
                return RedirectToAction("Retiro", "Usuarios", new UsuariosViewModel { NroCuenta= nroCuenta, Pin=pin, mensaje="No tiene suficiente saldo para realizar la operacion"});
            }
            
        }

    }
}
