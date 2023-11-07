using API.Services;
using Microsoft.AspNetCore.Mvc;
using Data.Entities;
using Data.Dto;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperacionesController : Controller
    {
        private readonly OperacionesService _operacionesService;

        public OperacionesController()
        {
            _operacionesService = new OperacionesService();
        }
        [HttpPost]
        [Route("GuardarOperacion")]
        public async Task<bool> GuardarOperacion(OperacionesDto operacionDto)
        {
            return await _operacionesService.GuardarOperacion(operacionDto);
        }
    }
}
