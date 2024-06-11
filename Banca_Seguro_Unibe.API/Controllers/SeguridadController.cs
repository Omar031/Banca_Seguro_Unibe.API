using Microsoft.AspNetCore.Mvc;

namespace Banca_Seguro_Unibe.API.Controllers
{
    public class SeguridadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
