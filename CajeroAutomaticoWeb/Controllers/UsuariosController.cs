using Microsoft.AspNetCore.Mvc;
using Data.Base;
using Web.Models;
using Microsoft.Data.SqlClient;
using Data.Entities;
using Data.Dto;
using Newtonsoft.Json;
using System.Data;

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
        public IActionResult Index(UsuariosViewModel usuario)
        {
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string nroCuenta)
        {
            var response = await baseApi.BuscarUsuario("Usuarios/BuscarUsuario", "?nroCuenta=" + nroCuenta);
            var resultado = response as OkObjectResult;
            if (resultado != null && resultado.StatusCode == StatusCodes.Status200OK)
            {
                return RedirectToAction("VerificarPin", new UsuariosViewModel { NroCuenta = nroCuenta, Intentos = 4 });
            }
            else
            {
                return RedirectToAction("Index", new UsuariosViewModel { mensaje = "No se ha encontrado cuenta activa" });
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
        public async Task<IActionResult> VerificarPin(string nroCuenta, int pin, int intentos)
        {

            var response = await baseApi.VerificarPin("Usuarios/VerificarPin", pin, nroCuenta);
            var resultado = response as OkObjectResult;
            if (resultado != null && resultado.StatusCode == StatusCodes.Status200OK)
            {
                return RedirectToAction("Operaciones", new UsuariosViewModel { NroCuenta = nroCuenta, Pin = pin });
            }
            else
            {
                return RedirectToAction("VerificarPin", new UsuariosViewModel { NroCuenta = nroCuenta, Intentos = --intentos });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Operaciones(UsuariosViewModel usuario)
        {
            var response = await baseApi.RecuperarCuenta(usuario.NroCuenta);
            var resultadoUsuario = response as OkObjectResult;
            UsuariosViewModel modelo = JsonConvert.DeserializeObject<UsuariosDto>(resultadoUsuario.Value.ToString());

            return View(modelo);
        }



        [HttpGet]
        public IActionResult Retiro(UsuariosViewModel usuarios)
        {

            return View(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> Retiro(int IdCuenta, string nroCuenta, int pin, string balance, string cantRetiro)
        {
            var baseApi = new BaseApi(_httpClient);
            var response = await baseApi.RetirarMonto("Usuarios/RetirarMonto", IdCuenta, nroCuenta, pin, Convert.ToDecimal(balance), Convert.ToDecimal(cantRetiro));
            var resultado = response as OkObjectResult;
            if (resultado != null && resultado.StatusCode == StatusCodes.Status200OK)
            {
                OperacionesViewModel modelo = resultado.Value as OperacionesDto;
                modelo.NroCuenta = nroCuenta;

                return RedirectToAction("ReporteOperacion", "Operaciones", modelo);
            }
            else
            {
                return RedirectToAction("Retiro", "Usuarios", new UsuariosViewModel { NroCuenta = nroCuenta, Pin = pin, Balance = Convert.ToDecimal(balance), mensaje = "No tiene suficiente saldo para realizar la operacion" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> Balance(string nroCuenta)
        {
            var response = await baseApi.RecuperarCuenta(nroCuenta);
            var resultadoUsuario = response as OkObjectResult;
            await baseApi.GuardarOperacion(JsonConvert.DeserializeObject<UsuariosDto>(resultadoUsuario.Value.ToString()), 2);
            UsuariosViewModel modelo = JsonConvert.DeserializeObject<UsuariosDto>(resultadoUsuario.Value.ToString());


            return View(modelo);
        }

        [HttpGet]
        public IActionResult Error(int codigoError)
        {
            ViewBag.CodigoError= codigoError;
            return View();
        }

    }
}
