using capaModelo.Modelos;
using capaNegocios.Acciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Banca_Seguro_Unibe.API.Controllers
{
    //[ApiController]
    public class SeguridadController : Controller
    {
        private readonly AccionSeguridad seguridad = new AccionSeguridad();

        private readonly IConfiguration configurate;

        public SeguridadController(IConfiguration _configurate) 
        {
            this.configurate = _configurate;
        }
         

        /// <summary>
        /// Metodo de acceso de usuarios (login)
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="clave"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/banco/seguridad/Login")]
        public IActionResult Login(LoginUsuario user)
        {
            try
            { 
                //generar token
                var gen_token = GetToken(user);

                //confirmar usuario registrado en db
                if (seguridad.CheckUserRegister(user))
                {
                    //suministrar la información al token 

                   var access = seguridad.InfoUsuariobyCliente(user, gen_token);

                    return Ok(new { msg = "ok", Response = access });
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


        /// <summary>
        /// Metodo de generar token unicos by users
        /// </summary>
        /// <param name="usrs"></param>
        /// <returns></returns>
        public string GetToken(LoginUsuario usrs)
        {
            string codigo_token = string.Empty;

            try
            {
                //Propiedades jwt appsetting.json 
                var jwt = configurate.GetSection("Jwt").Get<JWT>();

                var host = HttpContext.Request.Host;

                var appSetting = jwt.Issuer;

                //valida hosting
                if (!host.Equals(appSetting))
                {
                    var Tclaims = new[] {

                         new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                         new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                         new Claim("usuario", usrs.usuario)

                    };

                    var Securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                    var credentials = new SigningCredentials(Securitykey, SecurityAlgorithms.HmacSha256);
                    var times = DateTime.UtcNow.AddMinutes(jwt.Minutes);

                    var token = new
                        JwtSecurityToken(
                        issuer: jwt.Issuer,
                        audience: jwt.Audience,
                        claims: Tclaims,
                        expires: times,
                        signingCredentials: credentials);

                    codigo_token = new JwtSecurityTokenHandler().WriteToken(token);

                }
                else
                {
                    return "Token no generado";
                }

            }

            catch
            {
                return string.Empty;
            }

            return codigo_token;
        }
    }
}
