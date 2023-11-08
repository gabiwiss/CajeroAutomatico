using API;
using Data.Entities;
using Data.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace Data.Base
{
    public class BaseApi : ControllerBase
    {
        private readonly IHttpClientFactory _httpClient;

        public BaseApi(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> PostToApi(string ControllerName, string nroCuenta)
        {
            var client = _httpClient.CreateClient("useApi");

            UsuariosDto usuarios = new UsuariosDto();

            usuarios.Bloqueado = false;


            var response = await client.PostAsJsonAsync(ControllerName, usuarios);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> BloquearUsuario(string ControllerName, string nroCuenta)
        {
            var client = _httpClient.CreateClient("useApi");

            UsuariosDto usuarios = new UsuariosDto();
            usuarios.NroCuenta=nroCuenta;
            usuarios.Bloqueado=true;
            usuarios.Balance = 0;
            usuarios.Id = 0;
            usuarios.Pin = 0;
            usuarios.FechaVencimiento = new DateTime(2000, 1, 1);

            var response = await client.PostAsJsonAsync(ControllerName, usuarios);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> BuscarUsuario(string ControllerName, object parametrosAdicionales = null)
        {
            var client = _httpClient.CreateClient("useApi");
            ControllerName += parametrosAdicionales;
            var response = await client.GetStringAsync(ControllerName);
            if (Convert.ToBoolean(response))
                return Ok(response);
            else
                return BadRequest();

        }
        public async Task<IActionResult> VerificarPin(string ControllerName, int pin, string nroCuenta)
        {
            var client = _httpClient.CreateClient("useApi");

            UsuariosDto usuarios = new UsuariosDto();
            usuarios.NroCuenta = nroCuenta;
            usuarios.Bloqueado = true;
            usuarios.Balance = 0;
            usuarios.Id = 0;
            usuarios.Pin = pin;
            usuarios.FechaVencimiento = new DateTime(2000, 1, 1);

            var response = await client.PostAsJsonAsync(ControllerName,usuarios);
            if (response!=null && response.StatusCode == System.Net.HttpStatusCode.OK)
                return Ok(response);
            else
                return BadRequest();

        }

        public async Task<IActionResult> RetirarMonto(string ControllerName,int idCuenta, string nroCuenta, int pin,decimal balance, decimal monto)
        {
            var client = _httpClient.CreateClient("useApi");

            UsuariosDto usuarios = new UsuariosDto();
            usuarios.NroCuenta = nroCuenta;
            usuarios.Bloqueado = false;
            usuarios.Balance = balance;
            usuarios.Id = idCuenta;
            usuarios.Pin = pin;
            usuarios.FechaVencimiento = new DateTime(2000, 1, 1);
            usuarios.Retiro = monto;

            var response = await client.PostAsJsonAsync(ControllerName, usuarios);
            if (response.IsSuccessStatusCode && Convert.ToBoolean(await response.Content.ReadAsStringAsync()))
            {
                var content2 =await GuardarOperacion(usuarios, 1);
                
                return content2;
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> GuardarOperacion(UsuariosDto usuario, int IdTipoOperacion)
        {
            var client = _httpClient.CreateClient("useApi");
            OperacionesDto operacion = new OperacionesDto();
            operacion.IdUsuario = usuario.Id;
            operacion.IdTipoOperacion = IdTipoOperacion;
            operacion.MontoRetirado = usuario.Retiro;
            operacion.Balance = usuario.Balance - usuario.Retiro;
            operacion.FechaHoraOperacion = DateTime.Now;
            operacion.Id = 0;
            var response = await client.PostAsJsonAsync("Operaciones/GuardarOperacion", operacion);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(operacion);
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> RecuperarCuenta(string nroCuenta)
        {
            var client = _httpClient.CreateClient("useApi");
            var response = await client.GetAsync("Usuarios/RecuperarUsuario?nroCuenta=" + nroCuenta);

            if (response.IsSuccessStatusCode) 
            {                
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return BadRequest();
            }             
        }
    }
}
