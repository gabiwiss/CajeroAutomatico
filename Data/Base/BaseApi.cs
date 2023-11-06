using API;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Data.Base
{
    public class BaseApi : ControllerBase
    {
        private readonly IHttpClientFactory _httpClient;

        public BaseApi(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> PostToApi(string ControllerName, long nroCuenta)
        {
            var client = _httpClient.CreateClient("useApi");
            
            Usuarios usuarios = new Usuarios();
            
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

        public async Task<IActionResult> BloquearUsuario(string ControllerName, long nroCuenta)
        {
            var client = _httpClient.CreateClient("useApi");

            Usuarios usuarios = new Usuarios();
            usuarios.NroCuenta=Util.ConvertirIntAFormatoDeCuenta(nroCuenta);
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
        public async Task<IActionResult> VerificarPin(string ControllerName, int pin, long nroCuenta)
        {
            var client = _httpClient.CreateClient("useApi");

            Usuarios usuarios = new Usuarios();
            usuarios.NroCuenta = Util.ConvertirIntAFormatoDeCuenta(nroCuenta);
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

        public async Task<IActionResult> RetirarMonto(string ControllerName, long nroCuenta, int pin, decimal monto)
        {
            var client = _httpClient.CreateClient("useApi");

            Usuarios usuarios = new Usuarios();
            usuarios.NroCuenta = Util.ConvertirIntAFormatoDeCuenta(nroCuenta);
            usuarios.Bloqueado = false;
            usuarios.Balance = monto;
            usuarios.Id = 0;
            usuarios.Pin = pin;
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

    }
}
