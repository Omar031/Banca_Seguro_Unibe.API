using Microsoft.AspNetCore.Mvc;
using capaNegocios.Acciones;


namespace Banca_Seguro_Unibe.API.Controllers
{
    [ApiController]
    public class MantenimientoController : Controller
    {
        private readonly AccionMantenimientos mantenimientos = new AccionMantenimientos();

        [HttpGet]
        [Route("/api/banco/mantenimiento/_GetTenants/")]
        public IActionResult _GetTenants() //POST, GET, DELETE O PUT
        {
            var tenants = mantenimientos.GetTenants();

            return Ok(new { msg = "Ok", Response = tenants});
        }
    }
}
