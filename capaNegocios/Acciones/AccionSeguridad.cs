using capaModelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace capaNegocios.Acciones
{
    public class AccionSeguridad : AccionConexion
    { 
		/// <summary>
		/// Metodo de validacion de usuarios registrados en db
		/// </summary>
		/// <param name="usuario"></param>
		/// <returns></returns>
        public bool CheckUserRegister(LoginUsuario check)
        {
			try
			{
				// validar usuario o correo + clave = user register in db
				var estatus = dbSeguroContext
					.TM_Usuarios?
					.FirstOrDefault(x => (x.Nombre == check.usuario ||
                                    x.Email == check.usuario) && //pending encryption
									 x.ClaveUsuario == check.clave);

				bool sucess = (estatus.flagActivo) ? true : false;

				return sucess;

			}
			catch 
			{
				return false;
				
			}
			 
        }

		/// <summary>
		/// Mostrar la información de los usuarios registrados.
		/// </summary>
		/// <param name="usr"></param>
		/// <returns></returns>
		public Token InfoUsuariobyCliente(LoginUsuario usr, string tk)
		{
            Token temporal = new Token();

			try
			{
				//1. Validar si el usuario

				bool isUser = CheckUserRegister(usr);

				//2. Usuario confirmado en db
				if(isUser) {

                    //3. El usuario {usr} debe estar autorizado por un cliente by tenant
                    var info = dbSeguroContext
                        .VW_UsuarioTenancies
                        .FirstOrDefault(x => x.nombreUsuario.ToLower() == usr.usuario.ToLower() || 
						x.correo.ToLower() == usr.usuario.ToLower());
					 
					return new Token()
					{
						codigoCliente = info.idClientes,
						correo = info.correo,
						idTenant = info.IDTenant.Value,
						isAuthorize = info.isActive,
						idUsuario = info.idUsuario,
						usuario = info.nombreUsuario,
						token = tk						

					}; 

                }
				else
				{
                    return new Token();
                } 

            }
			catch
			{
				return null;
			}

		}


		public bool CheckTokenUser( ClaimsIdentity identity, bool Ischecked = false)
		{ 
            try
			{ 
                Ischecked = (identity.Claims.Count() > 0) ? true : false;				 
            }
			catch (Exception e)
			{
			   new ArgumentException($"Ocurrió un problema de: {e.Message}");
			}

            return Ischecked;
        }


    }
}
