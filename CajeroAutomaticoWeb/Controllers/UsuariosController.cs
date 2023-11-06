using Microsoft.AspNetCore.Mvc;
using Data.Base;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public UsuariosController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Index(string mensaje)
        {
            ViewBag.mensaje = mensaje;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> VerificarPin(long nroCuenta, int intentos = 4, string mensaje=null)
        {
            var baseApi = new BaseApi(_httpClient);
            var response = await baseApi.BuscarUsuario("Usuarios/BuscarUsuario","?nroCuenta="+nroCuenta);
            var resultado = response as OkObjectResult;

            if (resultado != null && resultado.StatusCode == StatusCodes.Status200OK && intentos>0)
            {
                ViewBag.mensaje = mensaje;
                ViewBag.intentos = intentos;
                ViewBag.nroCuenta = nroCuenta;
                return View();
            }
            else if (intentos==0)
            {
                await baseApi.BloquearUsuario("Usuarios/BloquearUsuario", nroCuenta);
                return RedirectToAction("Index", "Usuarios", new { mensaje = "Su cuenta ha sido bloqueada" });
            }
            else
            {
                return RedirectToAction("Index", "Usuarios", new { mensaje = "No se ha encontrado la cuenta"});
            }
        }

        [HttpPost]
        public async Task<IActionResult> Operaciones(long nroCuenta, int pin, int intentos)
        {
            var baseApi = new BaseApi(_httpClient);
            var response = await baseApi.VerificarPin("Usuarios/VerificarPin", pin, nroCuenta);
            var resultado = response as OkObjectResult;
            if (resultado != null && resultado.StatusCode == StatusCodes.Status200OK)
            {
                ViewBag.nroCuenta = nroCuenta;
                return View();
            }
            else
            {
                return RedirectToAction("VerificarPin", "Usuarios", new { nroCuenta=nroCuenta,intentos=--intentos, mensaje = "PIN INCORRECTO, TIENE "+intentos+" intentos" });
            }
        }

        [HttpGet]
        public IActionResult Retiro(Models.UsuariosViewModel modelo)
        {
            
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Retiro(int nroCuenta, int pin, string balance)
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
                return RedirectToAction("Retiro", "Usuarios", new {mensaje="No tiene suficiente saldo para realizar la operacion"});
            }
            
        }

    }
}
