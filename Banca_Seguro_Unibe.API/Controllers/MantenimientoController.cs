using Microsoft.AspNetCore.Mvc;
using capaNegocios.Acciones;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using capaModelo.Modelos;
using Microsoft.Identity.Client;


namespace Banca_Seguro_Unibe.API.Controllers
{
    [Authorize] 
    public class MantenimientoController : Controller
    {
        private readonly AccionMantenimientos mantenimientos = new AccionMantenimientos();
        private readonly AccionSeguridad seguridad = new AccionSeguridad();

        [HttpGet]
        [Route("/api/banco/mantenimiento/_GetTenants/")]
        public IActionResult _GetTenants() //POST, GET, DELETE O PUT
        {
            try
            { 
                var userIdentity = HttpContext.User.Identity as ClaimsIdentity;

                if (seguridad.CheckTokenUser(userIdentity))
                { 

                    var tenants = mantenimientos.GetTenants();

                    return Ok(new { msg = "Ok", Response = tenants });
                }
                else
                {
                    return NotFound(new {msg = "NotFound" , Response = "No se encontró el token" });
                }
                 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            } 

        }

        [HttpPost] //form. register
        [Route("/api/banco/mantenimento/CreateUser/")]
        public IActionResult CreateUser([FromBody] Usuario usuarios)
        {
            try
            {

                var userIdentity = HttpContext.User.Identity as ClaimsIdentity;

                if (seguridad.CheckTokenUser(userIdentity))
                {
                    var resultado = mantenimientos.CrearUsuario(usuarios);
                    
                    return Ok(resultado);

                }
                else
                {
                    return Unauthorized();
                }
                 

            }
            catch
            {
                return BadRequest();
            }
             
        } 


    }
}
