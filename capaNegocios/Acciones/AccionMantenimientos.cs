using capaDatos.Persistencia;
using capaModelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaNegocios.Acciones
{
    public class AccionMantenimientos : AccionConexion
    {

        /// Metodos statment
        /// 
        public List<VW_Tenant> GetTenants(int? id = 0)
        {
            var listar = dbSeguroContext.VW_Tenants.ToList();

            return listar ;

        }

        public string CrearUsuario(Usuario user)
        {
            string msg = string.Empty;
            try
            {
                //validar existencia
                var existe = dbSeguroContext
                    .TM_Usuarios
                    .FirstOrDefault(x => x.Nombre.ToLower() == user.nombre.ToLower() && 
                    x.flagActivo==true);

                //flujo del correcto o incorrecto (exista usuario en db)
                if(existe != null)
                {
                    msg = $"Este usuario {user.nombre} se encuentra registrado";
                }
                else
                { 
                    //1 instanciar a la clase encyptar
                    EncryptUsuarios encypt = new EncryptUsuarios { _password = user.clave, _byteKey = 16 };

                    //2. genero la clave encryptada
                    var pswEncryptada = encypt.Encrypt(encypt);

                    //3. guardar temporal la información a bases de datos

                    TM_Usuario usu = new TM_Usuario
                    {
                        ClaveUsuario = pswEncryptada,
                        CreadoPor = Environment.UserName,
                        Email = user.correo,
                        FechaCreacion = DateTime.Now,
                        flagActivo = true,
                        Nombre = user.nombre
                        

                    };

                    //4. insertar a la tabla INSERT INTO {table_name}
                    dbSeguroContext.TM_Usuarios.InsertOnSubmit(usu);

                    //5. guardar el cambio  (commit)
                    dbSeguroContext.SubmitChanges();

                    msg = $"El usuario {user.nombre} ha sido registrado correctamentes";

                } 

            }
            catch (Exception e)
            {

                return e.Message;
            }

            //resultado final
            return msg;
        }


    }
}
