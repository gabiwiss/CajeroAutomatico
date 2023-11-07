using Microsoft.AspNetCore.Mvc;
using Web.Models;
namespace Web.Controllers
{
    public class OperacionesController : Controller
    {
        public IActionResult ReporteOperacion(OperacionesViewModel modelo)
        {
            return View(modelo);
        }
    }
}
